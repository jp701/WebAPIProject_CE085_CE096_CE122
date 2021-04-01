using WebClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Data
{
    public interface IAccountRepository
    {
        string Register(User user);

        User Login(User user);

        User GetUser(int id);

        string UpdateUser(User user);
    }
}
