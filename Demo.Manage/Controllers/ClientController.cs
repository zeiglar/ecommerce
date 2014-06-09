using Demo.Core.DBs;
using Demo.Core.DTOs.UIs;
using Demo.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Demo.Manage.Controllers
{
    public class ClientController : Controller
    {
        DemoDBEntities _DemoDB;
        DateTime _Now = DateTime.Now;

        public ClientController(DemoDBEntities demoDB)
        {
            _DemoDB = demoDB;
        }

        public ActionResult EditClient(int? clientId, int? orderId)
        {
            if (!clientId.HasValue)
            {
                return HttpNotFound();
            }

            Client client = this._DemoDB.Clients.FirstOrDefault(s => s.Id == clientId.Value);
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

            return PartialView(oClient);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditClient(int? clientId, int? orderId, OClient oClient)
        {
            if (!ModelState.IsValid)
            {
                return View(oClient);
            }

            if (clientId.HasValue)
            {
                //update
                Client client = this._DemoDB.Clients.FirstOrDefault(s => s.Id == clientId.Value);

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
    }
}
