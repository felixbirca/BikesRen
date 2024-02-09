using BikesRent.DataAccessLayer.Entities;

namespace BikesRent.BusinessLogicLayer.ViewModels;

public class RentBikeViewModel
{
    public ICollection<BikeViewModel> AvailableBikes { get; set; } 
    public bool CanRentMore { get; set; }
}