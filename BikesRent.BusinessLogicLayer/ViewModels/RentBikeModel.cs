namespace BikesRent.BusinessLogicLayer.ViewModels;

public class RentBikeModel
{
    public string UserId { get; set; }
    public string BikeId { get; set; }
    
    public bool CanRentMore { get; set; }
}