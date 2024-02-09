using BikesRent.BusinessLogicLayer.ViewModels;

namespace BikesRent.BusinessLogicLayer;

public interface IUserService
{
    Task<ICollection<UserViewModel>> GetAll();
}