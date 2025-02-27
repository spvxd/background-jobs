using Hangfire;

namespace background_jobs.Services;

public class BackgroundJobService : IHostedService
{
    private readonly IParserService _parserService;

    public BackgroundJobService(IParserService parserService)
    {
        _parserService = parserService;
    }
    
    public Task StartAsync(CancellationToken cancellationToken)
    {
        StartBackgroundJobs();
        return Task.CompletedTask;
    }
    
    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public void StartBackgroundJobs()
    {
        RecurringJob.AddOrUpdate("parse-cars", () => ParseCarsAsync(), Cron.Minutely);
        RecurringJob.AddOrUpdate("check-archived-cars", () => CheckArchivedCarsAsync(), Cron.Hourly);
    }

    public async Task ParseCarsAsync()
    {
        await _parserService.ParseWebsite();
    }

    public async Task CheckArchivedCarsAsync()
    {
        await _parserService.CheckArchivedCars();
    }
    

    
}