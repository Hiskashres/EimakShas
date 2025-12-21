using EimakShas.Data;
using EimakShas.Models;
using Microsoft.EntityFrameworkCore;

namespace EimakShas.Services
{
    public class UserUmidService(ApplicationDbContext _context)
    {
        public void MarkUserDafAsComplete(int[] dafimIds, int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == userId);
            var shas = _context.ShasInfo.First();

            foreach (int dafId in dafimIds)
            {
                //Have to check if UserUmidim Are existing, and if it's not completed already


                var daf = _context.Dafim
                    .Include(d => d.Masechta)
                    .FirstOrDefault(d => d.DafId == dafId );

                // Checks if Daf is already completed by another user
                if (daf.IsCompleted_Daf == true)
                {
                    shas.DafimFinished++;
                    daf.Masechta.DafimFinished++;
                }

                user.DafimFinished++;
                shas.DafimLearned++;
                daf.IsCompleted_Daf = true;

                // Mark UserUmidim as complete
                var umidim = _context.Umidim
                    .Include(u => u.Daf)
                    .Include(u => u.UserUmidim)
                    .Where(u => u.DafId == dafId)
                    .ToList();
                foreach (var umid in umidim)
                {
                    umid.IsCompleted_Umid = true;
                    umid.UserUmidim.FirstOrDefault(u => u.UserId == userId).IsCompleted_UserUmid = true;
                }
            }
        }
    }
}
