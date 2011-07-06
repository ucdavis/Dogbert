using System;
using System.Linq;
using System.Web.Mvc;
using Dogbert2.Core.Domain;
using Dogbert2.Models;
using UCDArch.Core.PersistanceSupport;

namespace Dogbert2.Controllers
{
    /// <summary>
    /// Controller for the Project class
    /// </summary>
    [Authorize]
    public class ProjectController : ApplicationController
    {
	    private readonly IRepository<Project> _projectRepository;

        public ProjectController(IRepository<Project> projectRepository)
        {
            _projectRepository = projectRepository;
        }
    
        //
        // GET: /Project/
        public ActionResult Index()
        {
            var projectList = _projectRepository.Queryable.Where(a=>!a.Hide);

            return View(projectList.ToList());
        }


        //
        // GET: /Project/Details/5
        public ActionResult Details(int id)
        {
            var project = _projectRepository.GetNullableById(id);

            if (project == null) return RedirectToAction("Index");

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

            return View(project);
        }

        //
        // POST: /Project/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Project project)
        {
			var projectToDelete = _projectRepository.GetNullableById(id);

            if (projectToDelete == null) return RedirectToAction("Index");

            projectToDelete.Hide = true;
            _projectRepository.EnsurePersistent(projectToDelete);
            //_projectRepository.Remove(projectToDelete);

            Message = "Project Removed Successfully";

            return RedirectToAction("Index");
        }
    }
}
