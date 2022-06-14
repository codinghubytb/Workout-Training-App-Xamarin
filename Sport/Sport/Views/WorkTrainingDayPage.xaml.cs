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
    public partial class WorkTrainingDayPage : ContentPage
    {
        WorkTrainingDayViewModel _WorkTrainingDayViewModel { get; set; }
        public WorkTrainingDayPage()
        {
            InitializeComponent();
            _WorkTrainingDayViewModel = new WorkTrainingDayViewModel();
            BindingContext = new WorkTrainingDayViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            refresh.IsRefreshing = true;
            _WorkTrainingDayViewModel.OnAppearing();

        }
    }
}