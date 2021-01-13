using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace ApiParking.Handler
{
    public interface IJwtAuthenticationManager
    {
        string Authenticate(string username, string password, int id);

    }
}
