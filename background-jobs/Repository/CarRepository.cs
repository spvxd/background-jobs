using background_jobs.Models;
using background_jobs.Services;
using Microsoft.EntityFrameworkCore;

namespace background_jobs.Repository;

public class CarRepository : ICarRepository
{
    private readonly AppDbContext _context;

    public CarRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task CreateCarAsync(List<Car> car)
    {
        await _context.Cars.AddRangeAsync(car);
        await _context.SaveChangesAsync();
    }



    public async Task<List<string>> CheckCarAsync(List<Car> carList)
    {
        return await _context.Cars
            .Select(m => m.Link)
            .Where(link => carList.Select(c => c.Link).Contains(link))
            .ToListAsync();
    }
}