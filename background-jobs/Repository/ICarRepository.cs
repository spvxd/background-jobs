using background_jobs.Models;

namespace background_jobs.Repository;

public interface ICarRepository
{
    public Task CreateCarAsync(List<Car> carList);
    public Task<List<Car>> GetAllCarsAsync();
}