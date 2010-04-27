using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Dogbert.Controllers.Helpers;
using Dogbert.Controllers.ViewModels;
using Dogbert.Core.Domain;
using Dogbert.Core.Resources;
using MvcContrib.Attributes;
using UCDArch.Web.Controller;
using MvcContrib;
using UCDArch.Web.Helpers;
using UCDArch.Web.Validator;

namespace Dogbert.Controllers
{
    public class RequirementController : SuperController
    {
        //
        // GET: /Requirement/

        public ActionResult Index()
        {
            return this.RedirectToAction<ProjectController>(a => a.Index());
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
                Message = string.Format(NotificationMessages.STR_ObjectNotFound, "Project");
                return this.RedirectToAction<ProjectController>(a => a.Index());
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
        public ActionResult Create(Requirement requirement, int projectId)
        {
            var project = Repository.OfType<Project>().GetNullableByID(projectId);

            if (project == null)
            {
                Message = string.Format(NotificationMessages.STR_ObjectNotFound, "Project");
                return this.RedirectToAction<ProjectController>(a => a.Index());
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

        public ActionResult Edit(int id, int projectId)
        {
            //var project = Repository.OfType<Project>().GetNullableByID(projectId);
            var requirement = Repository.OfType<Requirement>().GetNullableByID(id);
            
            if (requirement == null)
            {
                Message = string.Format(NotificationMessages.STR_ObjectNotFound, "Requirement");
                return this.RedirectToAction<ProjectController>(a => a.Index());
            }
            var requirementViewModel = RequirementViewModel.Create(Repository, requirement.Project);
            requirementViewModel.Requirement = requirement;

            return View(requirementViewModel);

        }

        [AcceptPost]
        public ActionResult Edit(int id, int projectId, Requirement requirement)
        {
            //TODO: Clean up once we figure out why it is saving even when invalid
            var dest = Repository.OfType<Requirement>().GetNullableByID(id);

            if (requirement == null)
            {
                Message = string.Format(NotificationMessages.STR_ObjectNotFound, "Requirement");
                return this.RedirectToAction<ProjectController>(a => a.Index());
            }
            Copiers.CopyRequirement(requirement, dest);
            dest.TransferValidationMessagesTo(ModelState);

            //var test = dest.IsValid();

            if (ModelState.IsValid)
            {
                Repository.OfType<Requirement>().EnsurePersistent(dest);
                Message = string.Format(NotificationMessages.STR_ObjectUpdated, "Requirement");
                //return this.RedirectToAction<ProjectController>(a => a.Edit(projectId));
                return Redirect(Url.EditProjectUrl(dest.Project.Id, StaticValues.Tab_Requirements));
            }
            //return this.RedirectToAction(a => a.Edit(id, projectId));
            var proj = Repository.OfType<Project>().GetNullableByID(projectId);
            var viewModel = RequirementViewModel.Create(Repository, proj);
            viewModel.Requirement = requirement;

            return View(viewModel);
        }
    }
}
