using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RecipePortalWebAPI.Models;

namespace RecipePortalWebAPI.Data
{
    public class SQLAccountRepository: IAccountRepository
    {
        private readonly AppDbContext context;

        public SQLAccountRepository(AppDbContext context)
        {
            this.context = context;
        }

        public string Register(User user)
        {
            var status = context.Users.Any(u=> u.email==user.email);
            if (status == true) //user already exists
            {
                return "User with this email-id already exists..";
            }
            else
            {
                context.Users.Add(user);
                context.SaveChanges();
                return "success";
            }
        }
    }
}
