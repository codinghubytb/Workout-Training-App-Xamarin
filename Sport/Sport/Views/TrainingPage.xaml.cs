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
    public partial class TrainingPage : ContentPage
    {
        TrainingViewModel _trainingViewModel;
        public TrainingPage()
        {
            InitializeComponent();
            _trainingViewModel = new TrainingViewModel(StackDayTraining);
            BindingContext = _trainingViewModel = new TrainingViewModel(StackDayTraining);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _trainingViewModel.OnAppearing();
        }
    }
}