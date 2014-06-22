﻿using Demo.Core.DBs;
using Demo.Core.DTOs;
using Demo.Core.DTOs.UIs;
using Demo.Core.Enums;
using Demo.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
                             HomePone = clients.HomePhone,
                             WorkPhone = clients.WorkPhone,
                             Email = clients.Email
                         };

            ViewBag.PropertyInfo = models.ElementType.GetProperties();
            models = models.Processing(Request.QueryString);

            return PartialView(models.ToPagedList(pageIndex));
        }

        public ActionResult ShowInvoice(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            OInvoice invoice =
                            (from orders in this._DemoDB.Orders
                             join clients in this._DemoDB.Clients
                             on orders.ClientId equals clients.Id
                             where orders.Id == id.Value
                             select new OInvoice
                             {
                                 //order
                                 Invoice = orders.Id,
                                 DateCreated = orders.DateCreated,
                                 PriceIncGST = orders.PriceIncGST,
                                 AmountPaid = orders.AmountPaid,
                                 Memo = orders.Memo,
                                 IsSuccess = orders.IsSuccess,

                                 //Client
                                 ClientId = clients.Id,
                                 Title = (ETitle)clients.TitleType.Code,
                                 FirstName = clients.FirstName,
                                 LastName = clients.LastName,
                                 Address = clients.Address,
                                 Suburb = clients.Suburb,
                                 PostCode = clients.Postcode,
                                 Mobile = clients.Mobile,
                                 HomePone = clients.HomePhone,
                                 WorkPhone = clients.WorkPhone,
                                 Email = clients.Email
                             }).FirstOrDefault();

            invoice.OrderSteps = this._DemoDB.OrderHistories
                                            .Where(s => s.OrderId == invoice.Invoice)
                                            .Select(s => new OrderStep { OrderHistoryId = s.Id, Record = s.Content, DateCreated = s.DateUpdated })
                                            .ToList();

            invoice.Invoices = this._DemoDB.Payments
                                            .Where(s => s.OrderId == invoice.Invoice)
                                            .Select(s => new Invoice { PaymentId = s.Id, Paid = s.Paid, PaymentType = (Epayment)s.PaymentType.Code, AuthoredBy = s.AuthoredBy })
                                            .ToList();
            invoice.Shoppings =
                                (from orders in this._DemoDB.Orders
                                 join ordacts in this._DemoDB.OrderActivities on orders.Id equals ordacts.OrderId
                                 join activities in this._DemoDB.Activities on ordacts.ActivityId equals activities.Id
                                 where orders.Id == id.Value
                                 select new Shopping
                                 {
                                     ActivityId = activities.Id,
                                     Name = activities.Name,
                                     PriceType = (EPrice)ordacts.PriceType.Code,
                                     PriceIncGST = ordacts.PriceIncGST,
                                     TotalIncGST = ordacts.TotalIncGST
                                 }).ToList();

            //TempData["Invoice"] = invoice;
            //switch (this._DemoDB.Activities.Where(s => s.Id == invoice.ActivityId).Select(s => s.ActivityTypeId).FirstOrDefault())
            //{
            //    case (int)EActivity.Class:
            //        return RedirectToAction("ShowInvoiceClass");
            //    case (int)EActivity.Member:
            //        return RedirectToAction("ShowInvoiceMember");
            //    case (int)EActivity.Event:
            //        break;
            //    case (int)EActivity.Default:
            //        break;
            //    default:
            //        break;
            //}

            return PartialView(invoice);
        }

        public ActionResult ShowInvoiceClass()
        {
            OInvoice invoice = (OInvoice)TempData["Invoice"];
            OInvoiceClass invoiceClass = new OInvoiceClass(invoice);

            var activity = (from activities in this._DemoDB.Activities
                            join terms in this._DemoDB.Terms on activities.TermId equals terms.Id
                            join classes in this._DemoDB.Classes on activities.ClassId equals classes.Id
                            join levels in this._DemoDB.Levels on classes.LevelId equals levels.Id
                            join ordacts in this._DemoDB.OrderActivities on activities.Id equals ordacts.ActivityId
                            where ordacts.OrderId == invoice.Invoice
                            select new
                            {
                                //Term
                                Year = terms.Year,
                                Name = terms.Name,
                                DateStart = terms.DateStart,
                                DateEnd = terms.DateEnd,

                                //Class
                                DayTime = classes.DayTime,
                                Location = classes.Location,
                                Room = classes.Room,
                                Teacher = classes.Teacher,
                                Duration = classes.Duration,

                                //Level
                                Category = levels.Category,
                                Subcategory = levels.Subcategory,
                                Subject = levels.Subject,
                                Level = levels.Level1
                            }).FirstOrDefault();

            //Term
            invoiceClass.Year = activity.Year;
            invoiceClass.Name = activity.Name;
            invoiceClass.DateStart = activity.DateStart;
            invoiceClass.DateEnd = activity.DateEnd;

            //Class
            invoiceClass.DayTime = activity.DayTime;
            invoiceClass.Location = activity.Location;
            invoiceClass.Room = activity.Room;
            invoiceClass.Teacher = activity.Teacher;
            invoiceClass.Duration = activity.Duration;

            //Level
            invoiceClass.Category = activity.Category;
            invoiceClass.Subcategory = activity.Subcategory;
            invoiceClass.Subject = activity.Subject;
            invoiceClass.Level = activity.Level;

            @ViewBag.Action = "Invoice Detail - Class";
            return PartialView(invoiceClass);
        }

        public ActionResult ShowInvoiceMember()
        {
            OInvoice invoice = (OInvoice)TempData["Invoice"];
            OInvoiceMember invoiceMember = new OInvoiceMember(invoice);

            var activity = (from activities in this._DemoDB.Activities
                            join terms in this._DemoDB.Terms on activities.TermId equals terms.Id
                            join ordacts in this._DemoDB.OrderActivities on activities.Id equals ordacts.ActivityId
                            where ordacts.OrderId == invoice.Invoice
                            select new
                            {
                                //Term
                                Year = terms.Year,
                                Name = terms.Name,
                                DateStart = terms.DateStart,
                                DateEnd = terms.DateEnd
                            }).FirstOrDefault();

            invoiceMember.Year = activity.Year;
            invoiceMember.Name = activity.Name;
            invoiceMember.DateStart = activity.DateStart;
            invoiceMember.DateEnd = activity.DateEnd;

            @ViewBag.Action = "Invoice Detail - Member";
            return PartialView(invoiceMember);
        }

        public ActionResult UpdateMemo(int orderId)
        {
            var invoice = this._DemoDB.Orders.FirstOrDefault(s => s.Id == orderId);
            if (invoice == null)
                return HttpNotFound();

            OMemo memo = new OMemo()
            {
                Id = invoice.Id,
                Memo = invoice.Memo
            };

            @ViewBag.Action = "Invoice Memo Update";
            @ViewBag.OrderId = invoice.Id;
            return PartialView(memo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateMemo(int? orderId, OMemo oMemo)
        {
            if (!orderId.HasValue)
                return HttpNotFound();

            if (!ModelState.IsValid)
            {
                @ViewBag.Action = "Invoice Memo Update";
                @ViewBag.OrderId = orderId;
                return PartialView(oMemo);
            }

            var invoice = this._DemoDB.Orders.FirstOrDefault(s => s.Id == orderId);
            invoice.Memo = oMemo.Memo;

            try
            {
                this._DemoDB.SaveChanges();
            }
            catch (Exception ex)
            {
                //TODO: handle ex
            }

            return RedirectToAction("ShowInvoice", "Invoice", new { id = orderId });
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
