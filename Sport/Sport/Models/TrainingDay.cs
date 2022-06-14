using System;
using System.Collections.Generic;
using System.Text;

using SQLite;

namespace Sport.Models
{
    public class TrainingDay
    {
        [AutoIncrement, PrimaryKey]
        public int Id { get; set; }

        public string DateTime { get; set; }

        public int Day { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

        public int IdWorkout { get; set; }

        public bool IsRelaxation { get; set; }

        public int RoundPerformed { get; set; }

        public int ExercisePerformed { get; set; }

        public int RepetitionPerformed { get; set; }

        public string TimeTraining { get;set; }
    }
}
