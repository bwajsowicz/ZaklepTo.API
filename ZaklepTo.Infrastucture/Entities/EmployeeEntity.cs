using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZaklepTo.Infrastructure.Entities
{
    public class EmployeeEntity : AbstractUserEntity
    {
        [ForeignKey("RestaurantId")]
        public RestaurantEntity RestaurantEntity { get; set; }
        public Guid RestaurantId { get; set; }
    }
}
