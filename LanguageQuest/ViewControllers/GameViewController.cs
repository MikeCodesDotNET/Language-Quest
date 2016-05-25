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
        List<LanguageQuest.Models.Word> Words = new List<LanguageQuest.Models.Word>();

        public GameViewController (IntPtr handle) : base (handle)
        {
        }

        async public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            lblWord.Text = "Koffie";
            lblWord.Alpha = 0;

            var coffie = new LanguageQuest.Models.Word { English = "Coffee", Translation = "Koffie" };
            var cat = new LanguageQuest.Models.Word { English = "Cat", Translation = "Kat" };

            Words.Add(coffie);
            Words.Add(cat);
        }

        async void ImagePicker_FinishedPickingImage(object sender, UIImagePickerImagePickedEventArgs e)
        {
            var image = e.Image;

            var maxResizeFactor = Math.Min(400 / image.Size.Width, 400 / image.Size.Height);
            var width = maxResizeFactor * image.Size.Width;
            var height = maxResizeFactor * image.Size.Height;
            image.Scale(new CoreGraphics.CGSize(width, height));
            var stream = image.AsPNG().AsStream();

            try
            {
                Acr.UserDialogs.UserDialogs.Instance.ShowLoading("Analyzing Image");
                var result = await cognitiveServices.GetImageDescription(stream);
                Acr.UserDialogs.UserDialogs.Instance.HideLoading();
            }
            catch (Exception ex)
            {
                Acr.UserDialogs.UserDialogs.Instance.ShowError(ex.Message);
            }
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            lblWord.FadeIn(1, 1);
        }

        async partial void BtnSnapPhoto_TouchUpInside(UIButton sender)
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
            throw new NotImplementedException();
        }

        void ValidateVisionResponse(AnalysisResult result)
        {
            var selectedWord = Words.FirstOrDefault();
            foreach (var tag in result.Description.Tags)
            {
                Console.WriteLine(tag);
                if (tag == selectedWord.English.ToLower())
                {
                    Acr.UserDialogs.UserDialogs.Instance.ShowSuccess("Correct!");
                    Words.Remove(selectedWord);
                    if (Words.Count > 0)
                        lblWord.Text = Words.FirstOrDefault().Translation;
                    else
                    {
                        lblWord.Text = "Game Over";
                        btnSkip.Alpha = 0;
                        btnSnapPhoto.Alpha = 0;
                    }
                    return;
                }
            }

            Acr.UserDialogs.UserDialogs.Instance.ShowError($"Could not find {selectedWord.Translation}");
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