using System.Web.Mvc;
using UCDArch.Web.Authentication;
using UCDArch.Web.Controller;

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
            return Redirect("https://cas.ucdavis.edu/cas/logout");
        }

    }
}
