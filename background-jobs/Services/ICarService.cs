using background_jobs.Models;

namespace background_jobs.Services;

public interface ICarService
{
    public Task SaveCarsAsync(List<Car> carList);
    public Task<List<string>> CheckCarAsync(List<Car> carList);
    public Task<List<Car>> RemoveDuplicateCarsAsync(List<Car> carList, List<string> filteredCars);
    
    public Task<List<Car>> GetAllCarsAsync();
    
    public Task DeleteCarsAsync(List<Car> carList);
}