using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ZaklepTo.Core.Domain;

namespace ZaklepTo.Infrastructure.Entities
{
    public class ReservationEntity
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public bool IsConfirmed { get; set; }
        public bool IsActive { get; set; }

        [ForeignKey("RestaurantId")]
        public RestaurantEntity Restaurant { get; set; }
        public Guid RestaurantId { get; set; }

        [ForeignKey("TableId")]
        public TableEntity Table { get; set; }
        public Guid TableId { get; set; }

        [ForeignKey("CustomerLogin")]
        public CustomerEntity Customer { get; set; }
        public string CustomerLogin { get; set; }
    }
}
