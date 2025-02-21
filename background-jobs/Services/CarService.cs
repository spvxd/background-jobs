using background_jobs.Models;
using background_jobs.Repository;

namespace background_jobs.Services;

public class CarService: ICarService
{
    private readonly ICarRepository _carRepository;
    public CarService(ICarRepository carRepository)
    {
        _carRepository = carRepository;
    }

    public async Task SaveCarsAsync(List<Car> carList)
    {
        await _carRepository.CreateCarAsync(carList);
    }
}