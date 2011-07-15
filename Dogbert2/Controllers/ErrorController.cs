using System.Web.Mvc;

namespace Dogbert2.Controllers
{
    /// <summary>
    /// Controller for the Error class
    /// </summary>
    public class ErrorController : ApplicationController
    {
        //
        // GET: /Error/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NotAuthorized()
        {
            return View();
        }

    }

}
