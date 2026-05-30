using Bansi.Domain.Entities;
using Bansi.Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Bansi.Infrastructure
{
    public class BansiContext : DbContext
    {
        public DbSet<Examen> Examenes { get; set; }
        public BansiContext(DbContextOptions<BansiContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ExamenConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
