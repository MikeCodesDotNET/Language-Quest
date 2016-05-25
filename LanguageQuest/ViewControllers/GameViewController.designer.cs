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
    [Register ("GameViewController")]
    partial class GameViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnSkip { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnSnapPhoto { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblWord { get; set; }

        [Action ("BtnSnapPhoto_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void BtnSnapPhoto_TouchUpInside (UIKit.UIButton sender);

        [Action ("BtnSkip_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void BtnSkip_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (btnSkip != null) {
                btnSkip.Dispose ();
                btnSkip = null;
            }

            if (btnSnapPhoto != null) {
                btnSnapPhoto.Dispose ();
                btnSnapPhoto = null;
            }

            if (lblWord != null) {
                lblWord.Dispose ();
                lblWord = null;
            }
        }
    }
}