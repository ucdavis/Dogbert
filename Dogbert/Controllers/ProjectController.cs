using System.Linq;
using System.Web.Mvc;
using Dogbert.Core.Domain;
using MvcContrib.Attributes;
using UCDArch.Web.ActionResults;
using UCDArch.Web.Controller;
using MvcContrib;

namespace Dogbert.Controllers
{
    public class ProjectController : SuperController
    {
        // GET: /Project/
        public ActionResult Index()
        {
            var projects = Repository.OfType<Project>().Queryable.Where(p => p.Status.IsActive);
            return View(projects.ToList());
        }

        public ActionResult Edit(int id)
        {
            var project = Repository.OfType<Project>().GetNullableByID(id);

            if (project != null)
            {
                return View(project);
            }
            else
            {
                return this.RedirectToAction(a => a.Index());
            }

        }

        [AcceptPost]
        public ActionResult UpdateProjectPriority(int[] projects)
        {
            Project p;
            for (int i = 0; i < projects.Length; i++)
            {
                p = Repository.OfType<Project>().GetById(projects[i]);
                p.Priority = i + 1;
                Repository.OfType<Project>().EnsurePersistent(p);
            }
            return new JsonNetResult(true) ;

        }

       
   
    }
}
