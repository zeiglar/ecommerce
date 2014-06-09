using Demo.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Core.DTOs.UIs
{
    public class OClient
    {
        public int ClientId { get; set; }
        public ETitle Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Suburb { get; set; }
        public string PostCode { get; set; }
        public string Mobile { get; set; }
        public string HomePhone { get; set; }
        public string WorkPhone { get; set; }
        public string Email { get; set; }
    }
}