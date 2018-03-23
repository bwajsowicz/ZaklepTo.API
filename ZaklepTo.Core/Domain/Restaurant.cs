using System;
using System.Collections.Generic;
using System.Text;
using ZaklepTo.Core.Exceptions;
using ZaklepTo.Core.Extensions;
using System.Text.RegularExpressions;

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

        protected Restaurant(string name, string description, string cuisine, string localization)
        {
            SetCousin(cuisine);
            SetDescription(description);
            SetName(name);
            SetLocalization(localization);
        }

        public void SetName(string name)
        {
            if(name.Empty())
            {
                throw new DomainException(ErrorCodes.InvalidRestaurantName, "Name can't be null or empty.");
            }
            if(Name == name)
            {
                return;
            }
            Name = name;
        }

        public void SetDescription(string description)
        {
            if (description.Empty())
            {
                throw new DomainException(ErrorCodes.InvalidDescription, "Description can't be null or empty.");
            }
            if (Description == description)
            {
                return;
            }
            Description = description;
        }

        public void SetCousin(string cuisine)
        {
            Regex CousinePattern = new Regex("[^a-zA-Z]");

            if (cuisine.Empty())
            {
                throw new DomainException(ErrorCodes.InvalidCousine, "Cousine can't be null or empty.");
            }
            else if (CousinePattern.IsMatch(cuisine))
            {
                throw new DomainException(ErrorCodes.InvalidCousine, "Cousine can't contain special characters or numbers.");
            }
            if (Cuisine == cuisine)
            {
                return;
            }
            Cuisine = cuisine;
        }

        public void SetLocalization(string localization)
        {
            if (localization.Empty())
            {
                throw new DomainException(ErrorCodes.InvalidLocalization, "Localization can't be null or empty.");
            }
            if (Localization == localization)
            {
                return;
            }
            Localization = localization;
        }

        public void SetTable(List<Table> _tables)
        {
        }
    }
}
