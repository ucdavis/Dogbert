﻿using System;
using System.Linq;
using System.Web.Mvc;
using Dogbert2.Core.Domain;
using Dogbert2.Filters;
using Dogbert2.Models;
using Dogbert2.Services;
using UCDArch.Core.PersistanceSupport;
using MvcContrib;

namespace Dogbert2.Controllers
{
    /// <summary>
    /// Controller for the ProjectWorkgroup class
    /// </summary>
    [Authorize]
    public class ProjectWorkgroupController : ApplicationController
    {
        private readonly IRepository<ProjectWorkgroup> _projectWorkgroupRepository;
        private readonly IRepository<Project> _projectRepository;
        private readonly IAccessValidatorService _accessValidator;

        public ProjectWorkgroupController(IRepository<ProjectWorkgroup> projectWorkgroupRepository, IRepository<Project> projectRepository, IAccessValidatorService accessValidator)
        {
            _projectWorkgroupRepository = projectWorkgroupRepository;
            _projectRepository = projectRepository;
            _accessValidator = accessValidator;
        }

        /// <summary>
        /// GET: /ProjectWorkgroup/
        /// </summary>
        /// <param name="id">Project Id</param>
        /// <returns></returns>
        public ActionResult Index(int id)
        {
            var project = _projectRepository.GetNullableById(id);

            if (project == null) return this.RedirectToAction<ProjectController>(a => a.Index());

            // validate access
            var redirect = _accessValidator.CheckReadAccess(CurrentUser.Identity.Name, project);
            if (redirect != null)
            {
                Message = "Not authorized to edit project.";
                return redirect;
            }

            var projectWorkgroupList = _projectWorkgroupRepository.Queryable.Where(a => a.Project == project);

            ViewBag.ProjectId = id;

            return View(projectWorkgroupList.ToList());
        }


        /// <summary>
        /// Add a workgroup
        /// </summary>
        /// <param name="id">Project Id</param>
        /// <returns></returns>
        public ActionResult Add(int id)
        {
            var project = _projectRepository.GetNullableById(id);

            if (project == null) return this.RedirectToAction<ProjectController>(a => a.Index());

            // validate access
            var redirect = _accessValidator.CheckEditAccess(CurrentUser.Identity.Name, project);
            if (redirect != null)
            {
                Message = "Not authorized to edit project.";
                return redirect;
            }

            var viewModel = ProjectWorkgroupViewModel.Create(Repository, CurrentUser.Identity.Name, new ProjectWorkgroup() { Project = project});
            
            return View(viewModel);
        } 

        //
        // POST: /ProjectWorkgroup/Create
        [HttpPost]
        public ActionResult Add(ProjectWorkgroup projectWorkgroup)
        {
            // ensure we're not readding a workgroup already in there
            if (_projectWorkgroupRepository.Queryable.Any(a => a.Project == projectWorkgroup.Project && a.Workgroup == projectWorkgroup.Workgroup))
            {
                ModelState.AddModelError("", "Workgroup already added to project.");
            }

            // validate access
            var redirect = _accessValidator.CheckEditAccess(CurrentUser.Identity.Name, projectWorkgroup.Project);
            if (redirect != null)
            {
                Message = "Not authorized to edit project.";
                return redirect;
            }

            if (ModelState.IsValid)
            {
                _projectWorkgroupRepository.EnsurePersistent(projectWorkgroup);

                Message = "ProjectWorkgroup Created Successfully";

                //return RedirectToAction("Index");
                return this.RedirectToAction(a => a.Index(projectWorkgroup.Project.Id));
            }
			
            var viewModel = ProjectWorkgroupViewModel.Create(Repository, CurrentUser.Identity.Name);
            viewModel.ProjectWorkgroup = projectWorkgroup;

            return View(viewModel);
        }

       
        //
        // GET: /ProjectWorkgroup/Delete/5 
        public ActionResult Delete(int id)
        {
			var projectWorkgroup = _projectWorkgroupRepository.GetNullableById(id);

            if (projectWorkgroup == null) return RedirectToAction("Index");

            // validate access
            var redirect = _accessValidator.CheckEditAccess(CurrentUser.Identity.Name, projectWorkgroup.Project);
            if (redirect != null)
            {
                Message = "Not authorized to edit project.";
                return redirect;
            }

            return View(projectWorkgroup);
        }

        //
        // POST: /ProjectWorkgroup/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, ProjectWorkgroup projectWorkgroup)
        {
			var projectWorkgroupToDelete = _projectWorkgroupRepository.GetNullableById(id);

            if (projectWorkgroupToDelete == null) return this.RedirectToAction<ProjectController>(a => a.Index());

            // validate access
            var redirect = _accessValidator.CheckEditAccess(CurrentUser.Identity.Name, projectWorkgroupToDelete.Project);
            if (redirect != null)
            {
                Message = "Not authorized to edit project.";
                return redirect;
            }

            var projectId = projectWorkgroupToDelete.Project.Id;

            _projectWorkgroupRepository.Remove(projectWorkgroupToDelete);

            Message = "ProjectWorkgroup Removed Successfully";

            return this.RedirectToAction(a => a.Index(projectId));
        }
        
    }
}
