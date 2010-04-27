using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Dogbert.Controllers.ViewModels;
using Dogbert.Core.Domain;
using MvcContrib.Attributes;
using UCDArch.Web.Controller;
using MvcContrib;
using UCDArch.Web.Validator;

namespace Dogbert.Controllers
{
    public class RequirementController : SuperController
    {
        //
        // GET: /Requirement/

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// // GET: /Requirement/Create/{id}
        /// </summary>
        /// <param name="id">Project Id to link to</param>
        /// <returns></returns>
        public ActionResult Create(int projectId)
        {
            var project = Repository.OfType<Project>().GetNullableByID(projectId);

            if (project == null)
            {
                return this.RedirectToAction<ProjectController>(a => a.Index());
            }

            var viewModel = RequirementViewModel.Create(Repository, project);

            return View(viewModel);
        }

        [AcceptPost]
        public ActionResult Create(Requirement requirement, int projectId)
        {
            var project = Repository.OfType<Project>().GetNullableByID(projectId);

            if (project == null)
            {
                return this.RedirectToAction<ProjectController>(a => a.Index());
            }

            requirement.Project = project;

            MvcValidationAdapter.TransferValidationMessagesTo(ModelState, requirement.ValidationResults());



            var viewModel = RequirementViewModel.Create(Repository, project);
            viewModel.Requirement = requirement;

            return View(viewModel);
        }
    }
}
