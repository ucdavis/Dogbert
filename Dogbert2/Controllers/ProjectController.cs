using System;
using System.Linq;
using System.Web.Mvc;
using Dogbert2.Core.Domain;
using Dogbert2.Filters;
using Dogbert2.Models;
using Dogbert2.Services;
using UCDArch.Core.PersistanceSupport;

namespace Dogbert2.Controllers
{
    /// <summary>
    /// Controller for the Project class
    /// </summary>
    [AllRoles]
    public class ProjectController : ApplicationController
    {
	    private readonly IRepository<Project> _projectRepository;
        private readonly IRepository<ProjectWorkgroup> _projectWorkgroupRepository;
        private readonly IAccessValidatorService _accessValidator;

        public ProjectController(IRepository<Project> projectRepository, IRepository<ProjectWorkgroup> projectWorkgroupRepository, IAccessValidatorService accessValidator)
        {
            _projectRepository = projectRepository;
            _projectWorkgroupRepository = projectWorkgroupRepository;
            _accessValidator = accessValidator;
        }

        //
        // GET: /Project/
        public ActionResult Index()
        {
            var workgroups = _accessValidator.GetWorkgroupsByUser(CurrentUser.Identity.Name);

            var projects = _projectWorkgroupRepository.Queryable.Where(a => !a.Project.Hide && workgroups.Contains(a.Workgroup)).Select(a => a.Project).Distinct();

            return View(projects.ToList());
        }

        /// <summary>
        /// Reorder and organize projects for which user has admin access to workgroup
        /// </summary>
        /// <returns></returns>
        public ActionResult Manage()
        {
            var viewModel = ProjectListViewModel.Create(Repository, CurrentUser.Identity.Name);
            
            return View(viewModel);
        }

        //
        // GET: /Project/Details/5
        public ActionResult Details(int id)
        {
            var project = _projectRepository.GetNullableById(id);

            if (project == null) return RedirectToAction("Index");

            // validate access
            var redirect = _accessValidator.CheckReadAccess(CurrentUser.Identity.Name, project);
            if (redirect != null) return redirect;

            return View(project);
        }

        //
        // GET: /Project/Create
        public ActionResult Create()
        {
			var viewModel = ProjectViewModel.Create(Repository, CurrentUser.Identity.Name);
            
            return View(viewModel);
        } 

        //
        // POST: /Project/Create
        [HttpPost]
        public ActionResult Create(ProjectPostModel projectPostModel)
        {
            var project = projectPostModel.Project;
            var workgroup = projectPostModel.Workgroup;

            if (workgroup == null)
            {
                ModelState.AddModelError("Workgroup", "Workgroup is required.");
            }

            project.AddWorkgroup(workgroup);

            if (ModelState.IsValid)
            {
                _projectRepository.EnsurePersistent(project);

                Message = "Project Created Successfully";

                return RedirectToAction("Index");
            }
			
            var viewModel = ProjectViewModel.Create(Repository, CurrentUser.Identity.Name);
            viewModel.Project = project;
            viewModel.Workgroup = workgroup;

            return View(viewModel);
        }

        //
        // GET: /Project/Edit/5
        public ActionResult Edit(int id)
        {
            var project = _projectRepository.GetNullableById(id);

            if (project == null) return RedirectToAction("Index");

            // validate access
            var redirect = _accessValidator.CheckEditAccess(CurrentUser.Identity.Name, project);
            if (redirect != null)
            {
                Message = "Not authorized to edit project.";
                return redirect;
            }

			var viewModel = ProjectViewModel.Create(Repository, CurrentUser.Identity.Name);
			viewModel.Project = project;

			return View(viewModel);
        }
        
        //
        // POST: /Project/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Project project)
        {
            var projectToEdit = _projectRepository.GetNullableById(id);

            if (projectToEdit == null) return RedirectToAction("Index");

            // validate access
            var redirect = _accessValidator.CheckEditAccess(CurrentUser.Identity.Name, project);
            if (redirect != null)
            {
                Message = "Not authorized to edit project.";
                return redirect;
            }

            AutoMapper.Mapper.Map(project, projectToEdit);
            projectToEdit.LastUpdate = DateTime.Now;

            if (ModelState.IsValid)
            {
                _projectRepository.EnsurePersistent(projectToEdit);

                Message = "Project Edited Successfully";

                return RedirectToAction("Index");
            }
			
            var viewModel = ProjectViewModel.Create(Repository, CurrentUser.Identity.Name);
            viewModel.Project = project;

            return View(viewModel);
        }
        
        //
        // GET: /Project/Delete/5 
        public ActionResult Delete(int id)
        {
			var project = _projectRepository.GetNullableById(id);

            if (project == null) return RedirectToAction("Index");

            // validate access
            var redirect = _accessValidator.CheckEditAccess(CurrentUser.Identity.Name, project);
            if (redirect != null)
            {
                Message = "Not authorized to edit project.";
                return redirect;
            }

            return View(project);
        }

        //
        // POST: /Project/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Project project)
        {
			var projectToDelete = _projectRepository.GetNullableById(id);

            if (projectToDelete == null) return RedirectToAction("Index");

            // validate access
            var redirect = _accessValidator.CheckEditAccess(CurrentUser.Identity.Name, project);
            if (redirect != null)
            {
                Message = "Not authorized to edit project.";
                return redirect;
            }

            projectToDelete.Hide = true;
            _projectRepository.EnsurePersistent(projectToDelete);

            Message = "Project Removed Successfully";

            return RedirectToAction("Index");
        }
    }
}
