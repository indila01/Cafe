using Cafe.Domain.Entities;
using Cafe.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Cafe.Domain.Core.Errors.DomainErrors;

namespace Cafe.Persistance.EFCustomizations
{
    public class CafeDbContext : DbContext, IUnitOfWork
    {
        public CafeDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Cafe.Domain.Entities.Employee> Employees { get; set; }
        public DbSet<Cafe.Domain.Entities.Cafe> Cafes { get; set; }

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CafeDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            int result = await base.SaveChangesAsync(cancellationToken);
            return result;
        }
    }
}


