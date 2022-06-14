using Sport.Models;
using Sport.Services;
using Sport.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Sport.ViewModels
{
    [QueryProperty(nameof(ExerciseId), nameof(ExerciseId))]
    public class EditExerciseViewModel : BaseViewModel
    {

        private ExerciseDatabase exerciseDatabase;

        private ImageStorage imageStorage;

        private Exercise GetExercise;
        public FileResult takeImage { get; set; }
        public Command FavoriteCommand { get; }
        public Command TakePhotoCommand { get; }
        public Command SaveCommand { get; }
        public Command CancelCommand { get; }
        public Command ViewImageLargerCommand { get; }
        public Command DifficultyCommand { get; }

        private DateTime date = DateTime.Now;

        private ImageSource imgSrc;

        private int exerciseId;

        private string text;
        private string description;
        private string textDifficulty = "Easy";
        private string _starFavorite = "☆";

        private bool favorite = false;

        public int ExerciseId
        {
            get
            {
                return exerciseId;
            }
            set
            {
                exerciseId = value;
                LoadItemId(value);
            }
        }

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

        public EditExerciseViewModel()
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

        public async void LoadItemId(int exerciseId)
        {
            try
            {
                GetExercise = await exerciseDatabase.GetExerciseAsync(exerciseId);
                Text = GetExercise.Name;
                Image = GetExercise.Source.Replace("File: ", "");
                Description = GetExercise.Description;
                TextDifficulty = GetExercise.Difficulty;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
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
            try
            {
                GetExercise.Name = Text;
                GetExercise.Description = Description;
                GetExercise.DateTime = Date;
                if (takeImage != null)
                    GetExercise.Source = imageStorage.SourceImage(takeImage);
                else
                    GetExercise.Source = imgSrc.ToString();
                GetExercise.Difficulty = textDifficulty;
                GetExercise.Favorite = favorite;
                GetExercise.ColorDifficulty = ColorDifficultyCommand(textDifficulty);
                await exerciseDatabase.SaveExerciseAsync(GetExercise);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            try
            {
                await Shell.Current.GoToAsync($"{nameof(CheckBodyPartsExercisePage)}?{nameof(BodyPartsExerciseViewModel.ExerciseId)}={exerciseId}");
            }
            catch (Exception e)
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
        private string ColorDifficultyCommand(string difficulty)
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

        private async void OnImageLarger()
        {
            try
            {
                await Shell.Current.GoToAsync($"{nameof(LargerImagePage)}?{nameof(LargerImageViewModel.ImageId)}={imageStorage.SourceImage(takeImage)}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
