using ZaklepTo.Infrastructure.DTO;

namespace ZaklepTo.Infrastructure.Services.Interfaces
{
    public interface IJwtService
    {
        JwtDto CreateToken(string login, string role);
    }
}
