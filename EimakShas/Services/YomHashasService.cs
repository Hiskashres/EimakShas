using EimakShas.Data;
using EimakShas.DTOs;
using EimakShas.Interfaces;
using EimakShas.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace EimakShas.Services
{
    public class YomHashasService(ApplicationDbContext _context)/* : IYomHashasService*/
    {
        //private readonly ApplicationDbContext _context = dbContext;
        EimakShasService eimakShasService = new EimakShasService(_context);

        public void AddDafimToYomHashas(int[] dafimIds) 
        {
            List<YomHashas_Daf> newYomHashas_dafim = [];
            List<YomHashas_Daf> deselectedDafim = [];
            int[] dafimIds2 = dafimIds;

            // Add selected dafim
            foreach (int dafId in dafimIds) 
            {
                bool alreadyAssignd = _context.YomHashas_Dafim.Any(d => d.DafId == dafId);

                if (!alreadyAssignd)
                {
                    newYomHashas_dafim.Add(new YomHashas_Daf { DafId = dafId });
                    _context.YomHashas.First().DafimAmount++;
                }
            }
            _context.YomHashas_Dafim.AddRange(newYomHashas_dafim);

            // Remove deselected dafim
            deselectedDafim = _context.YomHashas_Dafim
                .Where(d => !dafimIds2.Contains(d.YomHashas_DafId))
                .ToList();
            foreach (var daf in deselectedDafim)
                _context.YomHashas.First().DafimAmount--;

            _context.YomHashas_Dafim.RemoveRange(deselectedDafim);

            _context.SaveChanges();

            return;
        }

        public void MarkDafAsCompleted(int dafId)
        {
            var completedDaf = _context.Dafim
                .Include(d => d.Masechta)
                .FirstOrDefault(d => d.DafId == dafId);

            if (completedDaf == null)
                return;

            var yomHashas = _context.YomHashas.First();
            yomHashas.DafimCompleted++;

            // Calculate Goal percentage
            yomHashas.PercentCompleted = eimakShasService.CalculatePercentage(yomHashas.Goal ,yomHashas.DafimCompleted);

            // Calculate Bonus Goal percentage
            yomHashas.PercentCompleted_Bonus = eimakShasService.CalculatePercentage(yomHashas.BonusGoal, yomHashas.DafimCompleted);

            completedDaf.IsCompleted_Daf = true;
            completedDaf.Masechta.DafimFinished++;
            _context.ShasInfo.First().DafimFinished++;

            _context.SaveChanges();
        }

        public void SetGoals(int firstGoal, int bonusGoal, TimeOnly endTime)
        {
            bool yomHashasExists = _context.YomHashas.Any();
            if (!yomHashasExists)
            {
                var createYomHashas = new YomHashas();
                _context.YomHashas.Add(createYomHashas);
                _context.SaveChanges();
            }

            _context.YomHashas.First().Goal = firstGoal;
            _context.YomHashas.First().BonusGoal = bonusGoal;
            _context.YomHashas.First().EndTime = endTime;

            _context.SaveChanges();

            return;
        }

        public YomHashasInfoDTO GetYomHashasInfo()
        {
            var yomHashas = _context.YomHashas.First();

            return new YomHashasInfoDTO
            {
                MainGoal = yomHashas.Goal,
                BonusGoal = yomHashas.BonusGoal,
                EndTime = yomHashas.EndTime,
                DafimAmount = yomHashas.DafimAmount,
                DafimFinished = yomHashas.DafimCompleted,
                DafimNotFinished = yomHashas.Goal - yomHashas.DafimCompleted,
                PercentageFinished = yomHashas.PercentCompleted,
                DafimNotFinished_Bonus = yomHashas.BonusGoal - yomHashas.DafimCompleted,
                PercentageFinished_Bonus = yomHashas.PercentCompleted_Bonus
            };
        }
    }
}
