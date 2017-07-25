using AutoMapper;
using DesafioMPCore.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioMPCore.API.AutoMapper
{
    public class AutoMapperProfileConfiguration : Profile
    {
        public AutoMapperProfileConfiguration() : this("MyProfile")
        {

        }
        protected AutoMapperProfileConfiguration(string profileName) : base(profileName)
        {
            CreateMap<Models.CreditCardTransaction, DesafioMPCore.Domain.CheckOut.TransactionCard>();
        }
    }
}
