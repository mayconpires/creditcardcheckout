using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DesafioMPCore.API.Models
{
    public class Error
    {
        public string ErrorDescription { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }

        public static Error CreateInternalError(string errorDescription = "Erro no servidor", HttpStatusCode httpStatusCode = (HttpStatusCode)500) =>
            new Error()
            {
                ErrorDescription = errorDescription,
                HttpStatusCode = httpStatusCode,
            };
    }
}
