using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sport.Models;
using Sport.Services;
using Sport.Views;
using Xamarin.Forms;

namespace Sport.ViewModels
{
    public class NewWorkoutViewModel : BaseViewModel
    {
        private StackLayout GetStackLayout { get; set; }

        private ExerciseDatabase ExerciseDatabase { get; set; }
        private WorkoutDatabase WorkoutDatabase { get; set; }

        public Command AddExerciseCommand { get; }
        public Command SaveCommand { get; }
        public Command CancelCommand { get; }
        public Command DifficultyCommand { get; }

        public Command DeleteFrameExerciseCommand { get; }

        private List<Exercise> ListExercise { get; set; }
        private Random GetRandom { get; set; }

        private List<string> IdExerciseWorkout { get; set; }
        private List<string> RepetitionExerciseWorkout { get; set; }

        private int _indexListExercise;

        private string _name;
        private string _description;
        private string _textDifficulty = "Easy";
        private string _rounds;
        private string _break;

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
        public string Rounds
        {
            get => _rounds;
            set => SetProperty(ref _rounds, value);
        }
        public string Break
        {
            get => _break;
            set => SetProperty(ref _break, value);
        }

        public string TextDifficulty
        {
            get => _textDifficulty;
            set => SetProperty(ref _textDifficulty, value);
        }

        public NewWorkoutViewModel(StackLayout stackLayout)
        {
            GetStackLayout = stackLayout;
            GetRandom = new Random();
            ExerciseDatabase = new ExerciseDatabase();
            WorkoutDatabase = new WorkoutDatabase();
            AddExerciseCommand = new Command(OnAddExercise);
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            DifficultyCommand = new Command(OnDifficulty);
            DeleteFrameExerciseCommand = new Command(OnDeleteExercise);
            ListExercise = new List<Exercise>();
            IdExerciseWorkout = new List<string>();
            RepetitionExerciseWorkout = new List<string>();
            _indexListExercise = -1;
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        public void OnAppearing()
        {
            if (App.ExerciseIdWorkout != null)
            {
                try
                {
                    _indexListExercise++;
                    ListExercise.Add(App.ExerciseIdWorkout);
                    IdExerciseWorkout.Add(App.ExerciseIdWorkout.Id.ToString());
                    RepetitionExerciseWorkout.Add(App.Répetitions.ToString());
                    AddFrame(_indexListExercise);
                }
                catch (Exception e)
                { 
                    Console.WriteLine(e);
                }
            }
        }

        private async void OnAddExercise()
        {
            await Shell.Current.GoToAsync(nameof(ListOfExerciseWorkout));
        }

        private void OnDeleteExercise(object sender)
        {
            var frame = sender as Frame;
            ListExercise.RemoveAt(int.Parse(frame.AutomationId));
            IdExerciseWorkout.RemoveAt(int.Parse(frame.AutomationId));
            RepetitionExerciseWorkout.RemoveAt(int.Parse(frame.AutomationId));
            _indexListExercise--;
            GetStackLayout.Children.Remove(frame);
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(_name)
                && !String.IsNullOrWhiteSpace(_description)
                && !String.IsNullOrWhiteSpace(_rounds)
                && !String.IsNullOrWhiteSpace(_break);
        }

        private void OnCancel()
        {
            BackToPage();
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

        private async void OnSave()
        {

            await WorkoutDatabase.SaveWorkoutAsync(new Workout
            {
                Name = _name,
                Description = _description,
                NameImage = _name[0].ToString(),
                NbRounds = ConvertStringToInt(_rounds),
                TimeBreak = ConvertStringToInt(_break),
                IdExerciseRound = SetterString(IdExerciseWorkout),
                RepetitionExerciceRound = SetterString(RepetitionExerciseWorkout),
                BodyPartsExercise = BackPartsExerciseWorkout(),
                NbExercise = ListExercise.Count,
                Difficulty = _textDifficulty,
                IsEdit = true
            });
            BackToPage();
        }

        private string SetterString(List<string> listString)
        {
            var result = "";
            try
            {
                result = String.Join("|", listString.ToArray());
                result.Substring(0, result.Length - 1);
            }
            catch
            {
                result = "";
            };
            return result;
        }

        private int ConvertStringToInt(string value)
        {
            int result;
            try
            {
                result = int.Parse(value);
            }
            catch
            {
                result = 0;
            }

            return result;
        }

        private string BackPartsExerciseWorkout()
        {
            string result = "";
            try
            {
                List<string> ListBodyPartWorkout = new List<string>();

                foreach (var exercise in ListExercise)
                {
                    string[] listBodyParts = exercise.BodyPartsExercise.Replace(" ", "").Split('|');
                    foreach (string part in listBodyParts)
                        ListBodyPartWorkout.Add(part);
                }

                HashSet<string> hashWithoutDuplicates = new HashSet<string>(ListBodyPartWorkout);
                List<string> listWithoutDuplicates = hashWithoutDuplicates.ToList();

                result = String.Join("|", listWithoutDuplicates.ToArray());
                result.Substring(0, result.Length - 1);
            }
            catch
            {

            }

            return result;
        }

        private async void BackToPage()
        {
            App.ExerciseIdWorkout = null;
            App.Répetitions = 0;
            await Shell.Current.GoToAsync("..");
        }

        private void AddFrame(int indexListExercise)
        {
            try
            {
                Frame frame = new Frame() { AutomationId = indexListExercise.ToString()};
                StackLayout stackLayout = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Children =
                    {
                        new Image { Source = App.ExerciseIdWorkout.Source, HeightRequest = 50, WidthRequest = 50 },
                        new StackLayout
                        {
                            Children =
                            {
                                new Label { Text = App.ExerciseIdWorkout.Name, FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                                    VerticalOptions = LayoutOptions.CenterAndExpand, VerticalTextAlignment = TextAlignment.Center },
                                new Label { Text = $"x{App.Répetitions} reps", FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                                    VerticalOptions = LayoutOptions.CenterAndExpand, VerticalTextAlignment = TextAlignment.Center}
                            }
                        },
                        new Button { Text = "delete", CornerRadius= 10, HorizontalOptions = LayoutOptions.EndAndExpand, 
                            BackgroundColor = Color.Red, Opacity = 0.5, TextColor = Color.White, Command=DeleteFrameExerciseCommand, CommandParameter = frame}
                    }
                };
                frame.Content = stackLayout;
                GetStackLayout.Children.Add(frame);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
