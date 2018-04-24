using System;
using System.Collections.Generic;
using System.Text;
using ZaklepTo.Infrastructure.DTO;

namespace ZaklepTo.Infrastructure.Services.Interfaces
{
    public interface IJwtHandler
    {
        JwtDto CreateToken(string login);
    }
}
