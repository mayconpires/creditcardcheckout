using DesafioMPCore.Domain.CheckOut;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DesafioMPCore.Domain.Interface.Application
{
    public interface IMerchantApp
    {
        Task<List<Merchant>> SeekMerchants(string userId);
    }
}
