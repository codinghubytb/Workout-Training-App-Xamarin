using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

using Sport.Models;
using Sport.Services;

namespace Sport.ViewModels
{
    [QueryProperty(nameof(ImageId), nameof(ImageId))]
    public class LargerImageViewModel : BaseViewModel
    {
        private ImageSource imgSrc;

        private string imageId;

        public string ImageId
        {
            get
            {
                return imageId;
            }
            set
            {

                imageId = value;
                LoadItemId(value);
            }
        }

        public ImageSource Image
        {
            get => imgSrc;
            set => SetProperty(ref imgSrc, value);
        }

        public void LoadItemId(string imageId)
        {
            try
            {
                Image = imageId.Replace("File: ", "");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
