using Cafe.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Cafe.Domain.Core.Errors.DomainErrors;

namespace Cafe.Persistance.EFCustomizations
{
    public class CafeDbContext : DbContext
    {
        public CafeDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Cafe.Domain.Entities.Cafe> Cafes { get; set; }

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CafeDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}


