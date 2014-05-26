using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Core.DTOs
{
    public class OLevel
    {
        public int Id { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string Subcategory { get; set; }

        [Required]
        [StringValidator(InvalidCharacters = " ~!@#$%^&*()[]{}/;'\"|\\",
            MinLength = 1, MaxLength = 60)]
        public string Subject { get; set; }

        [Required]
        public bool IsOnWebsite { get; set; }

        [Required]
        public bool IsValid { get; set; }
    }
}
