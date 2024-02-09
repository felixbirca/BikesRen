using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BikesRent.DataAccessLayer.Entities
{
    public class Bike : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        
        public string Type { get; set; }
        
        public string Brand { get; set; }
        
        public bool IsElectric { get; set; }
        
        public virtual ICollection<RentHistory> RentHistories { get; set; }
    }
}