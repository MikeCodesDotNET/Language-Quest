using System;
namespace LanguageQuest.Models
{
    public class Word : EntityData
    {
        public string English { get; set;}

        public string Dutch { get; set;}

        public Category Category { get; set;}

        public string Photographable { get; set;}
    }
}

