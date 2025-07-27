using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.Context
{
    public class TarbotDBContext : DbContext
    {
        public TarbotDBContext(DbContextOptions<TarbotDBContext> options)
           : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Add any additional model configurations here if needed
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Inek> Inek { get; set; }
        public DbSet<Reminder> Reminder { get; set; }

    }
}
