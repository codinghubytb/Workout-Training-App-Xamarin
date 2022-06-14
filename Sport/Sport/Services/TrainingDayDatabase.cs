using Sport.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sport.Services
{
    public class TrainingDayDatabase
    {
        /// <summary>
        /// Get list table TrainingDay async
        /// </summary>
        /// <returns>List TrainingDay</returns>
        public Task<List<TrainingDay>> GetTrainingDayAsync()
        {
            return App._database.Table<TrainingDay>().ToListAsync();
        }

        /// <summary>
        /// Get TrainingDay with variable id
        /// </summary>
        /// <param name="id">id TrainingDay</param>
        /// <returns>Workout</returns>
        public Task<TrainingDay> GetTrainingDayAsync(int id)
        {
            return App._database.Table<TrainingDay>()
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();
        }
        
        /// <summary>
        /// Get TrainingDay with variable day, month, year
        /// </summary>
        /// <param name="day">Day TrainingDay</param>
        /// <param name="month">Month TrainingDay</param>
        /// <param name="year">Year TrainingDay</param>
        /// <returns>TrainingDay</returns>
        public Task<TrainingDay> GetTrainingDayAsync(int day, int month, int year)
        {
            return App._database.Table<TrainingDay>()
                .Where(i => i.Day == day)
                .Where(i => i.Month == month)
                .Where(i => i.Year == year)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Get TrainingDay with variable DateTime
        /// </summary>
        /// <param name="date">DateTime TrainingDay</param>
        /// <returns>TrainingDay</returns>
        public Task<TrainingDay> GetTrainingDayAsync(string date)
        {
            return App._database.Table<TrainingDay>()
                .Where(i => i.DateTime == date)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Search List Training Day with variable day, month, year
        /// </summary>
        /// <param name="day">Day TrainingDay</param>
        /// <param name="month">Month Training Day</param>
        /// <param name="year">Year TrainingDay</param>
        /// <returns>List of TrainingDay</returns>
        public Task<List<TrainingDay>> SearchTrainingDayAsync(int day, int month, int year)
        {  
            return App._database.QueryAsync<TrainingDay>("SELECT * FROM TrainingDay WHERE Day LIKE ? AND Month LIKE ? AND Year LIKE ?", $"%{day}%", $"%{month}%", $"%{year}%");

        }

        /// <summary>
        /// Update or Insert TrainingDay
        /// </summary>
        /// <param name="TrainingDay">Class TrainingDay</param>
        /// <returns></returns>
        public Task<int> SaveTrainingDayAsync(TrainingDay TrainingDay)
        {
            if (TrainingDay.Id != 0)
                return App._database.UpdateAsync(TrainingDay);
            else
            {
                return App._database.InsertAsync(TrainingDay);
            }
        }

        /// <summary>
        /// Delete TrainingDay
        /// </summary>
        /// <param name="TrainingDay">Class TrainingDay</param>
        /// <returns></returns>
        public Task<int> DeleteTrainingDayAsync(TrainingDay TrainingDay)
        {
            return App._database.DeleteAsync(TrainingDay);
        }

        /// <summary>
        /// Add TrainingDay if table database TrainingDay is empty or elements are missing
        /// </summary>
        /// <returns></returns>
        public async Task AddTrainingDay()
        {
            List<TrainingDay> listTrainingDay = await GetTrainingDayAsync();
            if (listTrainingDay.Count == 0)
            {
                for (int i = 1; i < DateTime.Now.Day; i++)
                {

                    await SaveTrainingDayAsync(new TrainingDay
                    {
                        DateTime = $"{DateTime.Now.Month}/{i}/{DateTime.Now.Year}",
                        Day = i,
                        Month = DateTime.Now.Month,
                        Year = DateTime.Now.Year,
                        IsRelaxation = true,
                        TimeTraining = "0:0:0",
                    });
                }
            }
            else
            {
                for(int i = 1; i < DateTime.Now.Day; i++)
                {
                    TrainingDay trainingDay = await GetTrainingDayAsync(i, DateTime.Now.Month, DateTime.Now.Year);
                    if(trainingDay == null)
                    {
                        await SaveTrainingDayAsync(new TrainingDay
                        {
                            DateTime = $"{DateTime.Now.Month}/{i}/{DateTime.Now.Year}",
                            Day = i,
                            Month = DateTime.Now.Month,
                            Year = DateTime.Now.Year,
                            IsRelaxation = true,
                            TimeTraining = "0:0:0",
                        });
                    }
                }
            }
        }
    }
}
