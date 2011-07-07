

using System;
using System.Linq;
using System.Web.Mvc;
using Dogbert2.Core.Domain;
using UCDArch.Core.PersistanceSupport;
using UCDArch.Core.Utils;

namespace Dogbert2.Controllers
{
    /// <summary>
    /// Controller for the ProjectText class
    /// </summary>
    public class ProjectTextController : ApplicationController
    {
	    private readonly IRepository<ProjectText> _projectTextRepository;

        public ProjectTextController(IRepository<ProjectText> projectTextRepository)
        {
            _projectTextRepository = projectTextRepository;
        }
    
        //
        // GET: /ProjectText/
        public ActionResult Index()
        {
            var projectTextList = _projectTextRepository.Queryable;

            return View(projectTextList.ToList());
        }


        //
        // GET: /ProjectText/Details/5
        public ActionResult Details(int id)
        {
            var projectText = _projectTextRepository.GetNullableById(id);

            if (projectText == null) return RedirectToAction("Index");

            return View(projectText);
        }

        //
        // GET: /ProjectText/Create
        public ActionResult Create()
        {
			var viewModel = ProjectTextViewModel.Create(Repository);
            
            return View(viewModel);
        } 

        //
        // POST: /ProjectText/Create
        [HttpPost]
        public ActionResult Create(ProjectText projectText)
        {
            var projectTextToCreate = new ProjectText();

            TransferValues(projectText, projectTextToCreate);

            if (ModelState.IsValid)
            {
                _projectTextRepository.EnsurePersistent(projectTextToCreate);

                Message = "ProjectText Created Successfully";

                return RedirectToAction("Index");
            }
            else
            {
				var viewModel = ProjectTextViewModel.Create(Repository);
                viewModel.ProjectText = projectText;

                return View(viewModel);
            }
        }

        //
        // GET: /ProjectText/Edit/5
        public ActionResult Edit(int id)
        {
            var projectText = _projectTextRepository.GetNullableById(id);

            if (projectText == null) return RedirectToAction("Index");

			var viewModel = ProjectTextViewModel.Create(Repository);
			viewModel.ProjectText = projectText;

			return View(viewModel);
        }
        
        //
        // POST: /ProjectText/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ProjectText projectText)
        {
            var projectTextToEdit = _projectTextRepository.GetNullableById(id);

            if (projectTextToEdit == null) return RedirectToAction("Index");

            TransferValues(projectText, projectTextToEdit);

            if (ModelState.IsValid)
            {
                _projectTextRepository.EnsurePersistent(projectTextToEdit);

                Message = "ProjectText Edited Successfully";

                return RedirectToAction("Index");
            }
            else
            {
				var viewModel = ProjectTextViewModel.Create(Repository);
                viewModel.ProjectText = projectText;

                return View(viewModel);
            }
        }
        
        //
        // GET: /ProjectText/Delete/5 
        public ActionResult Delete(int id)
        {
			var projectText = _projectTextRepository.GetNullableById(id);

            if (projectText == null) return RedirectToAction("Index");

            return View(projectText);
        }

        //
        // POST: /ProjectText/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, ProjectText projectText)
        {
			var projectTextToDelete = _projectTextRepository.GetNullableById(id);

            if (projectTextToDelete == null) return RedirectToAction("Index");

            _projectTextRepository.Remove(projectTextToDelete);

            Message = "ProjectText Removed Successfully";

            return RedirectToAction("Index");
        }
        
        /// <summary>
        /// Transfer editable values from source to destination
        /// </summary>
        private static void TransferValues(ProjectText source, ProjectText destination)
        {
			//Recommendation: Use AutoMapper
			//Mapper.Map(source, destination)
            throw new NotImplementedException();
        }


    }

	/// <summary>
    /// ViewModel for the ProjectText class
    /// </summary>
    public class ProjectTextViewModel
	{
		public ProjectText ProjectText { get; set; }
 
		public static ProjectTextViewModel Create(IRepository repository)
		{
			Check.Require(repository != null, "Repository must be supplied");
			
			var viewModel = new ProjectTextViewModel {ProjectText = new ProjectText()};
 
			return viewModel;
		}
	}
}
