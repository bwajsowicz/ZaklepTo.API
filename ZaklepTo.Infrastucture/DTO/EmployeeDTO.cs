using System;

namespace ZaklepTo.Infrastructure.DTO
{
    public class EmployeeDto
    {
        public string Login { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime CreatedAt { get; set; }
        public RestaurantDto Restaurant { get; set; }
    }
}
