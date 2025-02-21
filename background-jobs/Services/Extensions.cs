namespace background_jobs.Services;

public static class Extensions
{
    public static IServiceCollection AddCarService(this IServiceCollection services)
    {
        services.AddScoped<ICarService, CarService>();
        return services;
    }
}