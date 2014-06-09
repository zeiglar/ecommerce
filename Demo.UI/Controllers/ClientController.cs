using Demo.Core.DBs;
using Demo.Core.DTOs;
using Demo.Core.Enums;
using Demo.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Demo.UI.Controllers
{
    public class ClientController : Controller
    {
        DemoDBEntities _DemoDB;
        DateTime _Now = DateTime.Now;

        public ClientController(DemoDBEntities demoDB)
        {
            _DemoDB = demoDB;
        }

        public ActionResult Client(int type, int activityId)
        {
            if (activityId < 1)
            {
                return HttpNotFound();
            }

            ViewBag.Title = GetTitleDropDownList();
            ViewBag.Type = type;
            ViewBag.ActivityId = activityId;
            ViewBag.Action = "You detail";

            return View();
        }

        [HttpPost]
        public ActionResult Client(int type, int activityId, OUIClient ouiclient)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Title = GetTitleDropDownList();
                ViewBag.Type = type;
                ViewBag.ActivityId = activityId;
                ViewBag.Action = "You detail";
                return View();
            }

            Client client = new Client()
            {
                TitleTypeId = (int)ouiclient.Title,
                MemberTypeId = 1,                   //None
                FirstName = ouiclient.FirstName,
                LastName = ouiclient.LastName,
                Address = ouiclient.Address,
                Suburb = ouiclient.Suburb,
                Postcode = ouiclient.PostCode,
                Mobile = ouiclient.Mobile,
                HomePhone = ouiclient.HomePhone,
                WorkPhone = ouiclient.WorkPhone,
                Email = ouiclient.Email,
                DateUpdated = this._Now,
                DateCreated = this._Now
            };

            try
            {
                this._DemoDB.Clients.Add(client);
                this._DemoDB.SaveChanges();
            }
            catch(Exception e)
            {
                ModelState.AddModelError("", "Server is in the maintenance, please come back later.");
                ViewBag.Title = GetTitleDropDownList();
                ViewBag.Type = type;
                ViewBag.ActivityId = activityId;
                ViewBag.Action = "You detail";
                return View();
            }

            return RedirectToAction("Payment", new { type = type, activityId = activityId, clientId = client.Id });
        }

        public ActionResult Payment(int type, int activityId, int clientId)
        {
            ViewBag.Type = type;
            ViewBag.ActivityId = activityId;
            ViewBag.CardType = GetCardTypeDropDownList();
            ViewBag.ClientId = clientId;

            return View();
        }

        [HttpPost]
        public ActionResult Payment(int type, int activityId, int clientId, OPayment ouipayment)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Type = type;
                ViewBag.ActivityId = activityId;
                ViewBag.CardType = GetCardTypeDropDownList();
                ViewBag.ClientId = clientId;
                View();
            }

            Order order = new Order();
            order.ActivityId = activityId;
            order.ClientId = clientId;
            order.PriceTypeId = 1;
            order.PriceIncGST = ouipayment.Price;
            order.AmountPaid = ouipayment.Price;
            order.Balance = 0;
            order.IsSuccess = true;
            order.DateUpdated = this._Now;
            order.DateCreated = this._Now;
            this._DemoDB.Orders.Add(order);

            Activity activity = this._DemoDB.Activities.FirstOrDefault(s => s.Id == activityId);
            activity.Enrolled++;

            try
            {
                this._DemoDB.SaveChanges();
            }
            catch (Exception ex)
            {
                ViewBag.Type = type;
                ViewBag.ActivityId = activityId;
                ViewBag.CardType = GetCardTypeDropDownList();
                ViewBag.ClientId = clientId;
                ModelState.AddModelError("", "Server is in the maintenance, please come back later.");
                return View();
            }

            return RedirectToAction("Close");
        }

        public ActionResult Close()
        {
            return View();
        }

        private SelectList GetTitleDropDownList()
        {
            var values = from ETitle title in Enum.GetValues(typeof(ETitle))
                         select new { Id = title, Name = title.ToString() };

            return new SelectList(values, "Id", "Name", ETitle.Mr);
        }

        private SelectList GetCardTypeDropDownList()
        {
            var values = from ECard title in Enum.GetValues(typeof(ECard))
                         select new { Id = title, Name = title.ToString() };

            return new SelectList(values, "Id", "Name", ECard.Visa);
        }
    }
}
