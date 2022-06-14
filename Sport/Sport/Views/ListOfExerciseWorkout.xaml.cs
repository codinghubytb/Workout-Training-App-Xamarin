using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Sport.ViewModels;
using Sport.Services;

namespace Sport.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListOfExerciseWorkout : ContentPage
    {
        ListExerciseWorkoutViewModel _ListExerciseWorkoutViewModel = new ListExerciseWorkoutViewModel();
        ExerciseDatabase GetExerciseDatabase;
        public ListOfExerciseWorkout()
        {
            InitializeComponent();
            GetExerciseDatabase = new ExerciseDatabase();
            BindingContext = _ListExerciseWorkoutViewModel = new ListExerciseWorkoutViewModel(); 
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _ListExerciseWorkoutViewModel.OnAppearing();
        }

        /// <summary>
        /// Display DisplayAlert for select the exercise 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [Obsolete]
        private async void BtnSelectExercise_Clicked(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            App.ExerciseIdWorkout = await GetExerciseDatabase.GetExerciseAsync(int.Parse(btn.AutomationId));
            string result = await DisplayPromptAsync("Repetitions", "Number of repetitions ?", "OK","Cancel", "10", 9, Keyboard.Numeric);
            if (!string.IsNullOrEmpty(result))
            {
                try
                {
                    App.Répetitions = int.Parse(result);
                }
                catch
                {
                    App.Répetitions = 0;
                }
                OnBackButtonPressed();
            }  
        }
    }
}