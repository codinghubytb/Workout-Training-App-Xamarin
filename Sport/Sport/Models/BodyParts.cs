using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Sport.Models
{
    public class BodyParts
    {
        [AutoIncrement, PrimaryKey]
        public int Id { get; set; }

        public string Name { get; set; }

        public int NbExercise { get; set; } = 0;
    }
}
