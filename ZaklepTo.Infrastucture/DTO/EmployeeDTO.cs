using System;
using System.Collections.Generic;
using System.Text;
using ZaklepTo.Core.Domain;

namespace ZaklepTo.Infrastucture.DTO
{
    class EmployeeDTO
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
