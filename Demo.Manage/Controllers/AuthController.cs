using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Demo.Manage.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        public ActionResult Login()
        {
       //     return View();
            return RedirectToAction("Index", "Home");
        }

    }
}
