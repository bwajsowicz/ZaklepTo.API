using System;
using System.Collections.Generic;
using System.Text;

namespace ZaklepTo.Core.Domain
{
    public class Restaurant
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Cuisine { get; private set; }
        public string Localization { get; private set; }
        private List<Table> _tables { get; set; }
        public IEnumerable<Table> Tables => _tables;

        public Restaurant(Guid id, string name, string description, string cuisine, string localization, List<Table> tables)
        {
            Id = id;
            Name = name;
            Description = description;
            Cuisine = cuisine;
            Localization = localization;
            _tables = tables;
        }
    }
}
