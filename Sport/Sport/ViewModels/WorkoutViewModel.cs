using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

using Sport.Views;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

using Sport.Models;
using Sport.Services;
using System.Diagnostics;

namespace Sport.ViewModels
{
    public class WorkoutViewModel : BaseViewModel
    {
        public WorkoutDatabase WorkoutDatabase { get; set; }
        public ObservableCollection<Workout> WorkoutItem { get; }

        public Workout _selectedWorkout;

        public Command CreateWorkout { get; }
        public Command LoadItemsCommand { get; }
        public Command<Workout> WorkoutTapped { get; }

        public string _searchText;

        public Workout SelectedWorkout
        {
            get => _selectedWorkout;
            set
            {
                SetProperty(ref _selectedWorkout, value);
                OnWorkoutSelected(value);
            }
        }

        public string SearchText
        {
            get => _searchText;
            set => SetProperty(ref _searchText, value);
        }


        public WorkoutViewModel()
        {
            WorkoutItem = new ObservableCollection<Workout>();
            WorkoutDatabase = new WorkoutDatabase();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            CreateWorkout = new Command(OnCreate);
            WorkoutTapped = new Command<Workout>(OnWorkoutSelected);
        }


        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                WorkoutItem.Clear();
                var items = await WorkoutDatabase.GetWorkoutAsync();
                foreach (var item in items)
                {
                    WorkoutItem.Add(item);
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

        async void OnWorkoutSelected(Workout workout)
        {
            if (workout == null)
                return;
            await Shell.Current.GoToAsync($"{nameof(WorkoutDetailPage)}?{nameof(WorkoutDetailViewModel.WorkoutId)}={workout.Id}");
        }

        private async void OnCreate()
        {
            await Shell.Current.GoToAsync(nameof(NewWorkoutPage));
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedWorkout = null;
        }

        public async Task OnSearchWorkout()
        {
            IsBusy = true;
            try
            {
                WorkoutItem.Clear();
                var items = await WorkoutDatabase.SearchWorkoutAsync(_searchText);
                foreach (var item in items)
                {
                    WorkoutItem.Add(item);
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
