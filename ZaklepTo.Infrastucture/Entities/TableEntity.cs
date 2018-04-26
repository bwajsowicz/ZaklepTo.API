using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ZaklepTo.Infrastructure.Entities
{
    public class TableEntity
    {
        [Key]
        public Guid Id { get; set; }
        public int NumberOfSeats { get; set; }
        [NotMapped]
        public (int x, int y) Coordinates { get; set; }
        
        [ForeignKey("RestaurantId")]
        public RestaurantEntity RestaurantEntity { get; set; }
        public Guid RestaurantId { get; set; }
    }
}
