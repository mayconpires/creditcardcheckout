using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioMPCore.Domain.Shared
{
    public class Token
    {
        public string AccessToken { get; set; }

        public string TokenType { get; set; }

        public int ExpiresIn { get; set; }

        public string UserId { get; set; }

        public string Name { get; set; }

        public string RefreshToken { get; set; }

        public string Username { get; set; }

        public string CustomerKey { get; set; }

    }
}
