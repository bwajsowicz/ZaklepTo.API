using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ZaklepTo.Infrastructure.Entities
{
    public class OwnerEntity : AbstractUserEntity
    {      
        [ForeignKey("RestaurantId")]
        public RestaurantEntity RestaurantEntity { get; set; }
        public Guid RestaurantId { get; set; }
    }
}
