using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Sport.ViewModels;

namespace Sport.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewWorkoutPage : ContentPage
    {
        NewWorkoutViewModel _NewWorkoutViewModel { get; set; }
        public NewWorkoutPage()
        {
            InitializeComponent();
            _NewWorkoutViewModel = new NewWorkoutViewModel(stackExercice);
            BindingContext = _NewWorkoutViewModel = new NewWorkoutViewModel(stackExercice);

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Console.WriteLine(App.ExerciseIdWorkout);
            _NewWorkoutViewModel.OnAppearing();
        }
    }
}