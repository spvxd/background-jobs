using background_jobs.Models;

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

    public async Task<List<Car>> GetAllCarsAsync()
    {
        return _context.Cars.ToList();
    }
}