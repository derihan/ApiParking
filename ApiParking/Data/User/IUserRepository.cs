using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiParking.Data.User
{
    public interface IUserRepository
    {
        bool SaveChanges();
        int UserRegistration(Dictionary<string, string> data);
    }
}
