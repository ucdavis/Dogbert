using System.Web.Mvc;
using UCDArch.Web.Attributes;
using UCDArch.Web.Controller;

namespace Dogbert.Controllers
{
    public class HomeController : SuperController
    {
        [HandleTransactionsManually]
        public ActionResult Index()
        {
            ViewData["Message"] = "Welcome to ASP.NET MVC!";

            return View();
        }

        [HandleTransactionsManually]
        public ActionResult About()
        {
            return View();
        }
    }
}
