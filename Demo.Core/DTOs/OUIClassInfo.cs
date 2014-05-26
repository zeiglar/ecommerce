using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Core.DTOs
{
    public class OUIClassInfo
    {
        //Activity Id
        public int Code { get; set; }

        //From Activity
        public string Name { get; set; }
        public decimal PriceIncGST { get; set; }
        public decimal? ConcessionPrice { get; set; }
        public decimal? MemberPrice { get; set; }
        public decimal? EarlyBirdPrice { get; set; }
        public DateTime? EarlyBirdDateTime { get; set; }

        //From Level
        public string Level { get; set; }

        //Class
        public string DayTime { get; set; }
        public string Location { get; set; }
        public string Room { get; set; }
        public string Teacher { get; set; }
    }
}
