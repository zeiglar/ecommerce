using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Core.DTOs
{
    public class OUIMember
    {
        //Activity Id
        public int Code { get; set; }

        //From Term
        public int Year { get; set; }
        public string Term { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        //From Activity
        public string Name { get; set; }
        public decimal PriceIncGST { get; set; }
        public decimal? ConcessionPrice { get; set; }
        public decimal? MemberPrice { get; set; }
        public decimal? EarlyBirdPrice { get; set; }
        public DateTime? EarlyBirdDateTime { get; set; }
    }
}
