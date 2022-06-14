using Sport.ViewModels;
using Sport.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Sport
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute(nameof(TrainingPage), typeof(TrainingPage));
            Routing.RegisterRoute(nameof(HistoryPage), typeof(HistoryPage));
            Routing.RegisterRoute(nameof(ExerciseDetailPage), typeof(ExerciseDetailPage));
            Routing.RegisterRoute(nameof(CheckBodyPartsExercisePage), typeof(CheckBodyPartsExercisePage));
            Routing.RegisterRoute(nameof(LargerImagePage), typeof(LargerImagePage));
            Routing.RegisterRoute(nameof(EditExercisePage), typeof(EditExercisePage));
            Routing.RegisterRoute(nameof(ExercisePage), typeof(ExercisePage));
            Routing.RegisterRoute(nameof(NewWorkoutPage), typeof(NewWorkoutPage));
            Routing.RegisterRoute(nameof(ListOfExerciseWorkout), typeof(ListOfExerciseWorkout));
            Routing.RegisterRoute(nameof(WorkoutDetailPage), typeof(WorkoutDetailPage));
            Routing.RegisterRoute(nameof(TrainingDayDetailPage), typeof(TrainingDayDetailPage));
            Routing.RegisterRoute(nameof(WorkTrainingDayPage), typeof(WorkTrainingDayPage));
            Routing.RegisterRoute(nameof(DayTrainingPage), typeof(DayTrainingPage));
        }

    }
}
