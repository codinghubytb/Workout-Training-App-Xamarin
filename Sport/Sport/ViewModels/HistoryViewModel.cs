using System;
using System.Collections.Generic;
using System.Text;

using Sport.Services;
using Sport.Models;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Xamarin.Forms;
using Sport.Views;

namespace Sport.ViewModels
{
    public class HistoryViewModel : BaseViewModel
    {
        private TrainingDayDatabase TrainingDayDatabase { get; set; }

        private TrainingDay _selectedTraining;
        public ObservableCollection<TrainingDay> TrainingDayItem { get; }
        public Command LoadItemsCommand { get; }
        public Command ShowTrainingCommand { get; }
        public Command<TrainingDay> TrainingTapped { get; }
        public Command CancelCommand { get; }

        public TrainingDay SelectedTraining
        {
            get => _selectedTraining;
            set
            {
                SetProperty(ref _selectedTraining, value);
                OnTrainingSelected(value);
            }
        }

        public HistoryViewModel()
        {
            TrainingDayDatabase = new TrainingDayDatabase();
            TrainingDayItem = new ObservableCollection<TrainingDay>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            TrainingTapped = new Command<TrainingDay>(OnTrainingSelected);
            CancelCommand = new Command(OnCancel);
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                TrainingDayItem.Clear();
                var items = await TrainingDayDatabase.GetTrainingDayAsync();
                Console.WriteLine(items);
                foreach (var item in items)
                {
                    TrainingDayItem.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        async void OnTrainingSelected(TrainingDay training)
        {
            if (training == null)
                return; 
            await Shell.Current.GoToAsync($"{nameof(TrainingDayDetailPage)}?{nameof(TrainingDayDetailViewModel.TrainingDate)}={$"{training.Month}/{training.Day}/{training.Year}"}");

        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedTraining = null;
        }

        private async void OnCancel()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
