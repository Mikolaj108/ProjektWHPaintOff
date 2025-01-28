using Microsoft.EntityFrameworkCore;
using WarhammerPaintCenter.Models.Entities;

namespace WarhammerPaintCenter.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
            
        }


        public DbSet<Paint> Paints {  get; set; }

        public DbSet<UserAccount> UserAccounts { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Paint>()
                .HasOne(p => p.UserAccount)
                .WithMany(u => u.Paints)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }




    }


}
