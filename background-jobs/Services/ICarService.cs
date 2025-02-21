using background_jobs.Models;

namespace background_jobs.Services;

public interface ICarService
{
    public Task SaveCarsAsync(List<Car> carList);
    
}