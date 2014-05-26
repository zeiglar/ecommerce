using Demo.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Core.DTOs
{
    public class OInvoice
    {
        //From Order
        public int Invoice { get; set; }
        public DateTime DateCreated { get; set; }
        public decimal PriceIncGST { get; set; }
        public decimal AmountPaid { get; set; }
        public string Memo { get; set; }
        public bool IsSuccess { get; set; }

        //From Client
        public ETitle Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Suburb { get; set; }
        public string PostCode { get; set; }
        public string Mobile { get; set; }
        public string HomePone { get; set; }
        public string WorkPhone { get; set; }
        public string Email { get; set; }
    }
}
