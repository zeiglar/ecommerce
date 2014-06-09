using Demo.Core.DBs;
using Demo.Core.DTOs;
using Demo.Core.Enums;
using Demo.Core.Helpers;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Demo.Manage.Controllers
{
    public class ProductController : Controller
    {
        DemoDBEntities _DemoDB;
        DateTime _Now = DateTime.Now;

        public ProductController(DemoDBEntities demoDB)
        {
            _DemoDB = demoDB;
        }

        #region Product
        #region Classes
        public ActionResult Classes(int pageIndex = 1)
        {
            var models = from activities in this._DemoDB.Activities
                         join classes in this._DemoDB.Classes
                         on activities.ClassId equals classes.Id
                         join levels in this._DemoDB.Levels
                         on classes.LevelId equals levels.Id
                         join terms in this._DemoDB.Terms
                         on activities.TermId equals terms.Id
                         where !activities.IsDeleted && activities.ActivityType.Code == (int)EActivity.Class
                         orderby activities.DateCreated descending
                         select new OClassInfo
                         {
                             Id = activities.Id,
                             Code = activities.Id,

                             Year = terms.Year,
                             Term = terms.Name,
                             StartDate = terms.DateStart,
                             EndDate = terms.DateEnd,

                             DayTime = classes.DayTime,
                             Location = classes.Location,
                             Room = classes.Room,
                             Teacher = classes.Teacher,

                             Name = activities.Name,
                             Enrolled = activities.Enrolled,
                             MaxNumber = activities.MaxNumber,

                             Level = levels.Level1
                         };


            ViewBag.PropertyInfo = models.ElementType.GetProperties();
            models = models.Processing(Request.QueryString);

            return PartialView(models.ToPagedList(pageIndex));
        }
        #endregion

        #region Events
        public ActionResult Events()
        {
            return PartialView();
        }
        #endregion

        #region Memberships
        public ActionResult Memberships(int pageIndex = 1)
        {
            var models = from activities in this._DemoDB.Activities
                         join terms in this._DemoDB.Terms
                         on activities.TermId equals terms.Id
                         where !activities.IsDeleted && activities.ActivityType.Code == 2
                         orderby activities.DateCreated descending
                         select new OMemberInfo
                         {

                             Id = activities.Id,
                             Code = activities.Id,

                             //From terms
                             Year = terms.Year,
                             Term = terms.Name,
                             StartDate = terms.DateStart,
                             EndDate = terms.DateEnd,

                             //From activies
                             Name = activities.Name,
                             Enrolled = activities.Enrolled,
                             MaxNumber = activities.MaxNumber,
                         };

            ViewBag.PropertyInfo = models.ElementType.GetProperties();
            models = models.Processing(Request.QueryString);

            return PartialView(models.ToPagedList(pageIndex));
        }
        #endregion
        #endregion

        #region Common
        public ActionResult EnrolledInfo(int type, int? activityId, int pageIndex = 1)
        {
            if (!activityId.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var models = from activities in this._DemoDB.Activities
                         join orders in this._DemoDB.Orders
                         on activities.Id equals orders.ActivityId
                         join clients in this._DemoDB.Clients
                         on orders.ClientId equals clients.Id
                         where activities.Id == activityId.Value && orders.IsSuccess
                         orderby activities.DateCreated descending
                         select new OEnrolledInfo
                         {
                             Invoice = orders.Id,
                             DateCreated = orders.DateCreated,
                             Price = orders.PriceIncGST,
                             PriceType = (EPrice)orders.PriceType.Code,
                             Memo = orders.Memo,

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

            switch (type)
            {
                case 1:
                    ViewBag.Source = "Classes";
                    break;
                case 2:
                    ViewBag.Source = "Memberships";
                    break;
                case 3:
                    ViewBag.Source = "Events";
                    break;
                default:
                    ViewBag.Source = "Classes";
                    break;
            }
            ViewBag.ActivityId = activityId;

            return PartialView(models.ToPagedList(pageIndex));
        }

        public void Download(int activityId, string name)
        {
            var models = from activities in this._DemoDB.Activities
                         join orders in this._DemoDB.Orders
                         on activities.Id equals orders.ActivityId
                         join clients in this._DemoDB.Clients
                         on orders.ClientId equals clients.Id
                         where activities.Id == activityId && orders.IsSuccess
                         orderby activities.DateCreated descending
                         select new OEnrolledInfo
                         {
                             Title = (ETitle)clients.TitleType.Code,
                             FirstName = clients.FirstName,
                             LastName = clients.LastName,
                             Address = clients.Address,
                             Suburb = clients.Suburb,
                             PostCode = clients.Postcode,
                             Mobile = clients.Mobile,
                             HomePone = clients.HomePhone,
                             WorkPhone = clients.WorkPhone,
                             Email = clients.Email,

                             Invoice = orders.Id,
                             DateCreated = orders.DateCreated,
                             Price = orders.PriceIncGST,
                             PriceType = (EPrice)orders.PriceType.Code,
                             Memo = orders.Memo
                         };

            //Return response
            Response.ClearContent();
            Response.AddHeader("content-disposition", string.Format("attachment;filename=Exported_Clients_{0}_{1}.csv", name, activityId));
            Response.ContentType = "text/csv";

            //content
            StringWriter sw = new StringWriter();
            sw.WriteLine("Title,FirstName,LastName,Address,Suburb,PostCode,Mobile,HomePone,WorkPhone,Email,Invoice,DateCreated,Price,PriceType,Memo");
            foreach (var client in models.AsEnumerable())
            {
                sw.WriteLine(string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14}",
                    client.Title,
                    client.FirstName,
                    client.LastName,
                    client.Address,
                    client.Suburb,
                    client.PostCode,
                    client.Mobile,
                    client.HomePone,
                    client.WorkPhone,
                    client.Email,
                    client.Invoice,
                    client.DateCreated,
                    client.Price,
                    client.PriceType,
                    client.Memo
                    ));
            }
            Response.Write(sw.ToString());
            Response.End();
        }
        #endregion
    }
}
