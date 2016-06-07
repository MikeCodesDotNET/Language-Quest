using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LanguageQuest.Models;
using Microsoft.WindowsAzure.MobileServices;

namespace LanguageQuest.Abstractions
{
    public interface IAzureService
    {
        IMobileServiceClient MobileClient { get; set;}

        Task Initialize();

        Task<bool> AddWord(Word headline);

        Task Sync();

        Task<IEnumerable<Word>> GetWords();

        bool IsInitialized { get; set; }
    }
}

