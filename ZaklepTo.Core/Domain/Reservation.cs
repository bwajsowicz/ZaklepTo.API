using System;

namespace ZaklepTo.Core.Domain
{
    public class Reservation
    {
        public Guid Id { get; set; }
        public Restaurant Restaurant { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public Table Table { get; set; }
        public Customer Customer { get; set; }
        public bool IsConfirmed { get; set; }
        private bool _isActive;

        protected Reservation()
        {
        }

        public Reservation(Restaurant restaurant, DateTime dateStart, DateTime dateEnd, 
            Table table, Customer customer, bool isConfirmed = false, bool isActive = true)
        {
            Id = Guid.NewGuid(); ;
            Restaurant = restaurant;
            DateStart = dateStart;
            DateEnd = dateEnd;
            Table = table;
            Customer = customer;
            IsConfirmed = isConfirmed;
            IsActive = isActive;
        }

        public bool IsActive
        {
            get => _isActive && DateTime.Now < DateEnd;
            private set => _isActive = value;
        }
    }
}
