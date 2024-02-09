using BikesRent.BusinessLogicLayer.ViewModels;
using BikesRent.DataAccessLayer;
using BikesRent.DataAccessLayer.Entities;

namespace BikesRent.BusinessLogicLayer;

public class UserService : IUserService
{
    private readonly IEntityRepository<User> _userRepository;
    public UserService(IEntityRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ICollection<UserViewModel>> GetAll()
    {
        return (await _userRepository.GetAll()).
            Select(x => new UserViewModel
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
    }
}