using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

using Sport.Models;
using Sport.Services;
using Sport.Views;

namespace Sport.ViewModels
{
    [QueryProperty(nameof(TrainingDate), nameof(TrainingDate))]
    public class TrainingDayDetailViewModel : BaseViewModel
    {
        private TrainingDayDatabase TrainingDayDatabase { get; set; }

        private WorkoutDatabase WorkoutDatabase { get; set; }

        private Workout GetWorkout { get; set; }

        public Command ViewWorkoutCommand { get; }
        public Command OKCommand { get; }

        private string _trainingDate;
        private string _date;
        private string _nameWorkout;
        private string _difficultyWorkout;
        private string _nameImageWorkout;
        private string _timeTraining;

        private int _exercisePerformed;
        private int _roundPerformed;
        private int _repetititonPerformed;

        private bool _isRelaxation;
        private bool _isWork;

        public string TrainingDate
        {
            get
            {
                return _trainingDate;
            }
            set
            {
                _trainingDate = value;
                LoadItemId(value);
            }
        }

        public string Date
        {
            get => _date;
            set => SetProperty(ref _date, value);
        }

        public bool Relaxation
        {
            get => _isRelaxation;
            set => SetProperty(ref _isRelaxation, value);
        }

        public bool Worked
        {
            get => _isWork;
            set => SetProperty(ref _isWork, value);
        }

        public string NameWorkout
        {
            get => _nameWorkout;
            set => SetProperty(ref _nameWorkout, value);
        }

        public string DifficultyWorkout
        {
            get => _difficultyWorkout;
            set => SetProperty(ref _difficultyWorkout, value);
        }

        public string NameImageWorkout
        {
            get => _nameImageWorkout;
            set => SetProperty(ref _nameImageWorkout, value);
        }

        public int RoundPerformed
        {
            get => _roundPerformed;
            set => SetProperty(ref _roundPerformed, value);
        }

        public int ExercisePerformed
        {
            get => _exercisePerformed;
            set => SetProperty(ref _exercisePerformed, value);
        }

        public string TimeTraining
        {
            get => _timeTraining;
            set => SetProperty(ref _timeTraining, value);
        }

        public int RepetitionPerformed
        {
            get => _repetititonPerformed;
            set => SetProperty(ref _repetititonPerformed, value);
        }

        public TrainingDayDetailViewModel()
        {
            TrainingDayDatabase = new TrainingDayDatabase();
            WorkoutDatabase = new WorkoutDatabase();
            OKCommand = new Command(OnOk);
            ViewWorkoutCommand = new Command(OnViewWorkout);
            GetWorkout = new Workout();
        }

        private async void LoadItemId(string dateTime)
        {
            TrainingDay trainingOfDay = new TrainingDay();
            try
            {
                trainingOfDay = await TrainingDayDatabase.GetTrainingDayAsync(dateTime);
                Date = trainingOfDay.DateTime;
                Worked = !trainingOfDay.IsRelaxation;
                Relaxation = trainingOfDay.IsRelaxation;
                TimeTraining = trainingOfDay.TimeTraining;
                RoundPerformed = trainingOfDay.RoundPerformed;
                ExercisePerformed = trainingOfDay.ExercisePerformed;
                RepetitionPerformed = trainingOfDay.RepetitionPerformed;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            try
            {
                GetWorkout = await WorkoutDatabase.GetWorkoutAsync(trainingOfDay.IdWorkout);
                if (GetWorkout == null)
                    NameWorkout = "The workout has been deleted";
                else
                {
                    NameWorkout = GetWorkout.Name;
                    DifficultyWorkout = GetWorkout.Difficulty;
                    NameImageWorkout = GetWorkout.NameImage;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private async void OnOk()
        {
            await Shell.Current.GoToAsync("..");
        }

        private async void OnViewWorkout()
        {
            if(GetWorkout != null)
                await Shell.Current.GoToAsync($"{nameof(WorkoutDetailPage)}?{nameof(WorkoutDetailViewModel.WorkoutId)}={GetWorkout.Id}");
        }
    }

}
