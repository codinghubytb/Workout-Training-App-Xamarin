using Sport.Models;
using Sport.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sport.Views
{
    public partial class NewItemPage : ContentPage
    {

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewExerciceViewModel();
        }

    }
}