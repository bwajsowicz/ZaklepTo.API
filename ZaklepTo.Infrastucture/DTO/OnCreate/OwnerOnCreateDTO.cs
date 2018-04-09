namespace ZaklepTo.Infrastructure.DTO.OnCreate
{
    public class OwnerOnCreateDTO
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public RestaurantOnCreateDTO Restaurant { get; set; }
    }
}
