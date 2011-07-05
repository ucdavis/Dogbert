using System;
using System.Linq;
using System.Web.Mvc;
using UCDArch.Core.PersistanceSupport;
using UCDArch.Core.Utils;

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
