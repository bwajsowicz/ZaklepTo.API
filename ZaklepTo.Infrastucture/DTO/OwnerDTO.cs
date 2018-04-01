using System;

namespace ZaklepTo.Infrastructure.DTO
{
    public class OwnerDTO
    {
        public string Login { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime CreatedAt { get; set; }
        public RestaurantDTO Restaurant { get; set; }
    }
}
