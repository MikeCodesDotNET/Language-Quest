// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace LanguageQuest
{
    [Register ("SadPandaViewController")]
    partial class SadPandaViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnTryAgain { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView imgSadPanda { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblErrorMessage { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblSadPanda { get; set; }

        [Action ("BtnTryAgain_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void BtnTryAgain_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (btnTryAgain != null) {
                btnTryAgain.Dispose ();
                btnTryAgain = null;
            }

            if (imgSadPanda != null) {
                imgSadPanda.Dispose ();
                imgSadPanda = null;
            }

            if (lblErrorMessage != null) {
                lblErrorMessage.Dispose ();
                lblErrorMessage = null;
            }

            if (lblSadPanda != null) {
                lblSadPanda.Dispose ();
                lblSadPanda = null;
            }
        }
    }
}