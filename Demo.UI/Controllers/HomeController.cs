using Demo.Core.DBs;
using Demo.Core.DTOs;
using Demo.Core.Enums;
using Demo.Core.Helpers;
using Demo.UI.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Demo.UI.Controllers
{
    public class HomeController : Controller
    {
        DemoDBEntities _DemoDB;
        DateTime _Now = DateTime.Now;

        public HomeController(DemoDBEntities demoDB)
        {
            _DemoDB = demoDB;
        }

        public ActionResult Index(int pageIndex = 1)
        {
            var models = from activities in this._DemoDB.Activities
                         join classes in this._DemoDB.Classes
                         on activities.ClassId equals classes.Id
                         join levels in this._DemoDB.Levels
                         on classes.LevelId equals levels.Id
                         join terms in this._DemoDB.Terms
                         on activities.TermId equals terms.Id
                         where !activities.IsDeleted && activities.IsValid
                            && activities.ActivityType.Code == (int)EActivity.Class
                         orderby activities.DateCreated descending
                         select new OUIClass
                         {
                             Code = activities.Id,

                             Year = terms.Year,
                             Term = terms.Name,
                             StartDate = terms.DateStart,
                             EndDate = terms.DateEnd,

                             Level = levels.Level1,

                             DayTime = classes.DayTime,
                             Location = classes.Location,
                             Room = classes.Room,
                             Teacher = classes.Teacher,

                             Name = activities.Name,
                             MaxNumber = activities.MaxNumber,
                             PriceIncGST = activities.PriceIncGST,
                             ConcessionPrice = activities.PriceConcession,
                             MemberPrice = activities.PriceMember,
                             EarlyBirdDateTime = activities.DateEarlyBird,
                             EarlyBirdPrice = activities.PriceDiscount
                         };


            ViewBag.PropertyInfo = models.ElementType.GetProperties();
            models = models.Processing(Request.QueryString);

            return View(models.ToPagedList(pageIndex));
        }

        #region Data
        [AllowCrossSiteJson]
        public ActionResult Members()
        {
            var models = from activities in this._DemoDB.Activities
                         join terms in this._DemoDB.Terms
                         on activities.TermId equals terms.Id
                         where !activities.IsDeleted && activities.IsValid
                            && activities.ActivityType.Code == (int)EActivity.Member
                         orderby activities.DateCreated descending
                         select new OUIMember
                         {
                             Code = activities.Id,

                             Year = terms.Year,
                             Term = terms.Name,
                             StartDate = terms.DateStart,
                             EndDate = terms.DateEnd,

                             Name = activities.Name,
                             PriceIncGST = activities.PriceIncGST,
                             ConcessionPrice = activities.PriceConcession,
                             MemberPrice = activities.PriceMember,
                             EarlyBirdDateTime = activities.DateEarlyBird,
                             EarlyBirdPrice = activities.PriceDiscount
                         };

            return Json(models, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [AllowCrossSiteJson]
        public ActionResult GetMember(int id)
        {
            OUIMember ouimember =
                        (from activities in this._DemoDB.Activities
                         join terms in this._DemoDB.Terms
                         on activities.TermId equals terms.Id
                         where !activities.IsDeleted && activities.IsValid
                            && activities.ActivityType.Code == (int)EActivity.Member
                            && activities.Id == id
                         orderby activities.DateCreated descending
                         select new OUIMember
                         {
                             Code = activities.Id,

                             Year = terms.Year,
                             Term = terms.Name,
                             StartDate = terms.DateStart,
                             EndDate = terms.DateEnd,

                             Name = activities.Name,
                             PriceIncGST = activities.PriceIncGST,
                             ConcessionPrice = activities.PriceConcession,
                             MemberPrice = activities.PriceMember,
                             EarlyBirdDateTime = activities.DateEarlyBird,
                             EarlyBirdPrice = activities.PriceDiscount
                         }).FirstOrDefault();

            return Json(ouimember, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [AllowCrossSiteJson]
        public ActionResult GetClass(int id)
        {
            OUIClass ouiclass =
                        (from activities in this._DemoDB.Activities
                         join classes in this._DemoDB.Classes
                         on activities.ClassId equals classes.Id
                         join levels in this._DemoDB.Levels
                         on classes.LevelId equals levels.Id
                         join terms in this._DemoDB.Terms
                         on activities.TermId equals terms.Id
                         where !activities.IsDeleted && activities.IsValid
                            && activities.ActivityType.Code == (int)EActivity.Class
                            && activities.Id == id
                         orderby activities.DateCreated descending
                         select new OUIClass
                         {
                             Code = activities.Id,

                             Year = terms.Year,
                             Term = terms.Name,
                             StartDate = terms.DateStart,
                             EndDate = terms.DateEnd,

                             Level = levels.Level1,

                             DayTime = classes.DayTime,
                             Location = classes.Location,
                             Room = classes.Room,
                             Teacher = classes.Teacher,

                             Name = activities.Name,
                             MaxNumber = activities.MaxNumber,
                             PriceIncGST = activities.PriceIncGST,
                             ConcessionPrice = activities.PriceConcession,
                             MemberPrice = activities.PriceMember,
                             EarlyBirdDateTime = activities.DateEarlyBird,
                             EarlyBirdPrice = activities.PriceDiscount
                         }).FirstOrDefault();

            return Json(ouiclass, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
