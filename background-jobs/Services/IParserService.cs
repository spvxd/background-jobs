using background_jobs.Models;
using Microsoft.AspNetCore.Mvc;

namespace background_jobs.Services;

public interface IParserService
{
    public Task ParseWebsite();
    public Task<string> GetPageContentAsync();
    public List<Car> ExtractCarsFromHtml(string htmlContent);
    public Task CheckArchivedCars();
}