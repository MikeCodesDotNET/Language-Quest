using Foundation;
using System;
using UIKit;
using System.Collections.Generic;
using LanguageQuest.Models;
using System.Linq;

namespace LanguageQuest
{
    public partial class SingleLessonTableViewCell : UITableViewCell
    {
        public SingleLessonTableViewCell (IntPtr handle) : base (handle)
        {
        }

        public void Init(List<Lesson> lessons)
        {
            if (lessons.Count > 1)
                throw new Exception("Too many lessons for this cell");

            var lesson = lessons.FirstOrDefault();
            lblTitle.Text = lesson.Title;

            switch (lesson.Category)
            {
                case Models.Category.Basics:
                    imgIcon.Image = UIImage.FromFile("category_basics.png");
                    break;
                case Models.Category.Animals:
                    imgIcon.Image = UIImage.FromFile("category_animals.png");
                    break;
                case Models.Category.Clothes:
                    imgIcon.Image = UIImage.FromFile("category_clothes.png");
                    break;
                case Models.Category.Food:
                    imgIcon.Image = UIImage.FromFile("category_food.png");
                    break;
                case Models.Category.Transport:
                    imgIcon.Image = UIImage.FromFile("category_transport.png");
                    break;
                case Models.Category.Tech:
                    imgIcon.Image = UIImage.FromFile("category_tech.png");
                    break;
                case Models.Category.Home:
                    imgIcon.Image = UIImage.FromFile("category_home.png");
                    break;
                case Models.Category.Sport:
                    imgIcon.Image = UIImage.FromFile("category_sport.png");
                    break;
                case Models.Category.Nature:
                    imgIcon.Image = UIImage.FromFile("category_nature.png");
                    break;
                default:
                    break;
            }
        }
    }
}