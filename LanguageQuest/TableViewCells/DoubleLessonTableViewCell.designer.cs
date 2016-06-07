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
    [Register ("DoubleLessonTableViewCell")]
    partial class DoubleLessonTableViewCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView imgIcon1 { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView imgIcon2 { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblTitle1 { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblTitle2 { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (imgIcon1 != null) {
                imgIcon1.Dispose ();
                imgIcon1 = null;
            }

            if (imgIcon2 != null) {
                imgIcon2.Dispose ();
                imgIcon2 = null;
            }

            if (lblTitle1 != null) {
                lblTitle1.Dispose ();
                lblTitle1 = null;
            }

            if (lblTitle2 != null) {
                lblTitle2.Dispose ();
                lblTitle2 = null;
            }
        }
    }
}