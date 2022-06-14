using Sport.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

using Sport.Services;
using System.Diagnostics;

using Sport.Views;

namespace Sport.ViewModels
{
    [QueryProperty(nameof(ExerciseId), nameof(ExerciseId))]
    public class BodyPartsExerciseViewModel : BaseViewModel
    {
        private Grid grid;

        private Exercise GetExercise;

        private ExerciseDatabase exerciseDatabase;

        private BodyPartsDatabase bodyPartsDatabase;

        public Command SaveCommand { get; }

        private List<CheckBox> checkBoxes = new List<CheckBox>();

        private int exerciseId;

        public int ExerciseId
        {
            get
            {
                return exerciseId;
            }
            set
            {
                exerciseId = value;
                LoadItemId(value);
            }
        }

        public BodyPartsExerciseViewModel(Grid gridPage)
        {
            grid = gridPage;
            GetExercise = new Exercise();
            exerciseDatabase = new ExerciseDatabase();
            bodyPartsDatabase = new BodyPartsDatabase();
            SaveCommand = new Command(OnSave);
            CreateCheckBox();
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        public async void LoadItemId(int exerciseId)
        {
            try
            {
                GetExercise = await exerciseDatabase.GetExerciseAsync(exerciseId);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }

        /// <summary>
        /// Save in the table dabase Exercice
        /// </summary>
        private async void OnSave()
        {

            GetExercise.BodyPartsExercise = SelectCheckBodyPart();
            await exerciseDatabase.SaveExerciseAsync(GetExercise);

            await Shell.Current.GoToAsync(nameof(ExercisePage));
        }

        /// <summary>
        /// Select bodyparts
        /// </summary>
        /// <returns>string all bodyparts</returns>
        private string SelectCheckBodyPart() 
        {
            List<string> bodyParts = new List<string>();
            var result = "";

            foreach (var check in checkBoxes)
            {
                if (check.IsChecked)
                {
                    bodyParts.Add(check.AutomationId);
                }
            }
            if(bodyParts.Count > 0)
            {
                result = String.Join(" | ", bodyParts.ToArray());
                result.Substring(0, result.Length - 1);
            }

            return result;
        }

        /// <summary>
        /// Create CheckBox
        /// </summary>
        private async void CreateCheckBox()
        {
            int row = 0, col = 0;
            List<BodyParts> exerciseList = await bodyPartsDatabase.GetBodyPartsAsync();
            foreach (var exercise in exerciseList)
            {
                CheckBox checkBox = new CheckBox { AutomationId = exercise.Name, Color = Color.Black, HorizontalOptions = LayoutOptions.EndAndExpand };
                checkBoxes.Add(checkBox);
                Label label = new Label { Text = exercise.Name, HorizontalOptions = LayoutOptions.StartAndExpand, VerticalTextAlignment = TextAlignment.Center,
                    FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)) };
                StackLayout stackLayout = new StackLayout
                {
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Orientation = StackOrientation.Horizontal
                };
                stackLayout.Children.Add(label);
                stackLayout.Children.Add(checkBox);
                Grid.SetColumn(stackLayout, col);
                Grid.SetRow(stackLayout, row);
                grid.Children.Add(stackLayout);
                col++;
                if (col == 2)
                {
                    row++;
                    col = 0;
                }
            }
        }
    }
}
