using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Core.DTOs
{
    public class OUIClass : OUIMember
    {
        //From Level
        public string Level { get; set; }

        //Class
        public string DayTime { get; set; }
        public string Location { get; set; }
        public string Room { get; set; }
        public string Teacher { get; set; }

        //From Activity
        public int MaxNumber { get; set; }
    }
}
