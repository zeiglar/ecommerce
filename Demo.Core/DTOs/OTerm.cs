using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Core.DTOs
{
    public class OTerm
    {
        private DateTime _StartDate = DateTime.Now;
        private DateTime _EndDate = DateTime.Now;
        public int Id { get; set; }

        [Required]
        [Display(Name = "Year", Order = 1)]
        [Range(2010, 2050)]
        public int Year { get; set; }

        [Required]
        [Display(Name = "Name", Order = 2)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Show On The Website", Order = 3)]
        public bool IsOnWebsite { get; set; }

        [Required]
        [Display(Name = "Start Date", Order = 4)]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime StartDate
        {
            get { return this._StartDate; }
            set { this._StartDate = value; }
        }

        [Required]
        [Display(Name = "End Date", Order = 5)]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime EndDate
        {
            get { return this._EndDate; }
            set { this._EndDate = value; }
        }

        //[Required]
        //[Display(Name = "One Day Event", Order = 6)]
        //public bool IsOneDayEvent { get; set; }

        //[Required]
        //[Display(Name = "Use Early Bird", Order = 7)]
        //public bool IsUseEarlyBird { get; set; }

        //[DataType(DataType.Date)]
        //[Display(Name = "Early Bird Date", Order = 8)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        //public DateTime? EarlyBirdDate { get; set; }
    }
}
