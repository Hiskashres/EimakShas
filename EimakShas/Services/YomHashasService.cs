using EimakShas.Data;
using EimakShas.DTOs;
using EimakShas.Models;
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
                bool alreadyAssignd = _context.YomHashas_Dafim
                    .Any(d => d.DafId == dafId);

                if (!alreadyAssignd)
                {
                    newYomHashas_dafim.Add(new YomHashas_Daf { DafId = dafId });
                    _context.YomHashas.First().DafimAmount++;
                }
            }
            _context.YomHashas_Dafim.AddRange(newYomHashas_dafim);

            // Remove deselected dafim
            deselectedDafim = _context.YomHashas_Dafim
                .Include(d => d.Daf.Masechta)
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
            var selectedDaf = _context.Dafim
                .Include(d => d.Masechta)
                .FirstOrDefault(d => d.DafId == dafId);

            var shas = _context.ShasInfo.First();
            var yomHashas = _context.YomHashas.First();

            if (selectedDaf == null) return;

            if (!selectedDaf.IsCompleted_Daf)
            {
                yomHashas.DafimCompleted++;
                selectedDaf.IsCompleted_Daf = true;
                selectedDaf.Masechta.DafimFinished++;
                shas.DafimFinished++;
                shas.DafimLearned++;
            }
            else
            {
                yomHashas.DafimCompleted--;
                selectedDaf.IsCompleted_Daf = false;
                selectedDaf.Masechta.DafimFinished--;
                shas.DafimFinished--;
                shas.DafimLearned--;
            }

            // Calculate Goal percentage
            yomHashas.PercentCompleted = eimakShasService.CalculatePercentage(yomHashas.Goal ,yomHashas.DafimCompleted);

            // Calculate Bonus Goal percentage
            yomHashas.PercentCompleted_Bonus = eimakShasService.CalculatePercentage(yomHashas.BonusGoal, yomHashas.DafimCompleted);

            _context.SaveChanges();
            return;
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

        public YomHashasDafDTO[] GetYomHashasDafim()
        {
            var yomHashas_dafim = _context.YomHashas_Dafim
                .Include(d => d.Daf)
                .Include(d => d.Daf.Masechta)
                .ToList();
            var dafimDTO = new List<YomHashasDafDTO>();

            foreach (var daf in yomHashas_dafim)
            {
                var linkedUsers = _context.UserUmidim
                            .Include(uu => uu.User)
                            .Include(uu => uu.Umid)
                            .Where(uu => uu.Umid.DafId == daf.DafId)
                            .Select(uu => uu.User)
                            .Distinct()
                            .OrderBy(u => u.UserId) // optional ordering
                            .ToList(); string chavrisa1 = linkedUsers.Count >= 1 ? $"{linkedUsers[0].FirstName} {linkedUsers[0].LastName}" : null;
                string chavrisa2 = linkedUsers.Count >= 2 ? $"{linkedUsers[1].FirstName} {linkedUsers[1].LastName}" : null;

                dafimDTO.Add(new YomHashasDafDTO
                {
                    DafId = daf.DafId,
                    DafLetter = daf.Daf.DafLetter,
                    MasechtaId = daf.Daf.Masechta.MasechtaId,
                    MasechtaName = daf.Daf.Masechta.MasechtaName,
                    MasechtaOrder = daf.Daf.Masechta.MasechtaOrder,
                    IsCompleted = daf.Daf.IsCompleted_Daf,
                    Chavrisa1 = chavrisa1,
                    Chavrisa2 = chavrisa2
                });
            }

            return dafimDTO.ToArray();
        }

        //public User[] GetUsers()
        //{
        //    var users = _context.Users
        //        .Include(u => u.yomHashas_Dafim)
        //        .ToList();

        //    foreach (var user in users)
        //    {

        //    }
        //}
    }
}
