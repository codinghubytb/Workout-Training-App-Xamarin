using Sport.Models;
using Sport.Services;
using Sport.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Sport.ViewModels
{
    public class WorkTrainingDayViewModel : BaseViewModel
    {
        public TrainingDayDatabase TrainingDayDatabase { get; set; }
        public WorkoutDatabase WorkoutDatabase { get; set; }
        public ObservableCollection<Workout> WorkoutItem { get; }
        public Command LoadItemsCommand { get; }
        public Command CancelCommand { get; }
        public Command DayOffCommand { get; }
        public Command<Workout> WorkoutTapped { get; }

        public Workout _selectedWorkout;

        public Workout SelectedWorkout
        {
            get => _selectedWorkout;
            set
            {
                SetProperty(ref _selectedWorkout, value);
                OnWorkoutSelected(value);
            }
        }

        public WorkTrainingDayViewModel()
        {
            TrainingDayDatabase = new TrainingDayDatabase();
            WorkoutItem = new ObservableCollection<Workout>();
            WorkoutDatabase = new WorkoutDatabase();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            WorkoutTapped = new Command<Workout>(OnWorkoutSelected);
            CancelCommand = new Command(OnCancel);
            DayOffCommand = new Command(OnSaveDayOff);
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
            await Shell.Current.GoToAsync($"{nameof(DayTrainingPage)}?{nameof(DayTrainingViewModel.WorkoutId)}={workout.Id}");
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }

        private async void OnCancel()
        {
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSaveDayOff()
        {
            await TrainingDayDatabase.SaveTrainingDayAsync(
                new TrainingDay
                {
                    DateTime = $"{DateTime.Now.Month}/{DateTime.Now.Day}/{DateTime.Now.Year}",
                    Day = DateTime.Now.Day,
                    Month = DateTime.Now.Month,
                    Year = DateTime.Now.Year,
                    IsRelaxation = true,
                    TimeTraining = null,
                    IdWorkout = -1,
                    RoundPerformed = -1,
                    ExercisePerformed = -1,
                    RepetitionPerformed = -1
                }
            );
            await Shell.Current.GoToAsync("..");
        }
    }
        
}
