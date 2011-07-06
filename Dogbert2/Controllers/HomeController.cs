using System.Web.Mvc;
using Dogbert2.Filters;

namespace Dogbert2.Controllers
{
    /// <summary>
    /// Controller for the Home class
    /// </summary>
    public class HomeController : ApplicationController
    {   
        //
        // GET: /Home/
        public ActionResult Index()
        {
            return View();
        }

        [AdminOnly]
        public ActionResult Admin()
        {
            return View();
        }
    }
}
