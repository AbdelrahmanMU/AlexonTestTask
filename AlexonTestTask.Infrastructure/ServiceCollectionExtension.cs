using AlexonTestTask.Core;
using AlexonTestTask.Core.Interfaces;
using AlexonTestTask.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AlexonTestTask.Infrastructure;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddAppInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<ApplicationDbContext>();
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

        services.AddDbContext<ApplicationDbContext>(option =>
        {
            option.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
        });

        services.BuildServiceProvider().GetRequiredService<ApplicationDbContext>().Database.Migrate();
        
        return services;
    }
}
