 using Sport.Models;
using Sport.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

using Sport.Views;
using System.IO;

namespace Sport.ViewModels
{
    public class NewExerciceViewModel : BaseViewModel
    {
        private ExerciseDatabase exerciseDatabase;

        private ImageStorage imageStorage;
        public FileResult takeImage { get; set; }

        public Command FavoriteCommand { get; }
        public Command TakePhotoCommand { get; }
        public Command SaveCommand { get; }
        public Command CancelCommand { get; }
        public Command ViewImageLargerCommand { get; }
        public Command DifficultyCommand { get; }

        private ImageSource imgSrc = "noImage.png";

        private DateTime date = DateTime.Now;

        private string text;
        private string description;
        private string textDifficulty = "Easy";
        private string _starFavorite = "☆";

        private bool favorite = false;

        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public string TextDifficulty
        {
            get => textDifficulty;
            set => SetProperty(ref textDifficulty, value);
        }

        public DateTime Date
        {
            get => date;
        }

        public ImageSource Image
        {
            get => imgSrc;
            set => SetProperty(ref imgSrc, value);
        }

        public string Favorite
        {
            get => _starFavorite;
            set => SetProperty(ref _starFavorite, value);
        }

        public NewExerciceViewModel()
        {
            exerciseDatabase = new ExerciseDatabase();
            imageStorage = new ImageStorage();
            TakePhotoCommand = new Command(OnTakePhoto);
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            FavoriteCommand = new Command(OnFavorite);
            DifficultyCommand = new Command(OnDifficulty);
            ViewImageLargerCommand = new Command(OnImageLarger);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(text)
                && !String.IsNullOrWhiteSpace(description);
        }


        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            Exercise newExercise = new Exercise();
            try
            {
                newExercise.Name = Text;
                newExercise.Description = Description;
                newExercise.DateTime = Date;
                if (takeImage != null)
                    newExercise.Source = imageStorage.SourceImage(takeImage);
                else
                    newExercise.Source = imageStorage.SourceImage(imgSrc);

                newExercise.Difficulty = textDifficulty;
                newExercise.Favorite = favorite;
                newExercise.ColorDifficulty = ColorDifficultyCommand(textDifficulty);
                newExercise.isEdit = true;
                await exerciseDatabase.SaveExerciseAsync(newExercise);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            try
            {
                List<Exercise> exercise = await exerciseDatabase.GetExerciseAsync();
                await Shell.Current.GoToAsync($"{nameof(CheckBodyPartsExercisePage)}?{nameof(BodyPartsExerciseViewModel.ExerciseId)}={exercise[exercise.Count-1].Id}");
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private async void OnTakePhoto()
        {
            try
            {
                takeImage = await imageStorage.OpenMediaPickerStorage();
                Image = await imageStorage.ReadImage(takeImage);
            }
            catch
            {
                Image = imgSrc;
            }
        }

        private async void OnDifficulty()
        {
            var result = await App.Current.MainPage.DisplayActionSheet("Difficulty Exercise", "Cancel", null, "Easy", "Medium", "Hard");

            if (result != "Cancel" && !string.IsNullOrEmpty(result))
            {
                TextDifficulty = result;
            }
            else
                TextDifficulty = "Easy";
        }

        private void OnFavorite()
        {
            favorite = !favorite;
            if (favorite)
                Favorite = "★";
            else
            {
                Favorite = "☆";
            }

        }
        public string ColorDifficultyCommand(string difficulty)
        {
            switch (difficulty)
            {
                case "Easy":
                    return "#32cd32";
                case "Medium":
                    return "#ff7f00";
                case "Hard":
                    return "#ff0000";
            }

            return "#32cd32";
        }

        public async void OnImageLarger()
        {
            try
            {
                await Shell.Current.GoToAsync($"{nameof(LargerImagePage)}?{nameof(LargerImageViewModel.ImageId)}={imageStorage.SourceImage(takeImage)}");
            }
            catch
            {
                await Shell.Current.GoToAsync($"{nameof(LargerImagePage)}?{nameof(LargerImageViewModel.ImageId)}={imgSrc}");
            }
        }
    }
}
