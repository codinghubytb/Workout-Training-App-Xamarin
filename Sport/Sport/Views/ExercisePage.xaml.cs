using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Sport.Services;
using Sport.ViewModels;
using System.Collections.ObjectModel;
using Sport.Models;

namespace Sport.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExercisePage : ContentPage
    {
        ExerciceViewModel _exerciceViewModel { get; set; }
        public ExercisePage()
        {
            InitializeComponent();

            BindingContext = _exerciceViewModel = new ExerciceViewModel();
        }

        
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _exerciceViewModel.OnAppearing();
        }

        /// <summary>
        /// Search List Exercise with a text
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var text = sender as SearchBar;
            if (string.IsNullOrEmpty(text.Text))
                OnAppearing();
            else
                await _exerciceViewModel.OnSearchExercise();
        }
    }
}