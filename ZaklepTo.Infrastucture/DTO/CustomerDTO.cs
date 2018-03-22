using System;
using System.Collections.Generic;
using System.Text;

namespace ZaklepTo.Infrastucture.DTO
{
    public class CustomerDTO
    {
        public string Login { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
