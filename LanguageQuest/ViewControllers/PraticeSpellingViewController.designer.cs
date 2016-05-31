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
    [Register ("PraticeSpellingViewController")]
    partial class PraticeSpellingViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblTranslateTo { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblWord { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField tbxTranslation { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (lblTranslateTo != null) {
                lblTranslateTo.Dispose ();
                lblTranslateTo = null;
            }

            if (lblWord != null) {
                lblWord.Dispose ();
                lblWord = null;
            }

            if (tbxTranslation != null) {
                tbxTranslation.Dispose ();
                tbxTranslation = null;
            }
        }
    }
}