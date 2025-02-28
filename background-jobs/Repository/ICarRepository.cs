﻿using background_jobs.Models;

namespace background_jobs.Repository;

public interface ICarRepository
{
    public Task CreateCarAsync(List<Car> carList);
    public Task<List<string>> CheckCarAsync(List<Car> carList);
    public Task<List<Car>> GetAllCarsAsync();
    public Task DeleteArchivedCarAsync(List<Car> carList);
}