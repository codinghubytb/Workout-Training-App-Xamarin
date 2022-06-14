using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sport.Models
{
    public class Workout
    {
        [AutoIncrement, PrimaryKey]
        public int Id { get; set; }

        public string ColorImage { get; set; }

        public string NameImage { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int NbExercise { get; set; }

        public string IdExerciseRound { get; set; }

        public string RepetitionExerciceRound { get; set; }

        public int NbRounds { get; set; }

        public int TimeBreak { get; set; }

        public string BodyPartsExercise { get; set; }

        public string Difficulty { get; set; }

        public bool IsEdit { get; set; }
    }
}
