using Foundation;
using System;
using UIKit;

using System.Collections.Generic;

using LanguageQuest.Services;
using Microsoft.ProjectOxford.Vision.Contract;
using System.Linq;

using MikeCodesDotNET.iOS;

namespace LanguageQuest
{
    public partial class GameViewController : UIViewController
    {
        CognitiveService cognitiveServices = new CognitiveService();
        EasyTablesService easyTableService = new EasyTablesService();

        List<Models.Word> words = new List<Models.Word>();

        public GameViewController (IntPtr handle) : base (handle)
        {
        }

        async public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            lblWord.Alpha = 0;
            lblSyncing.Alpha = 1;
            spinner.Alpha = 1;

            var result = await easyTableService.GetWords();
            words = result.ToList();

            lblWord.Text = words.FirstOrDefault().Dutch;

            var coffie = new Models.Word { English = "Coffee", Dutch = "Koffie" };
            var cat = new Models.Word { English = "Cat", Dutch = "Kat" };

            words.Add(coffie);
            words.Add(cat);
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            lblSyncing.Alpha = 0;
            spinner.Alpha = 0;
            lblWord.FadeIn(1, 1);
        }

        partial void BtnSnapPhoto_TouchUpInside(UIButton sender)
        {
            var imagePicker = new UIImagePickerController();
            imagePicker.SourceType = UIImagePickerControllerSourceType.Camera;
            PresentViewController(imagePicker, true, null);
            imagePicker.Canceled += async delegate
            {
                await imagePicker.DismissViewControllerAsync(true);
            };

            imagePicker.FinishedPickingMedia += async (object s, UIImagePickerMediaPickedEventArgs e) =>
            {
                try
                {
                    await imagePicker.DismissViewControllerAsync(true);

                    var image = e.OriginalImage;
                    Acr.UserDialogs.UserDialogs.Instance.ShowLoading("Analyzing Image");

                    var stream = ScaledImage(image, 500, 500).AsPNG().AsStream();
                    var result = await cognitiveServices.GetImageDescription(stream);
                    Acr.UserDialogs.UserDialogs.Instance.HideLoading();

                    ValidateVisionResponse(result);
                }
                catch (Exception ex)
                {
                    Acr.UserDialogs.UserDialogs.Instance.ShowError(ex.Message);
                }
            };
        }

        partial void BtnSkip_TouchUpInside(UIButton sender)
        {
            words.RemoveAt(0);
            lblWord.Text = words.FirstOrDefault().Dutch;
        }

        void ValidateVisionResponse(AnalysisResult result)
        {
            var selectedWord = words.FirstOrDefault();
            foreach (var tag in result.Description.Tags)
            {
                if (tag == selectedWord.English.ToLower())
                {
                    Acr.UserDialogs.UserDialogs.Instance.ShowSuccess("Correct!");
                    words.Remove(selectedWord);
                    if (words.Count > 0)
                        lblWord.Text = words.FirstOrDefault().Dutch;
                    else
                    {
                        lblWord.Text = "Game Over";
                        btnSkip.Alpha = 0;
                        btnSnapPhoto.Alpha = 0;
                    }
                    return;
                }
            }

            Acr.UserDialogs.UserDialogs.Instance.ShowError($"Could not find {selectedWord.Dutch}");
        }

        UIImage ScaledImage(UIImage image, nfloat maxWidth, nfloat maxHeight)
        {
            var maxResizeFactor = Math.Min(maxWidth / image.Size.Width, maxHeight / image.Size.Height);
            var width = maxResizeFactor * image.Size.Width;
            var height = maxResizeFactor * image.Size.Height;
            return image.Scale(new CoreGraphics.CGSize(width, height));
        }
    }
}