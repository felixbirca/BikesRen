using System.Collections;
using BikesRent.BusinessLogicLayer.ViewModels;
using BikesRent.DataAccessLayer;
using BikesRent.DataAccessLayer.Entities;

namespace BikesRent.BusinessLogicLayer;

public class SubscriptionService : ISubscriptionService
{
    private readonly IEntityRepository<Subscription> _subscriptionRepository;
    private readonly IEntityRepository<Bike> _bikeRepository;
    private readonly IEntityRepository<User> _userRepository;
    private readonly IEntityRepository<RentHistory> _rentHistoryRepository;

    public SubscriptionService(IEntityRepository<Subscription> subscriptionRepository, 
             IEntityRepository<Bike> bikeRepository,
             IEntityRepository<User> userRepository,
             IEntityRepository<RentHistory> rentHistoryRepository)
    {
        _subscriptionRepository = subscriptionRepository;
        _bikeRepository = bikeRepository;
        _userRepository = userRepository;
        _rentHistoryRepository = rentHistoryRepository;
    }

    public async Task<bool> CanRentABike(string userId)
    {
        var user = (await _userRepository.Where(x => x.Id == userId)).FirstOrDefault();
        var subscription = (await _subscriptionRepository.Where(x => x.Id == user.SubscriptionId)).FirstOrDefault();

        if (user.SubscriptionExpiration < DateTimeOffset.Now)
        {
            return false;
        }
        
        var activeRents = await _rentHistoryRepository.Where(x => x.UserId == user.Id && x.IsActive);
        
        if (activeRents.Count >= subscription.RentLimit)
        {
            return false;
        }

        return true;
    }

    public async Task RentBike(RentBikeModel rentBikeModel)
    {
        await _rentHistoryRepository.Create(new RentHistory
        {
            BikeId = rentBikeModel.BikeId,
            UserId = rentBikeModel.UserId,
            StartTime = DateTimeOffset.Now,
            IsActive = true
        });
    }

    public async Task EndRent(RentBikeModel rentBikeModel)
    {
        var activeRent = (await _rentHistoryRepository
                .Where(x => x.UserId == rentBikeModel.UserId && x.BikeId == rentBikeModel.BikeId && x.IsActive))
                .FirstOrDefault();

        activeRent.IsActive = false;
        activeRent.EndTime = DateTimeOffset.Now;

        await _rentHistoryRepository.Update();
    }

    public async Task<ICollection<RentHistoryViewModel>> GetAll()
    {
        var result = await _rentHistoryRepository.GetAll();
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