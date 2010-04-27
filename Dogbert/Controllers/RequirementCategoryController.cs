using System;
using System.Web.Mvc;
using Dogbert.Controllers.ViewModels;
using Dogbert.Core.Domain;
using Dogbert.Core.Resources;
using MvcContrib.Attributes;
using UCDArch.Web.Controller;
using MvcContrib;
using UCDArch.Web.Validator;

namespace Dogbert.Controllers
{
    [Authorize(Roles = "User")]
    public class RequirementCategoryController : SuperController
    {
        //
        // GET: /RequirementCategory/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create(int projectId)
        {
            var project = Repository.OfType<Project>().GetNullableByID(projectId);

            if (project == null)
            {
                return this.RedirectToAction<ProjectController>(a => a.DynamicIndex());
            }

            var viewModel = RequirementCategoryViewModel.Create(Repository, project);

            return View(viewModel);
        }

        [AcceptPost]
        public ActionResult Create(int projectId, [Bind(Exclude="Id, IsActive")]RequirementCategory requirementCategory)
        {
            var project = Repository.OfType<Project>().GetNullableByID(projectId);

            if (project == null)
            {
                return this.RedirectToAction<ProjectController>(a => a.DynamicIndex());
            }

            project.AddRequirementCategory(requirementCategory);

            MvcValidationAdapter.TransferValidationMessagesTo(ModelState, requirementCategory.ValidationResults());

            if (ModelState.IsValid)
            {
                Repository.OfType<Project>().EnsurePersistent(project);
                Message = String.Format(NotificationMessages.STR_ObjectCreated, "Requirement Category");
                return this.RedirectToAction<ProjectController>(a => a.Edit(project.Id));
            }

            var viewModel = RequirementCategoryViewModel.Create(Repository, project);

            return View(viewModel);
        }
    }
}
