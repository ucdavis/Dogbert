﻿using System.Web.Mvc;
using Dogbert2.App_GlobalResources;
using Dogbert2.Core.Domain;
using Dogbert2.Filters;
using Dogbert2.Models;
using Dogbert2.Services;
using UCDArch.Core.PersistanceSupport;
using MvcContrib;
using UCDArch.Web.Helpers;

namespace Dogbert2.Controllers
{
    /// <summary>
    /// Controller for the ProjectTerm class
    /// </summary>
    [Authorize]
    public class ProjectTermController : ApplicationController
    {
        private readonly IRepository<Project> _projectRepository;
        private readonly IRepository<ProjectTerm> _projectTermRepository;
        private readonly IAccessValidatorService _accessValidator;

        public ProjectTermController(IRepository<Project> projectRepository, IRepository<ProjectTerm> projectTermRepository, IAccessValidatorService accessValidator)
        {
            _projectRepository = projectRepository;
            _projectTermRepository = projectTermRepository;
            _accessValidator = accessValidator;
        }

        /// <summary>
        /// List of all project terms in a project
        /// </summary>
        /// <param name="projectId">project id</param>
        /// <returns></returns>
        public ActionResult Index(int id)
        {
            var project = _projectRepository.GetNullableById(id);

            if (project == null)
            {
                Message = string.Format(Messages.NotFound, "Project", id);
                return this.RedirectToAction<ProjectController>(a => a.Index());
            }

            // validate access
            var redirect = _accessValidator.CheckReadAccess(CurrentUser.Identity.Name, project);
            if (redirect != null) return redirect;

            ViewBag.ProjectId = project.Id;

            return View(project.ProjectTerms);
        }

        /// <summary>
        /// Create a project term for a project
        /// </summary>
        /// <param name="projectId">Project Id</param>
        /// <returns></returns>
        public ActionResult Create(int projectId)
        {
            var project = _projectRepository.GetNullableById(projectId);

            if (project == null)
            {
                Message = string.Format(Messages.NotFound, "Project", projectId);
                return this.RedirectToAction<ProjectController>(a => a.Index());
            }

            // validate access
            var redirect = _accessValidator.CheckEditAccess(CurrentUser.Identity.Name, project);
            if (redirect != null)
            {
                Message = "Not authorized to edit project.";
                return redirect;
            }

			var viewModel = ProjectTermViewModel.Create(Repository, project);            

            return View(viewModel);
        } 

        //
        // POST: /ProjectTerm/Create
        [HttpPost]
        public ActionResult Create(int projectId, ProjectTerm projectTerm)
        {
            var project = _projectRepository.GetNullableById(projectId);

            if (project == null)
            {
                Message = string.Format(Messages.NotFound, "Project", projectId);
                return this.RedirectToAction<ProjectController>(a => a.Index());
            }

            // validate access
            var redirect = _accessValidator.CheckEditAccess(CurrentUser.Identity.Name, project);
            if (redirect != null)
            {
                Message = "Not authorized to edit project.";
                return redirect;
            }

            projectTerm.Project = project;

            ModelState.Clear();
            projectTerm.TransferValidationMessagesTo(ModelState);

            if (ModelState.IsValid)
            {
                _projectTermRepository.EnsurePersistent(projectTerm);

                Message = "ProjectTerm Created Successfully";

                //return RedirectToAction("Index");
                return this.RedirectToAction(a => a.Index(project.Id));
            }
            
            var viewModel = ProjectTermViewModel.Create(Repository, project);
            viewModel.ProjectTerm = projectTerm;

            return View(viewModel);
        }

        //
        // GET: /ProjectTerm/Edit/5
        public ActionResult Edit(int id)
        {
            var projectTerm = _projectTermRepository.GetNullableById(id);

            if (projectTerm == null)
            {
                Message = string.Format(Messages.NotFound, "Project Term", id);
                return RedirectToAction("Index");
            }

            // validate access
            var redirect = _accessValidator.CheckEditAccess(CurrentUser.Identity.Name, projectTerm.Project);
            if (redirect != null)
            {
                Message = "Not authorized to edit project.";
                return redirect;
            }

			var viewModel = ProjectTermViewModel.Create(Repository, projectTerm.Project);
			viewModel.ProjectTerm = projectTerm;

			return View(viewModel);
        }

        //
        // POST: /ProjectTerm/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ProjectTerm projectTerm)
        {
            var projectTermToEdit = _projectTermRepository.GetNullableById(id);

            if (projectTermToEdit == null)
            {
                Message = string.Format(Messages.NotFound, "Project Term", id);
                return RedirectToAction("Index");
            }

            // validate access
            var redirect = _accessValidator.CheckEditAccess(CurrentUser.Identity.Name, projectTermToEdit.Project);
            if (redirect != null)
            {
                Message = "Not authorized to edit project.";
                return redirect;
            }

            AutoMapper.Mapper.Map(projectTerm, projectTermToEdit);

            ModelState.Clear();
            projectTermToEdit.TransferValidationMessagesTo(ModelState);

            if (ModelState.IsValid)
            {
                _projectTermRepository.EnsurePersistent(projectTermToEdit);

                Message = "ProjectTerm Edited Successfully";

                return RedirectToAction("Index", new {id=projectTermToEdit.Project.Id});
            }
            
            var viewModel = ProjectTermViewModel.Create(Repository, projectTermToEdit.Project);
            viewModel.ProjectTerm = projectTermToEdit;

            return View(viewModel);
        }

        //
        // GET: /ProjectTerm/Delete/5 
        public ActionResult Delete(int id)
        {
            var projectTerm = _projectTermRepository.GetNullableById(id);

            if (projectTerm == null) return RedirectToAction("Index");

            // validate access
            var redirect = _accessValidator.CheckEditAccess(CurrentUser.Identity.Name, projectTerm.Project);
            if (redirect != null)
            {
                Message = "Not authorized to edit project.";
                return redirect;
            }

            return View(projectTerm);
        }

        //
        // POST: /ProjectTerm/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, ProjectTerm projectTerm)
        {
            var projectTermToDelete = _projectTermRepository.GetNullableById(id);

            if (projectTermToDelete == null) return RedirectToAction("Index");

            // validate access
            var redirect = _accessValidator.CheckEditAccess(CurrentUser.Identity.Name, projectTermToDelete.Project);
            if (redirect != null)
            {
                Message = "Not authorized to edit project.";
                return redirect;
            }

            var projectId = projectTermToDelete.Project.Id;

            _projectTermRepository.Remove(projectTermToDelete);

            Message = "ProjectTerm Removed Successfully";

            return this.RedirectToAction(a => a.Index(projectId));
        }
    }
}
