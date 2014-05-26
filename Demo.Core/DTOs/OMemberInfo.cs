using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Core.DTOs
{
    /// <summary>
    /// Member Info has the member information in Activities Navigating
    /// </summary>
    public class OMemberInfo
    {
        //Activity Id
        public int Id { get; set; }

        //Same as Id
        public int Code { get; set; }

        //From Term
        public int Year { get; set; }
        public string Term { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        //From Activity
        public string Name { get; set; }
        public int Enrolled { get; set; }
        public int MaxNumber { get; set; }
    }
}
