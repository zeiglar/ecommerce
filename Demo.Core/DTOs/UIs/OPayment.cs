using Demo.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Core.DTOs.UIs
{
    public class OPayment
    {
        public int OrderId { get; set; }
        public int PaymentType { get; set; }
        public decimal Paid { get; set; }
    }
}
