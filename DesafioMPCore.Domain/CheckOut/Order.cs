using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioMPCore.Domain.CheckOut
{
    public class Order
    {
        public DateTime? CreateDate { get; set; }
        public string OrderKey { get; set; }
        public string OrderReference { get; set; }
    }
}
