using Microsoft.EntityFrameworkCore;
using EimakShas.Models;

namespace EimakShas.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

        public DbSet<User> Users { get; set; }
        public DbSet<ShasInfo> ShasInfo { get; set; }
        public DbSet<Masechta> Masechtas { get; set; }
        public DbSet<Daf> Dafim {  get; set; }
        public DbSet<Umid> Umidim { get; set; }
        public DbSet<UserUmid> UserUmidim { get; set; }
        public DbSet<YomHashas> YomHashas { get; set; }
        public DbSet<YomHashas_Daf> YomHashas_Dafim { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserUmid>()
                .HasKey(x => new { x.UserId, x.UmidId });

            modelBuilder.Entity<UserUmid>()
                .HasOne(x => x.User)
                .WithMany(u => u.UserUmidim)
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<UserUmid>()
                .HasOne(x => x.Umid)
                .WithMany(u => u.UserUmidim)
                .HasForeignKey(u => u.UmidId);
        }
    }
}