using Demo.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Core.DTOs
{
    public class OPayment
    {
        public ECard CardType { get; set; }
        public string CardNumber { get; set; }
        public string ExpiryMonth { get; set; }
        public string SecurityCode { get; set; }
        public decimal Price { get; set; }
    }
}
