using Foundation;
using System;
using UIKit;
using Plugin.Connectivity;
using MikeCodesDotNET.iOS;

namespace LanguageQuest
{
    public partial class SadPandaViewController : UIViewController
    {
        public SadPandaViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            imgSadPanda.Alpha = 0;
            lblSadPanda.Alpha = 0;
            lblErrorMessage.Alpha = 0;
            btnTryAgain.Alpha = 0;
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            View.FadeSubviewsIn(0.5f, 0.3f);
        }

        async partial void BtnTryAgain_TouchUpInside(UIButton sender)
        {
            var connected = await CrossConnectivity.Current.IsReachable("google.com");
            if (connected)
               await DismissViewControllerAsync(true);
            
        }
    }
}