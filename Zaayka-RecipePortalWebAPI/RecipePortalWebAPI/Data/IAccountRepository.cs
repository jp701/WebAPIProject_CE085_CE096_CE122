using RecipePortalWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipePortalWebAPI.Data
{
    public interface IAccountRepository
    {
        string Register(User user);

        User Login(User user);
    }
}
