using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics;

using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;

using Plugin.Connectivity;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using LanguageQuest.Models;
using LanguageQuest.Abstractions;

namespace LanguageQuest.Services
{
    public class EasyTablesService : IAzureService
    {
        public IMobileServiceClient MobileClient { get; set; }

        IMobileServiceSyncTable<Word> wordTable;
        IMobileServiceSyncTable<Category> categoryTable;

        public bool IsInitialized { get; set; }
        public async Task Initialize()
        {
            if (IsInitialized)
                return;

            MobileClient = new MobileServiceClient(Helpers.Keys.AzureAppServiceKey, null)
            {
                SerializerSettings = new MobileServiceJsonSerializerSettings()
                {
                    CamelCasePropertyNames = true
                }
            };

            var store = new MobileServiceSQLiteStore("languagequest.db");
            store.DefineTable<Word>();
            //store.DefineTable<Category>();

            await MobileClient.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler());

            wordTable = MobileClient.GetSyncTable<Word>();
           // categoryTable = MobileClient.GetSyncTable<Category>();

            IsInitialized = true;
        }

        public async Task<bool> AddWord(Word headline)
        {
            await Initialize();

            await wordTable.InsertAsync(headline);
            //Synchronize todos
            await Sync();
            return true;
        }

        public async Task Sync()
        {
            var connected = await CrossConnectivity.Current.IsReachable("google.com");
            if (connected == false)
                return;

            try
            {
                await MobileClient.SyncContext.PushAsync();
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
            await Sync();
            return await wordTable.ToEnumerableAsync();
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            await Initialize();
            await Sync();
            return await categoryTable.ToEnumerableAsync();
        }
    }
}