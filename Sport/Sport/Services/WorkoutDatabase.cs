using Sport.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sport.Services
{
    public class WorkoutDatabase
    {
        /// <summary>
        /// Get list table Workout async
        /// </summary>
        /// <returns>List Workout</returns>
        public Task<List<Workout>> GetWorkoutAsync()
        {
            return App._database.Table<Workout>().ToListAsync();
        }

        /// <summary>
        /// Get Workout with variable id
        /// </summary>
        /// <param name="id">id Workout</param>
        /// <returns>Workout</returns>
        public Task<Workout> GetWorkoutAsync(int id)
        {
            return App._database.Table<Workout>()
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Search Workout
        /// </summary>
        /// <param name="a">string text</param>
        /// <returns>List of Workout</returns>
        public Task<List<Workout>> SearchWorkoutAsync(string a)
        {
            string searchNoSpaces = a.Replace(" ", "%");
            var get_docnumb = App._database.QueryAsync<Workout>("SELECT * FROM Workout WHERE Name LIKE ?", "%" + searchNoSpaces + "%");

            return get_docnumb;
        }

        /// <summary>
        /// Update or Insert Workout
        /// </summary>
        /// <param name="Workout">Class Workout</param>
        /// <returns></returns>
        public Task<int> SaveWorkoutAsync(Workout Workout)
        {
            if (Workout.Id != 0)
                return App._database.UpdateAsync(Workout);
            else
            {
                return App._database.InsertAsync(Workout);
            }
        }

        /// <summary>
        /// Delete Workout
        /// </summary>
        /// <param name="Workout">Class Workout</param>
        /// <returns></returns>
        public Task<int> DeleteWorkoutAsync(Workout Workout)
        {
            return App._database.DeleteAsync(Workout);
        }

        /// <summary>
        /// Add Workout if the Table Database Workout is empty
        /// </summary>
        /// <returns></returns>
        public async Task AddWorkout()
        {
            List<Workout> bodyParts = await GetWorkoutAsync();
            if (bodyParts.Count == 0)
            {
                await SaveWorkoutAsync(new Workout
                {
                    NameImage = "W",
                    Name = "Workout Training Full Body 💪",
                    Description = "This is a full body weight workout that promotes muscle mass and muscular endurance",
                    Difficulty = "Medium",
                    NbExercise = 4,
                    IdExerciseRound = "1|2|3|4",
                    RepetitionExerciceRound = "10|10|10|10",
                    NbRounds = 10,
                    TimeBreak = 60,
                    BodyPartsExercise = "Back|Triceps|Pectorals|Shoulders|Quadriceps|Calves|Biceps|Abs",
                    ColorImage = "#f5d8a0",
                    IsEdit = false
                });
            }
        }
    }
}
