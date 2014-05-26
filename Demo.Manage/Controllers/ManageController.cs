using Demo.Core.DBs;
using Demo.Core.DTOs;
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
    public class ManageController : Controller
    {
        DemoDBEntities _DemoDB;
        DateTime _Now = DateTime.Now;

        public ManageController(DemoDBEntities demoDB)
        {
            _DemoDB = demoDB;
        }

        #region Manage

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
                         select new OClassBrief
                         {

                             Id = activities.Id,
                             Year = terms.Year,
                             Term = terms.Name,
                             Name = activities.Name,
                             Level = levels.Level1,
                             MaxNumber = activities.MaxNumber,
                             PriceIncGST = activities.PriceIncGST,
                             IsOnWebsite = !activities.IsHidden,
                             IsValid = activities.IsValid
                         };


            ViewBag.PropertyInfo = models.ElementType.GetProperties();
            models = models.Processing(Request.QueryString);

            return PartialView(models.ToPagedList(pageIndex));
        }

        public ActionResult CreateClass()
        {
            return RedirectToAction("EditClass");
        }

        public ActionResult EditClass(int? id)
        {
            OClass model = new OClass();
            if (id.HasValue)
            {
                Activity activity = this._DemoDB.Activities.Find(id);
                if (activity == null || activity.ActivityType.Code != (int)EActivity.Class)
                {
                    return HttpNotFound();
                }

                Term term = this._DemoDB.Terms.Find(activity.TermId);
                Class class1 = this._DemoDB.Classes.Find(activity.ClassId);
                if (term == null || class1 == null) return HttpNotFound();
                Level level = this._DemoDB.Levels.Find(class1.LevelId);

                //From Activity
                model.Name = activity.Name;
                model.IsValid = activity.IsValid;
                model.IsOnWebiste = !activity.IsHidden;
                model.MaxNumber = activity.MaxNumber;
                model.PriceWithGST = activity.PriceIncGST;
                model.EarlyBirdPrice = activity.PriceDiscount;
                model.ConcessionPrice = activity.PriceConcession;
                model.MemberPrice = activity.PriceMember;
                model.EarlyBirdPrice = activity.PriceDiscount;
                model.EarlyBirdDateTime = activity.DateEarlyBird;
                //From Term
                model.Year = term.Year;
                model.Term = term.Name;
                //From Class
                model.DayTime = class1.DayTime;
                model.Location = class1.Location;
                model.Room = class1.Room;
                model.Teacher = class1.Teacher;
                //From Level
                model.Level = level.Level1;

                TermYearDropDownList(term.Year, term.Id);
                LevelDropList(level.Level1);
            }
            else
            {
                TermYearDropDownList();
                LevelDropList();
            }

            @ViewBag.Action = "Edit Class";
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditClass(int? id, OClass model)
        {
            if (!ModelState.IsValid || string.IsNullOrEmpty(model.Term) || string.IsNullOrEmpty(model.Level))
            {
                if (string.IsNullOrEmpty(model.Term) || string.IsNullOrEmpty(model.Level))
                {
                    ModelState.AddModelError("", "Please select a Term and Level");
                }

                TermYearDropDownList(model.Year);
                LevelDropList();
                return View(model);
            }

            int termId = 0;
            int levelId = 0;
            if (!int.TryParse(model.Term, out termId))
                return View(model);
            if (!int.TryParse(model.Level, out levelId))
                return View(model);

            Level level = this._DemoDB.Levels.FirstOrDefault(s => s.Id == levelId);

            if (id.HasValue)
            {
                Activity activity = this._DemoDB.Activities.Find(id);
                //update Class
                activity.Class.DayTime = model.DayTime;
                activity.Class.Location = model.Location;
                activity.Class.Room = model.Room;
                activity.Class.Teacher = model.Teacher;

                //update Activity
                activity.ActivityTypeId = (int)EActivity.Class;
                activity.TermId = termId;
                activity.Name = model.Name;
                activity.IsValid = model.IsValid;
                activity.IsHidden = !model.IsOnWebiste;
                activity.MaxNumber = model.MaxNumber;
                activity.PriceIncGST = model.PriceWithGST;
                activity.PriceConcession = model.ConcessionPrice;
                activity.PriceMember = model.MemberPrice;
                activity.PriceDiscount = model.EarlyBirdPrice;
                activity.DateEarlyBird = model.EarlyBirdDateTime;
            }
            else
            {
                //create a class
                Class class1 = new Class()
                {
                    Level = level,
                    DayTime = model.DayTime,
                    Location = model.Location,
                    Room = model.Room,
                    Teacher = model.Teacher,
                    DateCreated = this._Now
                };

                //create
                Activity activity = new Activity()
                {
                    ActivityTypeId = (int)EActivity.Class,
                    Class = class1,
                    TermId = termId,
                    Name = model.Name,
                    IsValid = model.IsValid,
                    IsHidden = !model.IsOnWebiste,
                    MaxNumber = model.MaxNumber,
                    PriceIncGST = model.PriceWithGST,
                    PriceDiscount = model.EarlyBirdPrice,
                    PriceConcession = model.ConcessionPrice,
                    PriceMember = model.MemberPrice,
                    DateCreated = this._Now
                };

                this._DemoDB.Activities.Add(activity);
            }

            try
            {
                this._DemoDB.SaveChanges();
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                //TODO: handle ex
            }

            return RedirectToAction("Classes");
        }

        public ActionResult ShowClass(int? id)
        {
            OClass model = new OClass();
            if (id.HasValue)
            {
                Activity activity = this._DemoDB.Activities.Find(id);
                if (activity == null || activity.ActivityType.Code != (int)EActivity.Class)
                {
                    return HttpNotFound();
                }

                Term term = this._DemoDB.Terms.Find(activity.TermId);
                Class class1 = this._DemoDB.Classes.Find(activity.ClassId);
                if (term == null || class1 == null) return HttpNotFound();
                Level level = this._DemoDB.Levels.Find(class1.LevelId);

                //From Activity
                model.Id = activity.Id;
                model.Name = activity.Name;
                model.IsValid = activity.IsValid;
                model.IsOnWebiste = !activity.IsHidden;
                model.MaxNumber = activity.MaxNumber;
                model.PriceWithGST = activity.PriceIncGST;
                model.ConcessionPrice = activity.PriceConcession;
                model.MemberPrice = activity.PriceMember;
                model.EarlyBirdPrice = activity.PriceDiscount;
                model.EarlyBirdDateTime = activity.DateEarlyBird;
                //From Term
                model.Year = term.Year;
                model.Term = term.Name;
                //From Class
                model.DayTime = class1.DayTime;
                model.Location = class1.Location;
                model.Room = class1.Room;
                model.Teacher = class1.Teacher;
                //From Level
                model.Level = level.Level1;

                TermYearDropDownList(term.Year, term.Id);
                LevelDropList(level.Level1);
            }

            @ViewBag.Action = "Class Detail";
            return PartialView(model);
        }

        [HttpDelete]
        public ActionResult DeleteClass(int? id)
        {
            Activity activity = this._DemoDB.Activities.Find(id);
            activity.IsDeleted = true;

            try
            {
                this._DemoDB.SaveChanges();
            }
            catch (Exception)
            {
                //TODO: handle ex
            }

            return RedirectToAction("Classes");
        }
        #endregion

        #region Members
        public ActionResult Members(int pageIndex = 1)
        {
            var models = from activities in this._DemoDB.Activities
                         join terms in this._DemoDB.Terms
                         on activities.TermId equals terms.Id
                         where !activities.IsDeleted && activities.ActivityType.Code == 2
                         orderby activities.DateCreated descending
                         select new OMember
                         {
                             //From terms
                             Year = terms.Year,
                             Term = terms.Name,

                             //From events
                             Id = activities.Id,
                             Name = activities.Name,
                             IsValid = activities.IsValid,
                             MaxNumber = activities.MaxNumber,
                             PriceWithGST = activities.PriceIncGST,
                             EarlyBirdPrice = activities.PriceDiscount,
                             ConcessionPrice = activities.PriceConcession,
                             MemberPrice = activities.PriceMember
                         };

            ViewBag.PropertyInfo = models.ElementType.GetProperties();
            models = models.Processing(Request.QueryString);

            return PartialView(models.ToPagedList(pageIndex));
        }

        public ActionResult CreateMember()
        {
            return RedirectToAction("EditMember");
        }

        public ActionResult EditMember(int? id)
        {
            OMember member = new OMember();
            if (id.HasValue)
            {
                Activity activity = this._DemoDB.Activities.Find(id);
                if (activity == null || activity.ActivityType.Code != 2)
                {
                    return HttpNotFound();
                }

                Term term = this._DemoDB.Terms.Find(activity.TermId);
                if (term == null) return HttpNotFound();

                member = new OMember
                {
                    Name = activity.Name,
                    IsValid = activity.IsValid,
                    MaxNumber = activity.MaxNumber,
                    PriceWithGST = activity.PriceIncGST,
                    ConcessionPrice = activity.PriceConcession,
                    MemberPrice = activity.PriceMember,
                    EarlyBirdPrice = activity.PriceDiscount,
                    EarlyBirdDateTime = activity.DateEarlyBird
                };

                member.Year = term.Year;
                member.Term = term.Name;

                TermYearDropDownList(term.Year, term.Id);
            }
            else
            {
                TermYearDropDownList();
            }

            @ViewBag.Action = "Edit Member";
            return PartialView(member);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditMember(int? id, OMember model)
        {
            if (!ModelState.IsValid || string.IsNullOrEmpty(model.Term))
            {
                if (string.IsNullOrEmpty(model.Term))
                {
                    ModelState.AddModelError("", "Please select a Term");
                }

                TermYearDropDownList(model.Year);
                return View(model);
            }

            int termId = 0;
            if (!int.TryParse(model.Term, out termId))
                return View(model);

            if (id.HasValue)
            {
                //update
                Activity activity = this._DemoDB.Activities.Find(id);
                activity.ActivityTypeId = 2;
                activity.TermId = termId;
                activity.Name = model.Name;
                activity.IsValid = model.IsValid;
                activity.MaxNumber = model.MaxNumber;
                activity.PriceIncGST = model.PriceWithGST;
                activity.PriceConcession = model.ConcessionPrice;
                activity.PriceMember = model.MemberPrice;
                activity.PriceDiscount = model.EarlyBirdPrice;
                activity.DateEarlyBird = model.EarlyBirdDateTime;
            }
            else
            {
                //create
                Activity activity = new Activity()
                {
                    ActivityTypeId = 2,
                    TermId = termId,
                    Name = model.Name,
                    IsValid = model.IsValid,
                    MaxNumber = model.MaxNumber,
                    PriceIncGST = model.PriceWithGST,
                    PriceConcession = model.ConcessionPrice,
                    PriceMember = model.MemberPrice,
                    PriceDiscount = model.EarlyBirdPrice,
                    DateEarlyBird = model.EarlyBirdDateTime,
                    DateCreated = this._Now
                };

                this._DemoDB.Activities.Add(activity);
            }

            try
            {
                this._DemoDB.SaveChanges();
            }
            catch (Exception)
            {
                //TODO: handle ex
            }

            return RedirectToAction("Members");
        }

        public ActionResult ShowMember(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Activity activity = this._DemoDB.Activities.Find(id);
            if (activity == null || activity.ActivityType.Code != 2)
            {
                return HttpNotFound();
            }

            Term term = this._DemoDB.Terms.Find(activity.TermId);
            if (term == null) return HttpNotFound();

            OMember member = new OMember()
            {
                Id = activity.Id,
                Name = activity.Name,
                IsValid = activity.IsValid,
                MaxNumber = activity.MaxNumber,
                PriceWithGST = activity.PriceIncGST,
                ConcessionPrice = activity.PriceConcession,
                MemberPrice = activity.PriceMember,
                EarlyBirdPrice = activity.PriceDiscount,
                EarlyBirdDateTime = activity.DateEarlyBird
            };
            member.Year = term.Year;
            member.Term = term.Name;

            @ViewBag.Action = "Member Detail";
            return PartialView(member);
        }

        [HttpDelete]
        public ActionResult DeleteMember(int id)
        {
            Activity activity = this._DemoDB.Activities.Find(id);
            activity.IsDeleted = true;

            try
            {
                this._DemoDB.SaveChanges();
            }
            catch (Exception)
            {
                //TODO: handle ex
            }

            return RedirectToAction("Members");
        }
        #endregion

        #region Terms
        public ActionResult Terms(int pageIndex = 1)
        {
            IQueryable<OTerm> models = this._DemoDB.Terms.Where(s => !s.IsDeleted)
                .OrderByDescending(s => s.DateCreated).Select(s => new OTerm
                {
                    Year = s.Year,
                    Name = s.Name,
                    IsOnWebsite = !s.IsHidden,
                    StartDate = s.DateStart,
                    EndDate = s.DateEnd,
                    Id = s.Id
                });

            ViewBag.PropertyInfo = models.ElementType.GetProperties();
            models = models.Processing(Request.QueryString);

            return PartialView(models.ToPagedList(pageIndex));
        }

        public ActionResult CreateTerm()
        {
            return RedirectToAction("EditTerm");
        }

        public ActionResult EditTerm(int? id)
        {
            OTerm dateTerm = new OTerm();
            if (id.HasValue)
            {
                Term term = this._DemoDB.Terms.Find(id);
                dateTerm = new OTerm()
                {
                    Year = term.Year,
                    Name = term.Name,
                    IsOnWebsite = !term.IsHidden,
                    StartDate = term.DateStart,
                    EndDate = term.DateEnd,
                    Id = term.Id
                };
            }

            @ViewBag.Action = "Edit Term";
            return PartialView(dateTerm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTerm(int? id, OTerm model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (id.HasValue)
            {
                //update
                Term term = this._DemoDB.Terms.Find(id);
                term.Year = model.Year;
                term.Name = model.Name;
                term.DateStart = model.StartDate;
                term.DateEnd = model.EndDate;
                term.IsHidden = !model.IsOnWebsite;
            }
            else
            {
                //create
                Term term = new Term()
                {
                    Year = model.Year,
                    Name = model.Name,
                    DateStart = model.StartDate,
                    DateEnd = model.EndDate,
                    IsHidden = !model.IsOnWebsite,
                    DateCreated = this._Now
                };
                this._DemoDB.Terms.Add(term);
            }

            try
            {
                this._DemoDB.SaveChanges();
            }
            catch (Exception)
            {
                //TODO: handle ex
            }

            return RedirectToAction("Terms");
        }

        public ActionResult ShowTerm(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Term term = this._DemoDB.Terms.Find(id);
            if (term == null)
            {
                return HttpNotFound();
            }

            @ViewBag.Action = "Term Detail";
            return PartialView(new OTerm()
            {
                Year = term.Year,
                Name = term.Name,
                IsOnWebsite = !term.IsHidden,
                StartDate = term.DateStart,
                EndDate = term.DateEnd,
                Id = term.Id
            });
        }

        [HttpDelete]
        public ActionResult DeleteTerm(int id)
        {
            Term term = this._DemoDB.Terms.Find(id);
            term.IsDeleted = true;

            try
            {
                this._DemoDB.SaveChanges();
            }
            catch (Exception)
            {
                //TODO: handle ex
            }

            return RedirectToAction("Terms");
        }
        #endregion

        #region Levels
        public ActionResult Levels(int pageIndex = 1)
        {
            IQueryable<OLevel> models = this._DemoDB.Levels.Where(s => !s.IsDeleted)
                .OrderByDescending(s => s.DateCreated)
                .Select(s => new OLevel
                {
                    Category = s.Category,
                    Subcategory = s.Subcategory,
                    Subject = s.Subject,
                    IsOnWebsite = !s.IsHidden,
                    IsValid = s.IsValid,
                    Id = s.Id
                });


            ViewBag.PropertyInfo = models.ElementType.GetProperties();
            models = models.Processing(Request.QueryString);

            return PartialView(models.ToPagedList(pageIndex));
        }

        public ActionResult CreateLevel()
        {
            return RedirectToAction("EditLevel");
        }

        public ActionResult EditLevel(int? id)
        {
            OLevel classLevel = new OLevel();
            if (id.HasValue)
            {
                Level level = this._DemoDB.Levels.Find(id);
                classLevel = new OLevel()
                {
                    Category = level.Category,
                    Subcategory = level.Subcategory,
                    Subject = level.Subject,
                    IsOnWebsite = !level.IsHidden,
                    IsValid = level.IsValid,
                    Id = level.Id
                };
            }

            @ViewBag.Action = "Edit Level";
            return PartialView(classLevel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditLevel(int? id, OLevel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (id.HasValue)
            {
                //update
                Level level = this._DemoDB.Levels.Find(id);
                level.Category = model.Category;
                level.Subcategory = model.Subcategory;
                level.Subject = model.Subject;
                level.Level1 = string.Format("{0}-{1}-{2}", model.Category, model.Subcategory, model.Subject);
                level.IsValid = model.IsValid;
                level.IsHidden = !model.IsOnWebsite;
            }
            else
            {
                //create
                Level level = new Level()
                {
                    Category = model.Category,
                    Subcategory = model.Subcategory,
                    Subject = model.Subject,
                    Level1 = string.Format("{0}-{1}-{2}", model.Category, model.Subcategory, model.Subject),
                    IsHidden = !model.IsOnWebsite,
                    IsValid = model.IsValid,
                    DateCreated = this._Now
                };
                this._DemoDB.Levels.Add(level);
            }

            try
            {
                this._DemoDB.SaveChanges();
            }
            catch (Exception ex)
            {
                //TODO: handle ex
            }

            return RedirectToAction("Levels");
        }

        public ActionResult ShowLevel(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Level level = this._DemoDB.Levels.Find(id);
            if (level == null)
            {
                return HttpNotFound();
            }

            @ViewBag.Action = "Level Detail";
            return PartialView(new OLevel()
            {
                Category = level.Category,
                Subcategory = level.Subcategory,
                Subject = level.Subject,
                IsOnWebsite = !level.IsHidden,
                IsValid = level.IsValid,
                Id = level.Id
            });
        }

        [HttpDelete]
        public ActionResult DeleteLevel(int id)
        {
            Level level = this._DemoDB.Levels.Find(id);
            level.IsDeleted = true;

            try
            {
                this._DemoDB.SaveChanges();
            }
            catch (Exception)
            {
                //TODO: handle ex
            }

            return RedirectToAction("Levels");
        }
        #endregion
        #endregion

        #region Supports
        [HttpPost]
        public ActionResult GetTermsByYear(int year)
        {
            var terms = this._DemoDB.Terms.Where(s => s.Year == year).Select(s => new { Id = s.Id, Term = s.Name });
            SelectList items = new SelectList(terms, "Id", "Term", null);
            return Json(items);
        }

        private void TermYearDropDownList(int? selectedYear = null, int? id = null)
        {
            var termYear = this._DemoDB.Terms.GroupBy(s => s.Year)
                .Select(s => new { Year = s.Key }).OrderByDescending(s => s.Year).ToDictionary(s => s.Year, s => s.Year.ToString());
            if (selectedYear == null)
            {
                termYear.Add(0, "Please choose");
                ViewBag.TermYear = new SelectList(termYear, "Key", "Value", 0);
            }
            else
            {
                ViewBag.TermYear = new SelectList(termYear, "Key", "Value", selectedYear);
            }

            TermNameDropDownList(selectedYear, id);
        }

        private void TermNameDropDownList(int? year, int? id = null)
        {
            if (year != null)
            {
                var terms = this._DemoDB.Terms.Where(s => s.Year == year.Value).Select(s => new { Id = s.Id, Term = s.Name });
                ViewBag.TermName = new SelectList(terms, "Id", "Term", id);

            }
            else
                ViewBag.TermName = new SelectList(new List<int>());
        }

        private void LevelDropList(string level = null)
        {
            var levels = this._DemoDB.Levels.Where(s => !s.IsDeleted && !s.IsHidden && s.IsValid)
                .OrderByDescending(s => s.DateCreated).Select(s => new { Id = s.Id, Level = s.Level1 });
            ViewBag.Levels = new SelectList(levels, "Id", "Level", level);
        }
        #endregion
    }
}
