using System;
using System.Linq;
using System.Web.Mvc;
using Dogbert.Controllers.ViewModels;
using Dogbert.Core.Domain;
using Dogbert.Core.Resources;
using Dogbert.Controllers.Helpers;
using MvcContrib.Attributes;
using UCDArch.Web.Controller;
using MvcContrib;
using UCDArch.Web.Helpers;
using UCDArch.Web.Validator;

namespace Dogbert.Controllers
{
    [Authorize(Roles = "User")]
    public class RequirementCategoryController : SuperController
    {
        //
        // GET: /RequirementCategory/

        public ActionResult Create(int projectId)
        {
            var project = Repository.OfType<Project>().GetNullableById(projectId);

            if (project == null)
            {
                return this.RedirectToAction<ProjectController>(a => a.DynamicIndex());
            }

            var viewModel = RequirementCategoryViewModel.Create(Repository, project);

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(int projectId, [Bind(Exclude="Id, IsActive")]RequirementCategory requirementCategory)
        {
            var project = Repository.OfType<Project>().GetNullableById(projectId);

            if (project == null)
            {
                return this.RedirectToAction<ProjectController>(a => a.DynamicIndex());
            }

            if (project.RequirementCategories.Any(a => a.Name == requirementCategory.Name))
            {
                ModelState.AddModelError("Text Type", "Requirement Category already exists in this project");
            }
            else
            {//if requirmentcategory is not already in project, add to project
                project.AddRequirementCategory(requirementCategory);
                MvcValidationAdapter.TransferValidationMessagesTo(ModelState, requirementCategory.ValidationResults());
            }
            
            if (ModelState.IsValid)
            {
                Repository.OfType<Project>().EnsurePersistent(project);
                Message = String.Format(NotificationMessages.STR_ObjectCreated, "Requirement Category");
               // return this.RedirectToAction<ProjectController>(a => a.Edit(project.Id));
                return Redirect(Url.EditProjectUrl(projectId, StaticValues.Tab_RequirementCategories));
            }

            var viewModel = RequirementCategoryViewModel.Create(Repository, project);

            return View(viewModel);
        }
    }
}
