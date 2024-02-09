using BikesRent.BusinessLogicLayer.ViewModels;
using BikesRent.DataAccessLayer;
using BikesRent.DataAccessLayer.Entities;

namespace BikesRent.BusinessLogicLayer;

public class BikeService : IBikeService
{

    private readonly IEntityRepository _entityRepository;
    private readonly ICache _cache;

    public BikeService(IEntityRepository entityRepository, ICache cache)
    {
        _entityRepository = entityRepository;
        _cache = cache;
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
        _cache.Remove("bikes");

        var bike = (await _entityRepository.Where<Bike>(x => x.Id == model.Id)).FirstOrDefault();

        bike.IsElectric = model.IsElectric;
        bike.Brand = model.Brand;
        bike.Type = model.Type;

        await _entityRepository.Update();
    }
}