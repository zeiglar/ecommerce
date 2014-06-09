using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Core.DTOs.UIs
{
    /// <summary>
    /// OMemberInvoice, in ohter word, is a non-class invoice
    /// it will be updated later when the requirement gets clearer.
    /// </summary>
    public class OInvoiceMember : OInvoice
    {
        #region Constructors
        public OInvoiceMember()
            : base()
        {
        }

        public OInvoiceMember(OInvoice invoice)
        {
            if (invoice != null)
            {
                Invoice = invoice.Invoice;
                ActivityId = invoice.ActivityId;
                DateCreated = invoice.DateCreated;
                PriceIncGST = invoice.PriceIncGST;
                AmountPaid = invoice.AmountPaid;
                Memo = invoice.Memo;
                IsSuccess = invoice.IsSuccess;

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
        #endregion

        //Term
        public int Year { get; set; }
        public string Name { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
    }
}