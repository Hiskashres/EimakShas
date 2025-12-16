using EimakShas.Data;
using EimakShas.Interfaces;
using EimakShas.Models;

namespace EimakShas.Services
{
    public class YomHashasService(ApplicationDbContext dbContext) : IYomHashasService
    {
        private readonly ApplicationDbContext _context = dbContext;

        public void AddDafimToYomHashas(int[] dafimIds) 
        {
            List<YomHashas_Daf> newYomHashas_dafim = [];
            List<YomHashas_Daf> deselectedDafim = [];

            foreach (int dafId in dafimIds) 
            {
                bool alreadyAssignd = _context.YomHashas_Dafim.Any(d => d.DafId == dafId);

                if (!alreadyAssignd)
                {
                    newYomHashas_dafim.Add(new YomHashas_Daf { DafId = dafId });
                }
            }
            _context.YomHashas_Dafim.AddRange(newYomHashas_dafim);

            deselectedDafim = _context.YomHashas_Dafim
                .Where(d => !dafimIds.Contains(d.YomHashas_DafId))
                .ToList();
            _context.YomHashas_Dafim.RemoveRange(deselectedDafim);

            _context.SaveChanges();

            return;
        }

        public void MarkDafAsCompleted(int dafId) 
        {
            var yomHashas_daf = _context.Dafim
                .FirstOrDefault(d => d.DafId == dafId).IsCompleted_Daf = true;
        }

        public void SetGoal() { }
    }
}
