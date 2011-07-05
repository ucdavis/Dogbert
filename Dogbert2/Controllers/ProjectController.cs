

using System;
using System.Linq;
using System.Web.Mvc;
using Dogbert2.Core.Domain;
using UCDArch.Core.PersistanceSupport;
using UCDArch.Core.Utils;

namespace Dogbert2.Controllers
{
    /// <summary>
    /// Controller for the Project class
    /// </summary>
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
            var projectList = _projectRepository.Queryable;

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
			var viewModel = ProjectViewModel.Create(Repository);
            
            return View(viewModel);
        } 

        //
        // POST: /Project/Create
        [HttpPost]
        public ActionResult Create(Project project)
        {
            var projectToCreate = new Project();

            TransferValues(project, projectToCreate);

            if (ModelState.IsValid)
            {
                _projectRepository.EnsurePersistent(projectToCreate);

                Message = "Project Created Successfully";

                return RedirectToAction("Index");
            }
            else
            {
				var viewModel = ProjectViewModel.Create(Repository);
                viewModel.Project = project;

                return View(viewModel);
            }
        }

        //
        // GET: /Project/Edit/5
        public ActionResult Edit(int id)
        {
            var project = _projectRepository.GetNullableById(id);

            if (project == null) return RedirectToAction("Index");

			var viewModel = ProjectViewModel.Create(Repository);
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

            TransferValues(project, projectToEdit);

            if (ModelState.IsValid)
            {
                _projectRepository.EnsurePersistent(projectToEdit);

                Message = "Project Edited Successfully";

                return RedirectToAction("Index");
            }
            else
            {
				var viewModel = ProjectViewModel.Create(Repository);
                viewModel.Project = project;

                return View(viewModel);
            }
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

            _projectRepository.Remove(projectToDelete);

            Message = "Project Removed Successfully";

            return RedirectToAction("Index");
        }
        
        /// <summary>
        /// Transfer editable values from source to destination
        /// </summary>
        private static void TransferValues(Project source, Project destination)
        {
			//Recommendation: Use AutoMapper
			//Mapper.Map(source, destination)
            throw new NotImplementedException();
        }


    }

	/// <summary>
    /// ViewModel for the Project class
    /// </summary>
    public class ProjectViewModel
	{
		public Project Project { get; set; }
 
		public static ProjectViewModel Create(IRepository repository)
		{
			Check.Require(repository != null, "Repository must be supplied");
			
			var viewModel = new ProjectViewModel {Project = new Project()};
 
			return viewModel;
		}
	}
}
