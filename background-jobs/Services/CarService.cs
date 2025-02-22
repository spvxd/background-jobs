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

    public async Task<List<string>>  CheckCarAsync(List<Car> carList)
    {
        var filteredCars =  await _carRepository.CheckCarAsync(carList);
        Console.WriteLine(filteredCars);
        return filteredCars;
    }

    public async Task<List<Car>> RemoveDuplicateCarsAsync(List<Car> carList, List<string> filteredCars)
    {
        carList.RemoveAll(car => filteredCars.Contains(car.Link));
        return carList;
    }
}