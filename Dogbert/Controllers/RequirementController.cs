using System.Web.Mvc;
using Dogbert.Controllers.Helpers;
using Dogbert.Controllers.ViewModels;
using Dogbert.Core.Domain;
using Dogbert.Core.Resources;
using MvcContrib;
using MvcContrib.Attributes;
using UCDArch.Web.Controller;
using UCDArch.Web.Helpers;
using UCDArch.Core.Utils;

namespace Dogbert.Controllers
{
    [Authorize(Roles = "User")]
    public class RequirementController : SuperController
    {
        //
        // GET: /Requirement/
        public ActionResult Index()
        {
            return this.RedirectToAction<ProjectController>(a => a.DynamicIndex());
        }

        /// <summary>
        /// // GET: /Requirement/Create/{id}
        /// </summary>
        /// <param name="projectId">Project Id to link to</param>
        /// <returns></returns>
        public ActionResult Create(int projectId)
        {
            var project = Repository.OfType<Project>().GetNullableByID(projectId);

            if (project == null)
            {
                Message = string.Format(NotificationMessages.STR_ObjectNotFound, "Project");
                return this.RedirectToAction<ProjectController>(a => a.DynamicIndex());
            }

            var viewModel = RequirementViewModel.Create(Repository, project);

            return View(viewModel);
        }

        /// <summary>
        /// Creates the specified requirement.
        /// </summary>
        /// <param name="requirement">The requirement.</param>
        /// <param name="projectId">The project id.</param>
        /// <returns></returns>
        [AcceptPost]
        [ValidateInput(false)]
        public ActionResult Create(Requirement requirement, int projectId)
        {
            var project = Repository.OfType<Project>().GetNullableByID(projectId);

            if (project == null)
            {
                Message = string.Format(NotificationMessages.STR_ObjectNotFound, "Project");
                return this.RedirectToAction<ProjectController>(a => a.DynamicIndex());
            }            

            requirement.Project = project;

            requirement.TransferValidationMessagesTo(ModelState);
            if(ModelState.IsValid)
            {
                Repository.OfType<Requirement>().EnsurePersistent(requirement);
                Message = string.Format(NotificationMessages.STR_ObjectCreated, "Requirement");
                //return this.RedirectToAction<ProjectController>(a => a.Edit(projectId));
                return Redirect(Url.EditProjectUrl(projectId, StaticValues.Tab_Requirements));
            }

            var viewModel = RequirementViewModel.Create(Repository, project);
            viewModel.Requirement = requirement;
            return View(viewModel);
        }

        /// <summary>
        /// Edits the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            var requirement = Repository.OfType<Requirement>().GetNullableByID(id);
            
            if (requirement == null)
            {
                Message = string.Format(NotificationMessages.STR_ObjectNotFound, "Requirement");
                return this.RedirectToAction<ProjectController>(a => a.DynamicIndex());
            }
            var requirementViewModel = RequirementViewModel.Create(Repository, requirement.Project);
            requirementViewModel.Requirement = requirement;
           
            return View(requirementViewModel);

        }

        /// <summary>
        /// Edits the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="requirement">The requirement.</param>
        /// <returns></returns>
        [AcceptPost]
        [ValidateInput(false)]
        public ActionResult Edit(int id, Requirement requirement)
        {
            var dest = Repository.OfType<Requirement>().GetNullableByID(id);

            if (requirement == null)
            {
                Message = string.Format(NotificationMessages.STR_ObjectNotFound, "Requirement");
                return this.RedirectToAction<ProjectController>(a => a.DynamicIndex());
            }
            Copiers.CopyRequirement(requirement, dest);
            dest.TransferValidationMessagesTo(ModelState);

            if (ModelState.IsValid)
            {
                Repository.OfType<Requirement>().EnsurePersistent(dest);
                Message = string.Format(NotificationMessages.STR_ObjectUpdated, "Requirement");
                //return this.RedirectToAction<ProjectController>(a => a.Edit(projectId));
                return Redirect(Url.EditProjectUrl(dest.Project.Id, StaticValues.Tab_Requirements));
            }

            var viewModel = RequirementViewModel.Create(Repository, dest.Project);
            viewModel.Requirement = requirement;

            return View(viewModel);
        }

        /// <summary>
        /// Gets the specified id to Delete.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            var requirement = Repository.OfType<Requirement>().GetNullableByID(id);

            if (requirement == null)
            {
                Message = string.Format(NotificationMessages.STR_ObjectNotFound, "Requirement");
                return this.RedirectToAction<ProjectController>(a => a.DynamicIndex());
            }
            var requirementViewModel = RequirementViewModel.Create(Repository, requirement.Project);
            requirementViewModel.Requirement = requirement;

            return View(requirementViewModel);
        }

        /// <summary>
        /// Deletes the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="requirement">The requirement.</param>
        /// <returns></returns>
        [AcceptPost]
        [ValidateInput(false)]
        public ActionResult Delete(int id, Requirement requirement)
        {
            var requirementToDelete = Repository.OfType<Requirement>().GetNullableByID(id);

            if (requirementToDelete == null)
            {
                Message = string.Format(NotificationMessages.STR_ObjectNotFound, "Requirement");
                return this.RedirectToAction<ProjectController>(a => a.DynamicIndex());
            }

            var saveProjectId = requirementToDelete.Project.Id;

            Repository.OfType<Requirement>().Remove(requirementToDelete);
            Message = string.Format(NotificationMessages.STR_ObjectRemoved, "Requirement");
            return Redirect(Url.EditProjectUrl(saveProjectId, StaticValues.Tab_Requirements));

        }

      
    }
}
