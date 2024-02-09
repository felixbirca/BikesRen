using BikesRent.BusinessLogicLayer.ViewModels;
using BikesRent.DataAccessLayer;
using BikesRent.DataAccessLayer.Entities;

namespace BikesRent.BusinessLogicLayer;

public class UserService : IUserService
{
    private readonly IEntityRepository _entityRepository;
    public UserService(IEntityRepository entityRepository)
    {
        _entityRepository = entityRepository;
    }

    public async Task<ICollection<UserViewModel>> GetAll()
    {
        return (await _entityRepository.GetAll<User>()).
            Select(x => new UserViewModel
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
    }
}