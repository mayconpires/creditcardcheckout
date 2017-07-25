using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioMPCore.API.Models
{
    public class User
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public Domain.Shared.User ToDomain()
        {
            var user = new Domain.Shared.User()
            {
                UserName = UserName,
                Password = Password
            };
            return user;
        }
    }
}
