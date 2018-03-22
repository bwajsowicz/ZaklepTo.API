using System;
using System.Collections.Generic;
using System.Text;

namespace ZaklepTo.Infrastucture.DTO
{
    public class RestaurantDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Cousine { get; set; }
        public string Localization { get; set; }
        public IEnumerable<TableDTO> Tables { get; set; }
    }
}
