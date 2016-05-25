using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics;

using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;

using Plugin.Connectivity;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using LanguageQuest.Models;

namespace LanguageQuest.Services
{
    public class EasyTablesService
    {
        public MobileServiceClient MobileService { get; set; }
        IMobileServiceSyncTable<Word> wordTable;

        bool isInitialized;
        public async Task Initialize()
        {
            if (isInitialized)
                return;

             MobileService = new MobileServiceClient(Helpers.Keys.AzureAppServiceKey, null)
            {
                SerializerSettings = new MobileServiceJsonSerializerSettings()
                {
                    CamelCasePropertyNames = true
                }
            };

            var store = new MobileServiceSQLiteStore("word.db");
            store.DefineTable<Word>();

            await MobileService.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler());

            wordTable = MobileService.GetSyncTable<Word>();

            isInitialized = true;
        }

        public async Task<bool> AddWord(Word headline)
        {
            await Initialize();

            await wordTable.InsertAsync(headline);
            //Synchronize todos
            await SyncWords();
            return true;
        }

        public async Task SyncWords()
        {
            var connected = await CrossConnectivity.Current.IsReachable("google.com");
            if (connected == false)
                return;

            try
            {
                await MobileService.SyncContext.PushAsync();
                await wordTable.PullAsync("allWords", wordTable.CreateQuery());
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to sync items, that is alright as we have offline capabilities: " + ex);
            }
        }

        public async Task<IEnumerable<Word>> GetWords()
        {
            await Initialize();
            await SyncWords();
            return await wordTable.ToEnumerableAsync();
        }
    }
}