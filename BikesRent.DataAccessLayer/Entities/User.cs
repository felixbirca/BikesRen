using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BikesRent.DataAccessLayer.Entities;

public class User
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }
    
    public string Name { get; set; }
    
    public string Email { get; set; }
    
    public string? SubscriptionId { get; set; }
    
    public virtual Subscription Subscription { get; set; }
    
    public DateTimeOffset? SubscriptionExpiration { get; set; }
    
    public ICollection<RentHistory> RentHistories { get; set; }
}