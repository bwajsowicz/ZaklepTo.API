using System;
using System.Collections.Generic;
using System.Text;

namespace ZaklepTo.Infrastucture.DTO.OnCreate
{
    public class RestaurantOnCreateDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Cuisine { get; set; }
        public string Localization { get; set; }
        public IEnumerable<TableDTO> Tables { get; set; }
    }
}
