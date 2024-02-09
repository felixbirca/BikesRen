using BikesRent.BusinessLogicLayer.ViewModels;

namespace BikesRent.BusinessLogicLayer;

public interface IBikeService
{
    Task<ICollection<BikeViewModel>> GetAllBikes();
    Task CreateBike(CreateBikeModel bike);
    Task<ICollection<BikeViewModel>> GetAvailableBikes();
    Task UpdateBike(UpdateBikeModel model);
    Task<BikeViewModel> GetById(string id);
}