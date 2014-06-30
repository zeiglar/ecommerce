using Demo.Core.DBs;
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
                                            .Select(s => new Invoice { PaymentId = s.Id, Paid = s.Paid, PaymentType = (EPayment)s.PaymentType.Code, AuthoredBy = s.AuthoredBy })
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ShowInvoice(OInvoice oInvoice)
        {
            return null;
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
        public ActionResult NewInvoice(int orderId)
        {
            Demo.Core.DTOs.UIs.OPayment oPayment = new Core.DTOs.UIs.OPayment();
            oPayment.OrderId = orderId;

            PaymentTypeDropDownList();

            return PartialView(oPayment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewInvoice(Demo.Core.DTOs.UIs.OPayment oPayment)
        {
            if (!ModelState.IsValid)
            {
                PartialView(oPayment);
            }

            //Add new payment
            this._DemoDB.Payments.Add(new Payment()
            {
                PaymentTypeId = oPayment.PaymentType,
                OrderId = oPayment.OrderId,
                Paid = oPayment.Paid,
                DateUpdated = this._Now,
                DateCreated = this._Now
            });

            this._DemoDB.SaveChanges();

            //Update total payment
            Order order = this._DemoDB.Orders.FirstOrDefault(s => s.Id == oPayment.OrderId);
            order.AmountPaid += oPayment.Paid;
            order.Balance = order.AmountPaid - order.PriceIncGST;
            order.DateUpdated = this._Now;

            this._DemoDB.SaveChanges();

            //Add new payment history
            this._DemoDB.OrderHistories.Add(new OrderHistory()
            {
                OrderId = oPayment.OrderId,
                Content = string.Format("{0} has been paid by {1}", oPayment.Paid, (EPayment)oPayment.PaymentType),
                DateUpdated = this._Now,
                DateCreated = this._Now
            });

            this._DemoDB.SaveChanges();

            return RedirectToAction("ShowInvoice", new { id = oPayment.OrderId });
        }
        #endregion

        #region Edit Client
        public ActionResult EditClient(int orderId)
        {
            var invoice = this._DemoDB.Orders.FirstOrDefault(s => s.Id == orderId);
            Client client = this._DemoDB.Clients.FirstOrDefault(s => s.Id == invoice.ClientId);
            if (client == null)
                return HttpNotFound();

            OClient oClient = new OClient()
            {
                Title = (ETitle)client.TitleType.Code,
                FirstName = client.FirstName,
                LastName = client.LastName,
                Address = client.Address,
                Suburb = client.Suburb,
                PostCode = client.Postcode,
                Mobile = client.Mobile,
                HomePhone = client.HomePhone,
                WorkPhone = client.WorkPhone,
                Email = client.Email
            };

            @ViewBag.Action = "Edit Client";
            @ViewBag.OrderId = orderId;
            @ViewBag.ClientId = invoice.ClientId;

            return PartialView(oClient);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditClient(int? orderId, OClient oClient)
        {
            if (!ModelState.IsValid)
            {
                return View(oClient);
            }

            if (orderId.HasValue)
            {
                //update
                var invoice = this._DemoDB.Orders.FirstOrDefault(s => s.Id == orderId);
                Client client = this._DemoDB.Clients.FirstOrDefault(s => s.Id == invoice.ClientId);

                client.TitleTypeId = (int)oClient.Title;
                client.MemberTypeId = 1;                   //None
                client.FirstName = oClient.FirstName;
                client.LastName = oClient.LastName;
                client.Address = oClient.Address;
                client.Suburb = oClient.Suburb;
                client.Postcode = oClient.PostCode;
                client.Mobile = oClient.Mobile;
                client.HomePhone = oClient.HomePhone;
                client.WorkPhone = oClient.WorkPhone;
                client.Email = oClient.Email;
                client.DateUpdated = this._Now;
            }
            else
            {
                //create
                Client client = new Client()
                {
                    TitleTypeId = (int)oClient.Title,
                    MemberTypeId = 1,                   //None
                    FirstName = oClient.FirstName,
                    LastName = oClient.LastName,
                    Address = oClient.Address,
                    Suburb = oClient.Suburb,
                    Postcode = oClient.PostCode,
                    Mobile = oClient.Mobile,
                    HomePhone = oClient.HomePhone,
                    WorkPhone = oClient.WorkPhone,
                    Email = oClient.Email,
                    DateUpdated = this._Now,
                    DateCreated = this._Now
                };
                this._DemoDB.Clients.Add(client);
            }

            try
            {
                this._DemoDB.SaveChanges();
            }
            catch (Exception ex)
            {
                //TODO: handle ex
            }

            if (orderId.HasValue)
                return RedirectToAction("ShowInvoice", "Invoice", new { id = orderId });

            return RedirectToAction("Invoices", "Invoice");
        }
        #endregion

        private void PaymentTypeDropDownList()
        {
            var types = from EPayment p in Enum.GetValues(typeof(EPayment))
                        select new { Id = (int)p, Type = p.ToString() };

            ViewBag.PaymentType = new SelectList(types, "Id", "Type", null); ;
        }

        #endregion
    }
}
