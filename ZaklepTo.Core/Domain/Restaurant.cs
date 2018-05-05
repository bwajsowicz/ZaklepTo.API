using System;
using System.Collections.Generic;

namespace ZaklepTo.Core.Domain
{
    public class Restaurant
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Cuisine { get; set; }
        public string Localization { get; set; }
        public string RepresentativePhotoUrl { get; set; }
        public IEnumerable<Table> Tables { get; set; }

        protected Restaurant()
        {
        }

        public Restaurant(string name, string description, string cuisine, string representativePhotoUrl, string localization, IEnumerable<Table> tables)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            Cuisine = cuisine;
            RepresentativePhotoUrl = representativePhotoUrl;
            Localization = localization;
            Tables = tables;
        }
    }
}
