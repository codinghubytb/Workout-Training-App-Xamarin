using Sport.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sport.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditExercisePage : ContentPage
    {
        public EditExercisePage()
        {
            InitializeComponent();
            BindingContext = new EditExerciseViewModel();
        }
    }
}