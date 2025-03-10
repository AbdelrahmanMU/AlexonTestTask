using ExperimentsDemo.Core;
using ExperimentsDemo.Core.Interfaces;
using ExperimentsDemo.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExperimentsDemo.Infrastructure;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddAppInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<ApplicationDbContext>();
        services.AddTransient<IUnitOfWork, UnitOfWork>();

        services.AddScoped(typeof(BaseRepository<>));
        services.AddScoped(typeof(IBaseRepository<>), typeof(CachedMemoryRepository<>));
        //services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

        services.AddDbContext<ApplicationDbContext>(option =>
        {
            option.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
        });

        services.BuildServiceProvider().GetRequiredService<ApplicationDbContext>().Database.Migrate();
        services.AddMemoryCache();

        return services;
    }
}
