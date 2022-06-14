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
    public partial class WorkoutPage : ContentPage
    {
        WorkoutViewModel _WorkoutViewModel { get; set; }
        public WorkoutPage()
        {
            InitializeComponent();
            _WorkoutViewModel = new WorkoutViewModel();
            BindingContext = _WorkoutViewModel = new WorkoutViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _WorkoutViewModel.OnAppearing();
        }

        /// <summary>
        /// Search List Workout with a text
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var text = sender as SearchBar;
            if (string.IsNullOrEmpty(text.Text))
                OnAppearing();
            else
                await _WorkoutViewModel.OnSearchWorkout();
        }
    }
}