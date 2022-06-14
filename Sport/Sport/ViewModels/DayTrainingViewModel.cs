using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

using Sport.Models;
using Sport.Services;

using Sport.Views;

namespace Sport.ViewModels
{
    [QueryProperty(nameof(WorkoutId), nameof(WorkoutId))]
    public class DayTrainingViewModel : BaseViewModel
    {
        private WorkoutDatabase WorkoutDatabase;

        private TrainingDayDatabase TrainingDayDatabase;

        private ExerciseDatabase ExerciseDatabase;

        public Command StartCommand { get; }
        public Command CancelCommand { get; }
        public Command LeftWorkoutCommand { get; }
        public Command NextCommand { get; }

        private string[] IdExerciseWorkout { get; set; }
        private string[] RepetitionExerciseWorkout { get; set; }

        private int _workoutId;
        private int _indexIdExercise;
        private int _nbExerciseToRound;
        private int _nbRoundFull;
        private int _nbRoundPerformed;
        private int _nbExerciseFull;
        private int _nbExercisePerformed;
        private int _nbRepetitionPerformed;
        private int _break;

        private bool _timeValue = true;

        private bool _visibleStart = true, _visibleTime = false, _visibleTraining = false, _visibleFinish = false;

        private string _timeWorkout, _timeBreak = "3";
        private string _image;
        private string _name;
        private string _repetition;
        private string _textTime;
        private string _difficulty;

        public int WorkoutId
        {
            get => _workoutId;
            set
            {
                _workoutId = value;
                LoadItemId(_workoutId);
            }
        }

        public bool VisibleStart
        {
            get => _visibleStart;
            set => SetProperty(ref _visibleStart, value);
        }

        public bool VisibleTime
        {
            get => _visibleTime;
            set => SetProperty(ref _visibleTime, value);
        }

        public bool VisibleTraining
        {
            get => _visibleTraining;
            set => SetProperty(ref _visibleTraining, value);
        }

        public bool VisibleFinish
        {
            get => _visibleFinish;
            set => SetProperty(ref _visibleFinish, value);
        }

        public string Time
        {
            get => _timeBreak;
            set => SetProperty(ref _timeBreak, value);
        }

        public string Image
        {
            get => _image;
            set => SetProperty(ref _image, value);
        }
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }
        public string Repetition
        {
            get => _repetition;
            set => SetProperty(ref _repetition, value);
        }

        public string TextTime
        {
            get => _textTime;
            set => SetProperty(ref _textTime, value);
        }

        public int ExerciseFull
        {
            get => _nbExerciseFull;
            set => SetProperty(ref _nbExerciseFull, value);
        }

        public int ExercisePerformed
        {
            get => _nbExercisePerformed;
            set => SetProperty(ref _nbExercisePerformed, value);

        }

        public int RoundFull
        {
            get => _nbRoundFull;
            set => SetProperty(ref _nbRoundFull, value);
        }

        public int RoundPerformed
        {
            get => _nbRoundPerformed;
            set => SetProperty(ref _nbRoundPerformed, value);

        }

        public string TimeWorkout
        {
            get => _timeWorkout;
            set => SetProperty(ref _timeWorkout, value);
        }

        public string Difficulty
        {
            get => _difficulty;
            set => SetProperty(ref _difficulty, value);
        }

        public DayTrainingViewModel()
        {
            TrainingDayDatabase = new TrainingDayDatabase();
            WorkoutDatabase = new WorkoutDatabase();
            ExerciseDatabase = new ExerciseDatabase();
            _indexIdExercise = 0;
            _textTime = "Start in ";
            RoundPerformed = 0;
            ExercisePerformed = 1;
            _nbRepetitionPerformed = 0;
            StartTimeWorkout();
            StartCommand = new Command(OnStart);
            CancelCommand = new Command(OnCancel);
            NextCommand = new Command(OnNext);
            LeftWorkoutCommand = new Command(OnLeftWorkout);
        }

        public async void LoadItemId(int workoutId)
        {
            try
            {
                Workout workout = await WorkoutDatabase.GetWorkoutAsync(workoutId);
                IdExerciseWorkout = workout.IdExerciseRound.Split('|');
                RepetitionExerciseWorkout = workout.RepetitionExerciceRound.Split('|');
                _nbExerciseToRound = workout.NbExercise;
                ExerciseFull = _nbExerciseToRound * workout.NbRounds;
                RoundFull = workout.NbRounds;
                _break = workout.TimeBreak;
                Difficulty = workout.Difficulty;

                PutExerciseWorkout(_indexIdExercise);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private async void PutExerciseWorkout(int value)
        {
            try
            {
                Exercise exercise = await ExerciseDatabase.GetExerciseAsync(int.Parse(IdExerciseWorkout[value]));
                Name = exercise.Name;
                Image = exercise.Source;
                Repetition = $"x{RepetitionExerciseWorkout[_indexIdExercise]} reps";
                _nbRepetitionPerformed += int.Parse(RepetitionExerciseWorkout[_indexIdExercise]);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private async void OnCancel(object obj)
        {
            await Shell.Current.GoToAsync("..");
        }

        private void OnStart(object obj)
        {
            VisibleElement(true, true, false, false);
            TimeCommand(3);
        }

        private void OnNext()
        {
            _indexIdExercise++;
            ExercisePerformed++;
            if (_indexIdExercise >= _nbExerciseToRound )
            {
                _indexIdExercise = 0;
                RoundPerformed++;
                TextTime = "Pause Round";
                if(_nbRoundPerformed <= _nbRoundFull-1)
                    TimeCommand(_break);
            }

            if(_nbExercisePerformed <= _nbExerciseFull && _nbRoundPerformed <= _nbRoundFull)
                PutExerciseWorkout(_indexIdExercise);
            else
            {
                StopTimeWorkout();
                VisibleElement(false, false, false, true);
            }

        }

        private void VisibleElement(bool start, bool time, bool training, bool finish)
        {
            VisibleStart = start;
            VisibleTime = time;
            VisibleTraining = training;
            VisibleFinish = finish;
        }

        private void TimeCommand(int valueTime, bool value=false)
        {
            bool result = true;
            Time = valueTime.ToString();

            VisibleElement(false, true, false, false);

            Device.StartTimer(new TimeSpan(0,0,0,1), () =>
            {
                valueTime--;

                Time = valueTime.ToString();

                if (valueTime <= 0)
                {
                    result = !result;
                    VisibleElement(false, false, true, false);

                    if (value)
                        PutExerciseWorkout(_indexIdExercise);
                }
                return result;
            });
        }

        private void StartTimeWorkout()
        {
            int hour =0, minute=0, second =0;
            Device.StartTimer(new TimeSpan(0,0,1), () =>
            {
                second++;
                if (second >= 60)
                {
                    minute++;
                    second = 0;
                }
                if(minute >= 60)
                    hour++;

                TimeWorkout = $"{hour}:{minute}:{second}";
                return _timeValue;
            });
        }

        private void StopTimeWorkout()
        {
            _timeValue = false;
        }

        private async void OnLeftWorkout()
        {
            await TrainingDayDatabase.SaveTrainingDayAsync(
                new TrainingDay
                {
                    DateTime = $"{DateTime.Now.Month}/{DateTime.Now.Day}/{DateTime.Now.Year}",
                    Day = DateTime.Now.Day,
                    Month = DateTime.Now.Month,
                    Year = DateTime.Now.Year,
                    IdWorkout = WorkoutId,
                    IsRelaxation = false,
                    TimeTraining = _timeWorkout,
                    RoundPerformed = _nbRoundPerformed,
                    ExercisePerformed = _nbExerciseFull,
                    RepetitionPerformed = _nbRepetitionPerformed
                }
            );
            await Shell.Current.GoToAsync("../..");
        }
    }
}
