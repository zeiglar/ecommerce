using Demo.Core.DBs;
using Demo.Core.DTOs;
using Demo.Core.Enums;
using Demo.Core.Helpers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Demo.Manage.Controllers
{
    public class InvoiceController : Controller
    {
        DemoDBEntities _DemoDB;
        DateTime _Now = DateTime.Now;

        public InvoiceController(DemoDBEntities demoDB)
        {
            _DemoDB = demoDB;
        }

        #region Invoice
        #region Invoice List
        public ActionResult Invoices(int pageIndex = 1)
        {
            var models = from orders in this._DemoDB.Orders
                         join clients in this._DemoDB.Clients
                         on orders.ClientId equals clients.Id
                         orderby orders.DateCreated descending
                         select new OInvoice
                         {
                             Invoice = orders.Id,
                             DateCreated = orders.DateCreated,
                             PriceIncGST = orders.PriceIncGST,
                             AmountPaid = orders.AmountPaid,
                             Memo = orders.Memo,
                             IsSuccess = orders.IsSuccess,

                             Title = (ETitle)clients.TitleType.Code,
                             FirstName = clients.FirstName,
                             LastName = clients.LastName,
                             Address = clients.Address,
                             Suburb = clients.Suburb,
                             PostCode = clients.Postcode,
                             Mobile = clients.Mobile,
                             HomePone = clients.HomePone,
                             WorkPhone = clients.WorkPhone,
                             Email = clients.Email
                         };

            ViewBag.PropertyInfo = models.ElementType.GetProperties();
            models = models.Processing(Request.QueryString);

            return PartialView(models.ToPagedList(pageIndex));
        }
        #endregion

        #region New Invoice
        public ActionResult NewInvoices()
        {
            return PartialView();
        }
        #endregion
        #endregion
    }
}
