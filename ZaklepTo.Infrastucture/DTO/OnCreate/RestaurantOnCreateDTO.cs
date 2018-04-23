﻿using System.Collections.Generic;
using ZaklepTo.Core.Domain;

namespace ZaklepTo.Infrastructure.DTO.OnCreate
{
    public class RestaurantOnCreateDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Cuisine { get; set; }
        public string Localization { get; set; }
        public IEnumerable<Table> Tables { get; set; }
    }
}
