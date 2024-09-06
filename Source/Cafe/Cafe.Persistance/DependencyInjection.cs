using Cafe.Domain.Repositories;
using Cafe.Persistance.EFCustomizations;
using Cafe.SharedKernel;
using Cafe.SharedKernel.Util;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cafe.Persistance
{
    public static class DependancyInjection
    {
        public static IServiceCollection RegisterPersistenceServices(
           this IServiceCollection services,
           IConfiguration configuration)
        {
            // Registering the DbContext
            var connectionString = configuration.GetConnectionString(Connectionstring.CafeDbConnectionKey);
            Ensure.NotEmpty(connectionString, "DbConnection string is empty", nameof(connectionString));
            services.AddDbContext<CafeDbContext>(o => o.UseMySQL(connectionString));

            // Registering the Repositories
            services.AddScoped<ICafeRepository, CafeRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            return services;
        }

    }
}
