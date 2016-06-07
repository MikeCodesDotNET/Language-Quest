using System;
using System.Collections.Generic;
namespace LanguageQuest.Models
{
    public class Lesson
    {
        public string Title { get; set; }
        public Category Category { get; set;}
        public List<Word> Words {get; set;}
    }

    public enum Category
    {
        Basics,
        Food,
        Clothes,
        Animals,
        Transport,
        Tech,
        Home,
        Sport,
        Nature
    }
}

