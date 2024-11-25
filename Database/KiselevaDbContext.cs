using kiseleva_nastia_42_21.Models;
using kiseleva_nastia_42_21.Database.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace kiseleva_nastia_42_21.Database
{
    public class KiselevaDbContext : DbContext
    {
        //DbSet<EducationalSubject> EducationalSubjects { get; set; }
        //DbSet<Professor> Professors { get; set; }
        //DbSet<Workload> Workloads { get; set; }
        public DbSet<EducationalSubject> EducationalSubjects { get; set; }
        public DbSet<Professor> Professors { get; set; }
        public DbSet<Workload> Workloads { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EducationSubjectConfiguration());
            modelBuilder.ApplyConfiguration(new ProfessorConfiguration());
            modelBuilder.ApplyConfiguration(new WorkloadConfiguration());
        }
        public KiselevaDbContext(DbContextOptions options) : base(options) { }
        // public KiselevaDbContext(DbContextOptions<KiselevaDbContext> options) : base(options) { }
    }
    public class KiselevaDbContextFactory : IDesignTimeDbContextFactory<KiselevaDbContext>
    {
        public KiselevaDbContext CreateDbContext(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var optionsBuilder = new DbContextOptionsBuilder<KiselevaDbContext>();
            optionsBuilder.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
            return new KiselevaDbContext(optionsBuilder.Options);
        }
    }
}
