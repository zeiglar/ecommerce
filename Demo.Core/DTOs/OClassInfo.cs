using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Core.DTOs
{
    /// <summary>
    /// Class Info has the class information in Activities Navigating
    /// </summary>
    public class OClassInfo
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

        //From Class
        public string DayTime { get; set; }
        public string Location { get; set; }
        public string Room { get; set; }
        public string Teacher { get; set; }

        //From Activity
        public string Name { get; set; }
        public int Enrolled { get; set; }
        public int MaxNumber { get; set; }

        //From Level
        public string Level { get; set; }
    }

    public class OClassInfoDisplay
    {
        public int Code { get; set; }
        public string Date { get; set; }
        public string Detail { get; set; }
        public string Numbers { get; set; }
    }
}
