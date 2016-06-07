using Foundation;
using System;
using UIKit;
using System.Collections.Generic;
using LanguageQuest.Models;
using LanguageQuest.Abstractions;
using HockeyApp;

namespace LanguageQuest
{
    public partial class LessonsTableViewController : UITableViewController, IUITableViewDataSource
    {
        List<Lesson> lessons;
        public LessonsTableViewController (IntPtr handle) : base (handle)
        {
        }

        async public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var client = Helpers.ServiceLocator.Instance.Resolve<IAzureService>();
            if (client.IsInitialized == false)
                await client.Initialize();

        }

        public override void MotionEnded(UIEventSubtype motion, UIEvent evt)
        {
            if (motion == UIEventSubtype.MotionShake)
            {
                var feedbackManager = BITHockeyManager.SharedHockeyManager.FeedbackManager;
                feedbackManager.NavigationBarTintColor = UIColor.FromRGB(47, 135, 233);
                this.NavigationController.NavigationBar.TintColor = UIColor.FromRGB(47, 135, 233);
                feedbackManager.ShowFeedbackComposeView();
            }
            base.MotionEnded(motion, evt);
        }

        public override nint NumberOfSections(UITableView tableView)
        {
            return 0;
        }

        public override nint RowsInSection(UITableView tableView, nint section)
        {
            return lessons.Count;
        }
    }
}