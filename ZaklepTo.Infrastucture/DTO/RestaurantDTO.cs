using System;
using System.Collections.Generic;
using ZaklepTo.Core.Domain;

namespace ZaklepTo.Infrastructure.DTO
{
    public class RestaurantDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Cuisine { get; set; }
        public string Localization { get; set; }
        public IEnumerable<Table> Tables { get; set; }
    }
}
