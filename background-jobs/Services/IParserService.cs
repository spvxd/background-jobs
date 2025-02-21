using Microsoft.AspNetCore.Mvc;

namespace background_jobs.Services;

public interface IParserService
{
    public Task ParseWebsite();
}