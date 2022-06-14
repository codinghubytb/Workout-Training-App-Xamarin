using Sport.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

using Sport.Services;
using Sport.Models;

namespace Sport.ViewModels
{
    public class TrainingViewModel : BaseViewModel
    {
        private StackLayout _stackDayTraining;
        public TrainingDayDatabase TrainingDayDatabase { get; set; }
        public Command HistoryCommand { get; }
        public Command ViewTrainingDate { get; }
        public Command WorkTrainingDay { get; }

        private string _date = DateTime.Now.Date.ToString();

        private string _day = $"Day {DateTime.Now.Day}";

        public string TextDay
        {
            get => _day;
            set => SetProperty(ref _day, value);
        }
        public string Date
        {
            get => _date;
            set => SetProperty(ref _date, value);
        }


        public TrainingViewModel(StackLayout stackDay)
        {
            Title = "Training";
            _stackDayTraining = stackDay;
            HistoryCommand = new Command(OnHistory);
            ViewTrainingDate = new Command(OnViewTrainingDay);
            WorkTrainingDay = new Command(OnWorkDay);
            if(_stackDayTraining.Children.Count == 0)
                CreateTrainingDay();
        }

        public void OnAppearing()
        {
            if(DateTime.Now.Day.ToString() != _day.Replace("Day ",""))
            {
                Date = DateTime.Now.Date.ToString();
                TextDay = $"Day {DateTime.Now.Day}";
            }

        }

        private async void OnHistory(object obj)
        {
            await Shell.Current.GoToAsync(nameof(HistoryPage));
        }

        private async void OnViewTrainingDay(object sender)
        {
            var btn = sender as Button;
            await Shell.Current.GoToAsync($"{nameof(TrainingDayDetailPage)}?{nameof(TrainingDayDetailViewModel.TrainingDate)}={btn.AutomationId}");
        }

        private async void OnWorkDay()
        {
                TrainingDayDatabase trainingDayDatabase = new TrainingDayDatabase();
                TrainingDay trainingDay = await trainingDayDatabase.GetTrainingDayAsync($"{DateTime.Now.Month}/{DateTime.Now.Day}/{DateTime.Now.Year}");
                if(trainingDay == null )
                    await Shell.Current.GoToAsync(nameof(WorkTrainingDayPage));
                else
                    await Shell.Current.GoToAsync($"{nameof(TrainingDayDetailPage)}?{nameof(TrainingDayDetailViewModel.TrainingDate)}={$"{DateTime.Now.Month}/{DateTime.Now.Day}/{DateTime.Now.Year}"}");
        }

        private void CreateTrainingDay()
        {
            for(int i= 1; i<DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month)+1; i++)
            {
                if (i != DateTime.Now.Day)
                {
                    Frame frameDay = new Frame();

                    StackLayout stackLayout = new StackLayout() { Orientation = StackOrientation.Horizontal };

                    Label label = CreateLabel(FontAttributes.Bold, Color.Black, Color.White, Device.GetNamedSize(NamedSize.Medium, typeof(Label)), 
                        LayoutOptions.StartAndExpand, LayoutOptions.CenterAndExpand, $"Day {i}");

                    Button button = CreateButton(FontAttributes.Bold, Color.Black, 10, Color.White, Color.FromHex("fefeff"), Device.GetNamedSize(NamedSize.Medium, typeof(Button)),
                        LayoutOptions.EndAndExpand, LayoutOptions.CenterAndExpand, "Start");

                    stackLayout.Children.Add(label);

                    if (i < DateTime.Now.Day)
                    {
                        button.Text = "view your sports day";
                        button.TextColor = Color.White;
                        button.BackgroundColor = Color.FromHex("#5be9b3");
                        button.FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Button));
                        button.Command = ViewTrainingDate;
                        button.CommandParameter = button;
                        button.AutomationId = $"{DateTime.Now.Month}/{i}/{DateTime.Now.Year}";
                        stackLayout.Children.Add(button);
                    }
                    else if (i > DateTime.Now.Day)
                    {
                        button.Text = "See you seen";
                        button.TextColor = Color.Black;
                        button.BackgroundColor = Color.Transparent;
                        button.FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Button));
                        stackLayout.Children.Add(button);
                    }

                    frameDay.Content = stackLayout;
                    _stackDayTraining.Children.Add(frameDay);
                }
                   
            }
        }

        private Button CreateButton(FontAttributes fontAttributes, Color color, int cornerRadius, Color backColor, Color borderColor, double fontSize,
            LayoutOptions HorizontalLayoutOptions, LayoutOptions VerticalLayoutOptions, string text = "", double borderWidth = 1.5)
        {
            return new Button { 
                Text = text, 
                FontAttributes = fontAttributes, 
                TextColor = color, 
                BorderColor = borderColor, 
                CornerRadius = cornerRadius, 
                BackgroundColor = backColor, 
                BorderWidth = borderWidth, 
                FontSize = fontSize, 
                HorizontalOptions = HorizontalLayoutOptions, 
                VerticalOptions = VerticalLayoutOptions 
            };
        }

        private Label CreateLabel(FontAttributes fontAttributes, Color color, Color backColor, double fontSize,
            LayoutOptions HorizontalLayoutOptions, LayoutOptions VerticalLayoutOptions, string text = "")
        {
            return new Label
            {
                Text = text,
                FontAttributes = fontAttributes,
                TextColor = color,
                BackgroundColor = backColor,
                FontSize = fontSize,
                HorizontalOptions = HorizontalLayoutOptions,
                VerticalOptions = VerticalLayoutOptions
            };
        }
    }
}
