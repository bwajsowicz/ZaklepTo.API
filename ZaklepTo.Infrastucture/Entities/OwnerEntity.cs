using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZaklepTo.Infrastructure.Entities
{
    public class OwnerEntity : AbstractUserEntity
    {      
        [ForeignKey("RestaurantId")]
        public RestaurantEntity RestaurantEntity { get; set; }
        public Guid RestaurantId { get; set; }
    }
}
