using Sport.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

using Sport.Models;
using System.Collections.ObjectModel;
using Sport.Services;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Sport.ViewModels
{
    public class ExerciceViewModel : BaseViewModel
    {
        private ExerciseDatabase exerciseDatabase;

        private Exercise _selectedExercise;
        private ImageStorage imageStorage { get; set; }
        public ObservableCollection<Exercise> ExercisesItem { get; }

        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<Exercise> ExerciseTapped { get; }
        public Command SearchCommand { get; }

        private bool _isSearch { get; set; }

        private string _searchText;

        public Exercise SelectedExercise
        {
            get => _selectedExercise;
            set
            {
                SetProperty(ref _selectedExercise, value);
                OnExerciseSelected(value);
            }
        }

        public string SearchText
        {
            get => _searchText;
            set => SetProperty(ref _searchText, value);
        }

        public ExerciceViewModel()
        {
            Title = "Exercise";
            _isSearch = false;
            exerciseDatabase = new ExerciseDatabase();
            imageStorage = new ImageStorage();
            ExercisesItem = new ObservableCollection<Exercise>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            AddItemCommand = new Command(OnAddItem);
            ExerciseTapped = new Command<Exercise>(OnExerciseSelected);
            SearchCommand = new Command(async () => await OnSearchExercise());

        }

        async Task ExecuteLoadItemsCommand()
        {
            if (string.IsNullOrEmpty(SearchText))
            {
                IsBusy = true;

                try
                {
                    ExercisesItem.Clear();
                    var items = await exerciseDatabase.GetExerciseAsync();
                    foreach (var item in items)
                    {
                        ExercisesItem.Add(item);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
                finally
                {
                    IsBusy = false;
                }
            }
            
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewItemPage));
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedExercise = null;
        }

        async void OnExerciseSelected(Exercise exercise)
        {
            if (exercise == null)
                return;
            await Shell.Current.GoToAsync($"{nameof(ExerciseDetailPage)}?{nameof(ExerciseDetailViewModel.ExerciseId)}={exercise.Id}");
        }

        public async Task OnSearchExercise()
        {
            IsBusy = true;
            try
            {
                ExercisesItem.Clear();
                var items = await exerciseDatabase.SearchExerciseAsync(_searchText);
                foreach (var item in items)
                {
                    ExercisesItem.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }

        }
    }
}
