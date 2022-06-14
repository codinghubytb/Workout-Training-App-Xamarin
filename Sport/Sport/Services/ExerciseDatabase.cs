using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

using Sport.Models;
using System.Threading.Tasks;

namespace Sport.Services
{
    public class ExerciseDatabase
    {

        /// <summary>
        /// Get list table Exercise async
        /// </summary>
        /// <returns>List Exercise</returns>
        public Task<List<Exercise>> GetExerciseAsync()
        {
            return App._database.Table<Exercise>().ToListAsync();
        }

        /// <summary>
        /// Get Exercise with variable id
        /// </summary>
        /// <param name="id">id Exercise</param>
        /// <returns>Exercise</returns>
        public Task<Exercise> GetExerciseAsync(int id)
        {
            return App._database.Table<Exercise>()
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Search Exercise
        /// </summary>
        /// <param name="a">string text</param>
        /// <returns>List of exercise</returns>
        public Task<List<Exercise>> SearchExerciseAsync(string a)
        {
            string searchNoSpaces = a.Replace(" ", "%");
            var get_docnumb = App._database.QueryAsync<Exercise>("SELECT * FROM Exercise WHERE Name LIKE ?", "%" + searchNoSpaces + "%");

            return get_docnumb;
        }

        /// <summary>
        /// Update or Insert Exercise
        /// </summary>
        /// <param name="exercise">Class Exercise</param>
        /// <returns></returns>
        public Task<int> SaveExerciseAsync(Exercise exercise)
        {
            if (exercise.Id != 0)
                return App._database.UpdateAsync(exercise);
            else
            {
                return App._database.InsertAsync(exercise);
            }
        }

        /// <summary>
        /// Delete Exercise
        /// </summary>
        /// <param name="exercise">Class Exercise</param>
        /// <returns></returns>
        public Task<int> DeleteExerciselAsync(Exercise exercise)
        {
            return App._database.DeleteAsync(exercise);
        }

        /// <summary>
        /// Add Exercise if table database Exercise is empty
        /// </summary>
        /// <returns></returns>
        public async Task AddExercise()
        {
            ImageStorage imageStorage = new ImageStorage();
            List<Exercise> bodyParts = await GetExerciseAsync();
            if (bodyParts.Count == 0)
            {
                await SaveExerciseAsync(new Exercise { Name = "Pull Up", Source = "pullUp.gif", isEdit= false, BodyPartsExercise = "back | biceps | shoulders", Difficulty = "Easy", ColorDifficulty = "#32cd32", DateTime = DateTime.Now, Favorite = true, Description = "The pull-up is a physical exercise that consists of raising your shoulders to the level of a bar by holding it with your hands. The main purpose of pull-ups is to develop the back and arm muscles. It is a basic multi-joint strength training exercise that is very popular because it is simple and effective. There is a variant in which the exercise is performed horizontally." });
                await SaveExerciseAsync(new Exercise { Name = "Push Up", Source = "pushUp.gif", isEdit = false, BodyPartsExercise = "pectorals | triceps | shoulders", Difficulty = "Easy", ColorDifficulty = "#32cd32", DateTime = DateTime.Now, Favorite = true, Description = "The push-up is a strength training exercise that mainly involves the pectoralis major, deltoid and triceps. This exercise is popular because it can be performed anywhere, requiring no equipment." });
                await SaveExerciseAsync(new Exercise { Name = "Dips", Source = "dips.gif", isEdit = false, BodyPartsExercise = "triceps | pectorals | shoulders", Difficulty = "Easy", ColorDifficulty = "#32cd32", DateTime = DateTime.Now, Favorite = true, Description = "Double bars, also known as dips, are a multi-joint strength training exercise designed to develop strength and volume in the triceps, pectorals and shoulders (anterior deltoid)." });
                await SaveExerciseAsync(new Exercise { Name = "Squat", Source = "squat.gif", isEdit = false, BodyPartsExercise = "quadriceps | calves | ischios", Difficulty = "Easy", ColorDifficulty = "#32cd32", DateTime = DateTime.Now, Favorite = true, Description = "The squat is a poly-articular bodybuilding movement useful for all sports and ideal for building up the whole lower body. The main muscles involved are the quadriceps, glutes and adductors." });
                await SaveExerciseAsync(new Exercise { Name = "Abs", Source = "abs.gif", isEdit = false, BodyPartsExercise = "abs", Difficulty = "Easy", ColorDifficulty = "#32cd32", DateTime = DateTime.Now, Favorite = true, Description = "ABS (Abdominal Body System) training is defined as an exercise programme aimed primarily at exposing and developing strong abdominal muscles." });

            }
        }
    }
}
