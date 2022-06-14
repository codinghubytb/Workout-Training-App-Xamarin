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
    public partial class HistoryPage : ContentPage
    {
        HistoryViewModel _historyViewModel = new HistoryViewModel();
        public HistoryPage()
        {
            InitializeComponent();
            BindingContext = _historyViewModel = new HistoryViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            refresh.IsRefreshing = true;
            _historyViewModel.OnAppearing();
        }
    }
}