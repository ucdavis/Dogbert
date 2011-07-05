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


    }
}
