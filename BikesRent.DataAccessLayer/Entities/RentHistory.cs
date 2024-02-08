using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BikesRent.DataAccessLayer.Entities;

public class RentHistory
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }
    
    public string UserId { get; set; }
    
    public virtual User User { get; set; }
    
    public string BikeId { get; set; }
    
    public virtual Bike Bike { get; set; }
    
    public DateTimeOffset StartTime { get; set; }
    
    public DateTimeOffset? EndTime { get; set; }
    
    public bool IsActive { get; set; }
}