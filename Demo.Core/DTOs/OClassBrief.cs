using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Core.DTOs
{
    /// <summary>
    /// Class Brief has a few class information for displaying in Manage navigating
    /// </summary>
    public class OClassBrief
    {
        public int Id { get; set; }

        public int Year { get; set; }
        public string Term { get; set; }
        public string Name { get; set; }
        public string Level { get; set; }

        public int MaxNumber { get; set; }
        public decimal PriceIncGST { get; set; }

        public bool IsOnWebsite { get; set; }
        public bool IsValid { get; set; }
    }
}
