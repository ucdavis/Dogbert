using System;
using System.Web.Mvc;
using System.Web.Security;
using UCDArch.Web.Authentication;
using UCDArch.Web.Controller;
using MvcContrib;

namespace Dogbert.Controllers
{
    public class AccountController : SuperController
    {
        public ActionResult LogOn(string returnUrl)
        {
            string resultUrl = CASHelper.Login();

            if (resultUrl != null)
            {
                return Redirect(resultUrl);
            }

            TempData["URL"] = resultUrl;

            return View();
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            // build a return url
            var returnUrl = String.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Action("Index", "Home"));

            // figure out if the user is cas? or openid
            return Redirect("https://cas.ucdavis.edu/cas/logout?service=" + returnUrl);
        }

    }
}
