using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalLibrary.Server.Services
{
    public interface IJwtTokenService
    {
        string BuildToken(string email, string idString);
    }
}
