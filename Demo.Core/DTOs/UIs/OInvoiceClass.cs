using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Core.DTOs.UIs
{
    public class OInvoiceClass : OInvoiceMember
    {
        #region Constructors
        public OInvoiceClass()
            : base()
        {
        }

        public OInvoiceClass(OInvoice invoice)
        {
            if (invoice != null)
            {
                Invoice = invoice.Invoice;
                DateCreated = invoice.DateCreated;
                PriceIncGST = invoice.PriceIncGST;
                AmountPaid = invoice.AmountPaid;
                Memo = invoice.Memo;
                IsSuccess = invoice.IsSuccess;
                OrderSteps = invoice.OrderSteps;

                //From Client
                ClientId = invoice.ClientId;
                Title = invoice.Title;
                FirstName = invoice.FirstName;
                LastName = invoice.LastName;
                Address = invoice.Address;
                Suburb = invoice.Suburb;
                PostCode = invoice.PostCode;
                Mobile = invoice.Mobile;
                HomePone = invoice.HomePone;
                WorkPhone = invoice.WorkPhone;
                Email = invoice.Email;
            }
        }

        public OInvoiceClass(OInvoiceMember invoiceMember)
        {
            if (invoiceMember != null)
            {
                Invoice = invoiceMember.Invoice;
                DateCreated = invoiceMember.DateCreated;
                PriceIncGST = invoiceMember.PriceIncGST;
                AmountPaid = invoiceMember.AmountPaid;
                Memo = invoiceMember.Memo;
                IsSuccess = invoiceMember.IsSuccess;
                OrderSteps = invoiceMember.OrderSteps;

                //From Client
                ClientId = invoiceMember.ClientId;
                Title = invoiceMember.Title;
                FirstName = invoiceMember.FirstName;
                LastName = invoiceMember.LastName;
                Address = invoiceMember.Address;
                Suburb = invoiceMember.Suburb;
                PostCode = invoiceMember.PostCode;
                Mobile = invoiceMember.Mobile;
                HomePone = invoiceMember.HomePone;
                WorkPhone = invoiceMember.WorkPhone;
                Email = invoiceMember.Email;

                //Term
                Year = invoiceMember.Year;
                Name = invoiceMember.Name;
                DateStart = invoiceMember.DateStart;
                DateEnd = invoiceMember.DateEnd;
            }
        }
        #endregion

        //Class
        public string DayTime { get; set; }
        public string Location { get; set; }
        public string Room { get; set; }
        public string Teacher { get; set; }
        public string Duration { get; set; }

        //Level
        public string Category { get; set; }
        public string Subcategory { get; set; }
        public string Subject { get; set; }
        public string Level { get; set; }
    }
}
