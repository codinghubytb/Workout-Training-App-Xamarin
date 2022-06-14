using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using Xamarin.Forms;

namespace Sport.Models
{
    public class Exercise
    {
        [AutoIncrement, PrimaryKey]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime DateTime { get; set; }

        public string Source { get; set; }

        public string Difficulty { get; set; }

        public string ColorDifficulty { get; set; }

        public bool Favorite { get; set; }

        public string StarFavorite { get; set; }

        public string BodyPartsExercise { get; set; }

        public bool isEdit { get; set; }
    }
}
