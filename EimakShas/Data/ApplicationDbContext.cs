using Microsoft.EntityFrameworkCore;
using EimakShas.Models;

namespace EimakShas.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<ShasCycle> ShasCycles { get; set; }
        public DbSet<Masechta> Masechtas { get; set; }
        public DbSet<Daf> Dafim {  get; set; }
        public DbSet<Umid> Umidim { get; set; }
    }
}