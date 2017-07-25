using DesafioMPCore.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DesafioMPCore.Domain.Interface.Application
{
    public interface IAccessTokenApp
    {
        Task<Token> AuthenticateToCreateUserAccessToken(User user);
    }
}
