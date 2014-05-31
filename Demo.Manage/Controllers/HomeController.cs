using Demo.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Demo.Manage.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(InitilisePanel());
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        /// <summary>
        /// Initial web page navigate
        /// </summary>
        /// <returns>navigate choice</returns>
        private IEnumerable<PanelModel> InitilisePanel()
        {
            List<PanelModel> panels = new List<PanelModel>();

            //TODO: update later
            panels.Add(new PanelModel()
            {
                Id = 1,
                Item = "Invoices",
                SubItems = new List<SubItem>(new SubItem[] { 
                    new SubItem(){Name = "Invoice List", Action = "Invoices", Controller = "Invoice"}})
            });

            panels.Add(new PanelModel()
            {
                Id = 2,
                Item = "View Classes & Lists",
                SubItems = new List<SubItem>(new SubItem[] { 
                    new SubItem(){Name = "Classes", Action = "Classes", Controller = "Product"}, 
                    new SubItem(){Name = "Events", Action = "Events", Controller = "Product" }, 
                    new SubItem(){Name = "Memberships", Action = "Memberships", Controller = "Product" }})
            });

            panels.Add(new PanelModel()
            {
                Id = 3,
                Item = "Setup Classes & Lists",
                SubItems = new List<SubItem>(new SubItem[] { 
                    new SubItem(){Name = "Classes", Action="Classes", Controller = "Manage"}, 
                    new SubItem(){Name = "Members", Action="Members", Controller = "Manage"}, 
                    new SubItem(){Name = "Terms", Action="Terms", Controller = "Manage" }, 
                    new SubItem(){Name = "Levels", Action="Levels", Controller = "Manage" }})
            });

            panels.Add(new PanelModel()
            {
                Id = 4,
                Item = "Adminstration",
                SubItems = new List<SubItem>(new SubItem[] { 
                    new SubItem(){Name = "Users", Action="Users", Controller = "Administration"}})
            });

            panels.Add(new PanelModel()
            {
                Id = 5,
                Item = "About",
                SubItems = new List<SubItem>(new SubItem[] { 
                    new SubItem(){Name = "Who we are", Action = "About", Controller = "Home"}, 
                    new SubItem(){Name = "Contact", Action = "Contact", Controller = "Home" }})
            });

            return panels;
        }

    }
}
