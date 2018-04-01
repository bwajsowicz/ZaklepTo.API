namespace ZaklepTo.Infrastructure.DTO.OnUpdate
{
    public class PasswordChange
    {
        public string Login { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
