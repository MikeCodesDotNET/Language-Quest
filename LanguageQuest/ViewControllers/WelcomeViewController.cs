using Foundation;
using System;
using UIKit;
using LanguageQuest.Abstractions;
using Newtonsoft.Json.Linq;
using Microsoft.WindowsAzure.MobileServices;

namespace LanguageQuest
{
    public partial class WelcomeViewController : UIViewController
    {
        public WelcomeViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            btnLogin.Layer.CornerRadius = btnLogin.Frame.Height / 2;
            btnLogin.Layer.MasksToBounds = true;
        }

        async partial void BtnLogin_TouchUpInside(UIButton sender)
        {
            var connected = await Plugin.Connectivity.CrossConnectivity.Current.IsReachable("google.com");
            if (connected)
            {
                var client = Helpers.ServiceLocator.Instance.Resolve<IAzureService>();
                if (!client.IsInitialized)
                    await client.Initialize();

                try
                {
                    await client.MobileClient.LoginAsync(this, MobileServiceAuthenticationProvider.Facebook);
                    var vc = Storyboard.InstantiateViewController("MainAppNavigationController");
                    await PresentViewControllerAsync(vc, true);
                }
                catch (Exception ex)
                {
                    Acr.UserDialogs.UserDialogs.Instance.ShowError(ex.Message);
                }
            }
            else
            {
                var vc = Storyboard.InstantiateViewController("SadPanda");
                await PresentViewControllerAsync(vc, true);
            }
        }
    }
}