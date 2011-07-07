using System.Web.Mvc;
using System.Web.Security;
using UCDArch.Web.Authentication;
using MvcContrib;

namespace Dogbert2.Controllers
{
    /// <summary>
    /// Controller for the Account class
    /// </summary>
    public class AccountController : ApplicationController
    {
        //
        // GET: /Account/
        public ActionResult LogOn(string returnUrl)
        {
            string resultUrl = CASHelper.Login();

            if (resultUrl != null)
            {
                return Redirect(resultUrl);
            }

            TempData["URL"] = returnUrl;

            return this.RedirectToAction<ErrorController>(a => a.NotAuthorized());
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return Redirect("https://cas.ucdavis.edu/cas/logout");
        }

#if DEBUG
        public ActionResult Emulate(string loginId)
        {
            FormsAuthentication.RedirectFromLoginPage(loginId, false);
            Session["Emulation"] = true;

            return this.RedirectToAction<HomeController>(a => a.Index());
        }

        public ActionResult EndEmulate()
        {
            FormsAuthentication.SignOut();
            Session["Emulation"] = false;

            return this.RedirectToAction<HomeController>(a => a.Index());
        }
#endif
    }
}
