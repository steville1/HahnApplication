using Hahn.ApplicatonProcess.December2020.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hahn.ApplicatonProcess.December2020.Data
{
    public class HahnApplicationDBContext : DbContext
    {
        public HahnApplicationDBContext()
        {
        }
        public HahnApplicationDBContext(DbContextOptions options) : base(options)
        {
        }

            public DbSet<Applicant> Applicants { get; set; }
       

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

        }

    }
}

