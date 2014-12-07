using System;
using System.Collections.Generic;
using System.IdentityModel.Services;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CreatureOfCode.Web.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Logout()
        {
            FederatedAuthentication.SessionAuthenticationModule.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}