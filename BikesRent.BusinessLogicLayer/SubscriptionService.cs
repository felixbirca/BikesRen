using System.Collections;
using BikesRent.BusinessLogicLayer.ViewModels;
using BikesRent.DataAccessLayer;
using BikesRent.DataAccessLayer.Entities;

namespace BikesRent.BusinessLogicLayer;

public class SubscriptionService : ISubscriptionService
{
    private readonly IEntityRepository _entityRepository;

    public SubscriptionService(IEntityRepository entityRepository)
    {
        _entityRepository = entityRepository;
    }

    public async Task<bool> CanRentABike(string userId)
    {
        var user = (await _entityRepository.Where<User>(x => x.Id == userId)).FirstOrDefault();
        var subscription = (await _entityRepository.Where<Subscription>(x => x.Id == user.SubscriptionId)).FirstOrDefault();

        if (user.SubscriptionExpiration < DateTimeOffset.Now)
        {
            return false;
        }
        
        var activeRents = await _entityRepository.Where<RentHistory>(x => x.UserId == user.Id && x.IsActive);
        
        if (activeRents.Count >= subscription.RentLimit)
        {
            return false;
        }

        return true;
    }

    public async Task RentBike(RentBikeModel rentBikeModel)
    {
        await _entityRepository.Create(new RentHistory
        {
            BikeId = rentBikeModel.BikeId,
            UserId = rentBikeModel.UserId,
            StartTime = DateTimeOffset.Now,
            IsActive = true
        });
    }

    public async Task EndRent(RentBikeModel rentBikeModel)
    {
        var activeRent = (await _entityRepository
                .Where<RentHistory>(x => x.UserId == rentBikeModel.UserId && x.BikeId == rentBikeModel.BikeId && x.IsActive))
                .FirstOrDefault();

        activeRent.IsActive = false;
        activeRent.EndTime = DateTimeOffset.Now;

        await _entityRepository.Update();
    }

    public async Task<ICollection<RentHistoryViewModel>> GetAll()
    {
        var result = await _entityRepository.GetAll<RentHistory>();
        var rentHistoryViewModels = new List<RentHistoryViewModel>();
        
        foreach (var rentItem in result)
        {
            rentHistoryViewModels.Add(new RentHistoryViewModel
            {
                Id = rentItem.Id,
                StartTime = rentItem.StartTime,
                EndTime = rentItem.EndTime,
                IsActive = rentItem.IsActive,
                User = rentItem.User,
                Bike = rentItem.Bike
            });
        }

        return rentHistoryViewModels;
    }
}