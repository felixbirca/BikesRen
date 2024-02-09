using BikesRent.BusinessLogicLayer.ViewModels;
using BikesRent.DataAccessLayer;
using BikesRent.DataAccessLayer.Entities;
using Microsoft.Extensions.Logging;

namespace BikesRent.BusinessLogicLayer;

public class BikeService : IBikeService
{
    private readonly IEntityRepository _entityRepository;
    private readonly ICache _cache;
    private readonly ILogger<BikeService> _logger;

    public BikeService(IEntityRepository entityRepository, ICache cache, ILogger<BikeService> logger)
    {
        _entityRepository = entityRepository;
        _cache = cache;
        _logger = logger;
    }

    public async Task<ICollection<BikeViewModel>> GetAllBikes()
    {
        var cachedBikes = _cache.Get<ICollection<BikeViewModel>>("bikes");

        if (cachedBikes != null)
        {
            return cachedBikes;
        }

        var bikeViewModels = new List<BikeViewModel>();

        var bikes = await _entityRepository.GetAll<Bike>();

        foreach (var bike in bikes)
        {
            bikeViewModels.Add(new BikeViewModel
            {
                Id = bike.Id,
                Brand = bike.Brand,
                Type = bike.Type,
                IsElectric = bike.IsElectric
            });
        }

        _cache.Set("bikes", bikeViewModels);

        return bikeViewModels;
    }

    public async Task<BikeViewModel> GetById(string id)
    {
        var bike = (await _entityRepository.Where<Bike>(x => x.Id == id)).FirstOrDefault();

        if (bike == null)
        {
            return null;
        }

        return new BikeViewModel
        {
            Id = bike.Id,
            Brand = bike.Brand,
            Type = bike.Type,
            IsElectric = bike.IsElectric
        };
    }

    public async Task CreateBike(CreateBikeModel bike)
    {
        _cache.Remove("bikes");

        await _entityRepository.Create(new Bike
        {
            Brand = bike.Brand,
            Type = bike.Type,
            IsElectric = bike.IsElectric,
        });
    }

    public async Task<ICollection<BikeViewModel>> GetAvailableBikes()
    {
        var bikeViewModels = new List<BikeViewModel>();

        var bikes = await _entityRepository.GetAll<Bike>();

        foreach (var bike in bikes)
        {
            bikeViewModels.Add(new BikeViewModel
            {
                Id = bike.Id,
                Brand = bike.Brand,
                Type = bike.Type,
                IsElectric = bike.IsElectric
            });
        }

        var busyBikes = (await _entityRepository.GetAll<RentHistory>())
            .Where(x => x.IsActive)
            .Select(x => x.BikeId);

        bikeViewModels = bikeViewModels.Where(x => !busyBikes.Contains(x.Id)).ToList();

        return bikeViewModels;
    }

    public async Task UpdateBike(UpdateBikeModel model)
    {
        _logger.LogInformation($"Updating bike with id {model.Id}.");
        _cache.Remove("bikes");

        var bike = (await _entityRepository.Where<Bike>(x => x.Id == model.Id)).FirstOrDefault();

        if (bike == null)
        {
            _logger.LogError($"Bike with id {model.Id} was not found in the system.");
            return;
        }

        bike.IsElectric = model.IsElectric;
        bike.Brand = model.Brand;
        bike.Type = model.Type;

        await _entityRepository.Update();
        _logger.LogInformation("Successfully updated bike with id {model.Id}");
    }
}