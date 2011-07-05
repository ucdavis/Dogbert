using System.Web.Mvc;

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
    }
}
