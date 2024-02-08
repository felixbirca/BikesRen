using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BikesRent.DataAccessLayer.Entities;

public class Subscription
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }
    
    public string Name { get; set; }
    
    public double Price { get; set; }
    
    public int RentLimit { get; set; }
    
    public virtual ICollection<User> Users { get; set; }
}