using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

using Sport.Services;
using Sport.Models;
using Sport.Views;

namespace Sport.ViewModels
{
    [QueryProperty(nameof(ExerciseId), nameof(ExerciseId))]
    public class ExerciseDetailViewModel : BaseViewModel
    {
        private ExerciseDatabase exerciseDatabase;

        public ImageStorage imageStorage;

        private Exercise GetExercise;
        public Command OKCommand { get; }
        public Command EditCommand { get; }

        private ImageSource _imageSource;

        public int Id { get; set; }
        private int exerciseId;

        private string _name;
        private string _description;
        private string _date;
        private string _textDifficulty;
        private string _favorite;
        private string _bodyParts;
        private string _colorDifficulty;

        private bool _isEdit;

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

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public string Date
        {
            get => _date;
            set => SetProperty(ref _date, value);
        }

        public string TextDifficulty
        {
            get => _textDifficulty;
            set => SetProperty(ref _textDifficulty, value);
        }

        public string ColorDifficulty
        {
            get => _colorDifficulty;
            set => SetProperty(ref _colorDifficulty, value);
        }

        public string Favorite
        {
            get => _favorite;
            set => SetProperty(ref _favorite, value);
        }

        public bool isEdit
        {
            get => _isEdit;
            set => SetProperty(ref _isEdit, value);
        }

        public string BodyPartsWork
        {
            get => _bodyParts;
            set => SetProperty(ref _bodyParts, value); 
        }
        public ImageSource Image
        {
            get => _imageSource;
            set => SetProperty(ref _imageSource, value);
        }

        public ExerciseDetailViewModel()
        {
            exerciseDatabase = new ExerciseDatabase();
            imageStorage = new ImageStorage();
            OKCommand = new Command(OnOk);
            EditCommand = new Command(OnEdit);
        }

        public async void LoadItemId(int exerciseId)
        {
            try
            {
                GetExercise = await exerciseDatabase.GetExerciseAsync(exerciseId);
                Id = GetExercise.Id;
                Name = GetExercise.Name;
                Date = GetExercise.DateTime.ToString();
                Image = GetExercise.Source.Replace("File: ", "");
                Description = GetExercise.Description;
                TextDifficulty = GetExercise.Difficulty;
                Favorite = GetFavoriteStar(GetExercise.Favorite);
                BodyPartsWork = GetExercise.BodyPartsExercise;
                ColorDifficulty = GetExercise.ColorDifficulty;
                isEdit = GetExercise.isEdit;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        private string GetFavoriteStar(bool favorite)
        {
            if (favorite)
                return "★";
            else
                return "☆";
        }

        private async void OnOk()
        {
            await Shell.Current.GoToAsync("..");
        }

        private async void OnEdit()
        {
            await Shell.Current.GoToAsync($"{nameof(EditExercisePage)}?{nameof(EditExerciseViewModel.ExerciseId)}={exerciseId}");
        }
    }
}
