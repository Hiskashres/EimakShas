using EimakShas.Data;

namespace EimakShas.Services
{
    public class EimakShasService(ApplicationDbContext _context)
    {
        public int CalculatePercentage(int mainNumber, int secondNum)
        {
            return (int)((double)secondNum / mainNumber * 100);
        }
    }
}
