using Sport.Models;
using Sport.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace Sport.ViewModels
{
    [QueryProperty(nameof(WorkoutId), nameof(WorkoutId))]
    public class WorkoutDetailViewModel : BaseViewModel
    {
        private StackLayout GetStackExerciseRound;

        private StackLayout GetStackBodyPart;

        private WorkoutDatabase WorkoutDatabase;

        private ExerciseDatabase ExerciseDatabase;

        private Workout GetWorkout;

        public Command DeleteCommand { get; }
        public Command OKCommand { get; }

        private int _workoutId;
        private int _rounds;
        private int _break;
        private int _nbExerciseRound;

        private string _name;
        private string _description;

        private bool _isEdit;

        public int WorkoutId
        {
            get
            {
                return _workoutId;
            }
            set
            {
                _workoutId = value;
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

        public int Rounds
        {
            get => _rounds;
            set => SetProperty(ref _rounds, value);
        }

        public int Break
        {
            get => _break;
            set => SetProperty(ref _break, value);
        }

        public int NbExerciseRound
        {
            get => _nbExerciseRound;
            set => SetProperty(ref _nbExerciseRound, value);
        }

        public bool IsEdit
        {
            get => _isEdit;
            set => SetProperty(ref _isEdit, value);
        }

        public WorkoutDetailViewModel(StackLayout stackExerciseRound, StackLayout stackBtnBackParts)
        {
            GetStackExerciseRound = stackExerciseRound;
            GetStackBodyPart = stackBtnBackParts;
            WorkoutDatabase = new WorkoutDatabase();
            ExerciseDatabase = new ExerciseDatabase();
            DeleteCommand = new Command(OnDelete);
            OKCommand = new Command(OnOk);
        } 

        public async void LoadItemId(int exerciseId)
        {
            try
            {
                GetWorkout = await WorkoutDatabase.GetWorkoutAsync(_workoutId);
                Name = GetWorkout.Name;
                Description = GetWorkout.Description;
                Rounds = GetWorkout.NbRounds;
                Break = GetWorkout.TimeBreak;
                NbExerciseRound = GetWorkout.NbExercise;
                IsEdit = GetWorkout.IsEdit;
                PutFrameExerciseWorkout(GetWorkout.IdExerciseRound.Split('|'));
                PutButtonBodyPartWorkout(GetWorkout.BodyPartsExercise);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private async void OnDelete()
        {
            await WorkoutDatabase.DeleteWorkoutAsync(GetWorkout);
            await Shell.Current.GoToAsync("..");
        }

        private async void OnOk()
        {
            await Shell.Current.GoToAsync("..");
        }

        private async void PutFrameExerciseWorkout(string[] listIdExerciseRoundWorkout)
        {
            for (int i = 0; i < listIdExerciseRoundWorkout.Length; i++)
            {
                try
                {
                    int id = int.Parse(listIdExerciseRoundWorkout[i]);
                    string rep = GetWorkout.RepetitionExerciceRound.Split('|')[i];
                    Exercise exercise = await ExerciseDatabase.GetExerciseAsync(id);
                    GetStackExerciseRound.Children.Add(CreateFrameExercise(exercise, rep));
                }
                catch(Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        private Frame CreateFrameExercise(Exercise exercise, string rep)
        {
            
            Frame frame = new Frame
            {
                CornerRadius = 10,
                BackgroundColor = Color.WhiteSmoke
            };
            StackLayout stackLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children =
                {
                    new Image{ Source = exercise.Source, WidthRequest = 50, HeightRequest = 50},
                    new StackLayout
                    {
                        Margin = new Thickness(20,0,0,0),
                        Children =
                        {
                            new Label{Text = exercise.Name, TextColor= Color.Black, FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label))},
                            new Label{Text = $"x{rep} reps", TextColor = Color.Gray },
                        }
                    }
                }
            };
            frame.Content = stackLayout;
            return frame;
        }

        private void PutButtonBodyPartWorkout(string bodyPartsWorkout)
        {
            string[] listBodyParts = bodyPartsWorkout.Split('|');
            List<string> bodyParts = new List<string>();

            for (int i = 0; i < listBodyParts.Length - 1; i++)
            {
                bodyParts.Add(listBodyParts[i]);
            }
            foreach (string part in bodyParts)
            { 
                GetStackBodyPart.Children.Add(CreateButtonBodyPart(part));
            }
        }

        private Button CreateButtonBodyPart(string text)
        {
            return new Button { Text = text, TextColor = Color.White, BorderColor = Color.White, BorderWidth = 1.5, BackgroundColor = Color.Black };
        }

    }
}
