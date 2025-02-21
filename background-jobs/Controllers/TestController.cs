using background_jobs.Services;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;

namespace background_jobs.Controllers;
[ApiController]
[Route("api/[controller]")]

public  class  TestController: ControllerBase
{
    private readonly IParserService _parserService;

    public TestController(IParserService parserService)
    {
        _parserService = parserService;
    }
    [HttpGet]
    public async Task<IActionResult> Get()
    {

        await _parserService.ParseWebsite();
        return Ok("good"); 
    }
}