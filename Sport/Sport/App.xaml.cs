using Sport.Models;
using Sport.Services;
using Sport.Views;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace Sport
{
    public partial class App : Application
    {
        public static SQLiteAsyncConnection _database;
        public static TableDatabase Database { get; set; }
        public static Exercise ExerciseIdWorkout { get; set; } = null;
        public static int Répetitions { get; set; }
        public App()
        {
            InitializeComponent();
            InitializeDatabase();
            MainPage = new AppShell();
        }

        public void InitializeDatabase()
        {
            if (Database == null)
            {
                Database = new TableDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "sport.db3"));
                
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
