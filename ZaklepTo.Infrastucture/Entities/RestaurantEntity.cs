using System;
using System.ComponentModel.DataAnnotations;

namespace ZaklepTo.Infrastructure.Entities
{
    public class RestaurantEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Cuisine { get; set; }
        public string Localization { get; set; }
    }
}
