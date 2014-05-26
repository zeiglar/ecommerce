using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Core.DTOs
{
    /// <summary>
    /// Class has enough information to CRUD in real life
    /// </summary>
    public class OClass
    {
        public int Id { get; set; }
        
        //From term
        [Required]
        public int Year { get; set; }
        [Required]
        public string Term { get; set; }

        //From Level
        [Required]
        public string Level { get; set; }

        //From Activity
        [Required]
        public string Name { get; set; }

        [Required]
        public bool IsValid { get; set; }
        [Required]
        public bool IsOnWebiste { get; set; }

        public int MaxNumber { get; set; }

        //Class
        [Required]
        public string DayTime { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public string Room { get; set; }
        public string Teacher { get; set; }

        //From Activity
        [Required]
        public decimal PriceWithGST { get; set; }

        public decimal? ConcessionPrice { get; set; }
        public decimal? MemberPrice { get; set; }

        public decimal? EarlyBirdPrice { get; set; }
        public DateTime? EarlyBirdDateTime { get; set; }
    }
}
