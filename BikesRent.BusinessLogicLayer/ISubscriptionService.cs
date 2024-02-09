using BikesRent.BusinessLogicLayer.ViewModels;

namespace BikesRent.BusinessLogicLayer;

public interface ISubscriptionService
{
    Task<bool> CanRentABike(string userId);
    Task RentBike(RentBikeModel rentBikeModel);
    Task EndRent(RentBikeModel rentBikeModel);
    Task<ICollection<RentHistoryViewModel>> GetAll();
}