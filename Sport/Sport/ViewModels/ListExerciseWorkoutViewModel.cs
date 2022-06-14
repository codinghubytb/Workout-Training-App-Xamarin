using Sport.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

using Sport.Services;
using Xamarin.Forms;
using System.Diagnostics;

namespace Sport.ViewModels
{
    class ListExerciseWorkoutViewModel : BaseViewModel
    {
        public ExerciseDatabase exerciseDatabase { get; }
        public ObservableCollection<Exercise> ExercisesItem { get; }

        public Command LoadItemsCommand { get; }

        public ListExerciseWorkoutViewModel()
        {
            Title = "List Of Exercise";
            exerciseDatabase = new ExerciseDatabase();
            ExercisesItem = new ObservableCollection<Exercise>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }


        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;
            try
            {
                ExercisesItem.Clear();
                var items = await exerciseDatabase.GetExerciseAsync();
                foreach (var item in items)
                {
                    ExercisesItem.Add(item);
                    Console.WriteLine(item.Source);
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

        public void OnAppearing()
        {
            IsBusy = true;
        }
    }
}
