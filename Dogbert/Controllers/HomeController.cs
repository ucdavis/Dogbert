using System.Linq;
using System.Web.Mvc;
using Dogbert.Core.Domain;
using UCDArch.Web.Attributes;
using UCDArch.Web.Controller;

namespace Dogbert.Controllers
{
    public class HomeController : SuperController
    {
        [HandleTransactionsManually]
        public ActionResult Index()
        {
            var projects = Repository.OfType<Project>().Queryable.Where(a => !a.StatusCode.IsComplete);

            return View(projects);
        }

        public ActionResult Designer()
        {
            var projects =
                Repository.OfType<Project>().Queryable.Where(
                    a => (a.ProjectType.Id == "WA" || a.ProjectType.Id == "WS") && a.DesignerShow).
                    OrderBy(a => a.DesignerOrder).ToList();

            return View(projects);
        }

        [HandleTransactionsManually]
        public ActionResult About()
        {
            return View();
        }
    }
}
