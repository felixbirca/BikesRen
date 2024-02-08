using BikesRent.DataAccessLayer.Entities;

namespace BikesRent.Web.Models;

public class RentHistoryViewModel
{
    public string Id { get; set; }
    
    public virtual User User { get; set; }
    
    public virtual Bike Bike { get; set; }
    
    public DateTimeOffset StartTime { get; set; }
    
    public DateTimeOffset? EndTime { get; set; }
    
    public bool IsActive { get; set; }
}