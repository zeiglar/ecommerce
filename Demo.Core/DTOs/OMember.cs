using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Core.DTOs
{
    /// <summary>
    /// Member has a few member information for displaying in Manage navigating
    /// </summary>
    public class OMember
    {
        public int Id { get; set; }

        [Required]
        public int Year { get; set; }
        [Required]
        public string Term { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public bool IsValid { get; set; }

        public int MaxNumber { get; set; }

        [Required]
        public decimal PriceWithGST { get; set; }

        public decimal? ConcessionPrice { get; set; }
        public decimal? MemberPrice { get; set; }

        public decimal? EarlyBirdPrice { get; set; }
        public DateTime? EarlyBirdDateTime { get; set; }
    }
}
