using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Sport.Models;
using Sport.Services;

using Sport.ViewModels;

namespace Sport.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CheckBodyPartsExercisePage : ContentPage
    {
        public CheckBodyPartsExercisePage()
        {
            InitializeComponent();
            BindingContext = new BodyPartsExerciseViewModel(grid);
        }

        protected override bool OnBackButtonPressed()
        {
            return base.OnBackButtonPressed();
        }
    }
}