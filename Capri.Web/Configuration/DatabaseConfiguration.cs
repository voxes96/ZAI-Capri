using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Capri.Database;

namespace Capri.Web.Configuration
{
    public static class DatabaseConfiguration
    {
        public static void AddDatabaseConfiguration(this IServiceCollection services, string connectionString)
        {
            if (System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
                services.AddDbContext<CapriDbContext>(options => options.UseSqlServer(connectionString));
            else
                services.AddDbContext<CapriDbContext>(options => options.UseSqlServer("Server = localhost; Database = CapriDB; Trusted_Connection = True;"));

            services.BuildServiceProvider().GetService<CapriDbContext>().Database.Migrate();

            services.AddScoped<ISqlDbContext, CapriDbContext>();
        }
    }
}
