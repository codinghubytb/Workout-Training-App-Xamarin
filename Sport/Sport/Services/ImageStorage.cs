using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Sport.Services
{
    public class ImageStorage
    {
        /// <summary>
        /// Open Media Picker Storage
        /// </summary>
        /// <returns>FileResult</returns>
        public async Task<FileResult> OpenMediaPickerStorage()
        {
            var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
            {
                Title = "Please pick a photo"
            });

            return result;
        }

        /// <summary>
        /// Read FileResult
        /// </summary>
        /// <param name="fileResult">FileResult</param>
        /// <returns>ImageSource</returns>
        public async Task<ImageSource> ReadImage(FileResult fileResult)
        {
            var stream = await fileResult.OpenReadAsync();
            return ImageSource.FromStream(() => stream);
        }

        /// <summary>
        /// Give source image full path
        /// </summary>
        /// <param name="fileResult">FileResult</param>
        /// <returns>Full path image</returns>
        public String SourceImage(FileResult fileResult)
        {
            return fileResult.FullPath;
        }

        /// <summary>
        /// Give source image
        /// </summary>
        /// <param name="imgSrc">ImageSource</param>
        /// <returns>Source Image</returns>
        public String SourceImage(ImageSource imgSrc)
        {
            string result = imgSrc.ToString();
            return result.Replace("File: ","");
        }
    }
}
