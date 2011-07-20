using System;
using System.Web.Mvc;
using Dogbert2.App_GlobalResources;
using Dogbert2.Core.Domain;
using Dogbert2.Services;
using UCDArch.Core.PersistanceSupport;
using UCDArch.Core.Utils;
using MvcContrib;

namespace Dogbert2.Controllers
{
    /// <summary>
    /// Controller for the UseCase class
    /// </summary>
    public class UseCaseController : ApplicationController
    {
	    private readonly IRepository<UseCase> _useCaseRepository;
        private readonly IRepository<Project> _projectRepository;
        private readonly IAccessValidatorService _accessValidator;

        public UseCaseController(IRepository<UseCase> useCaseRepository, IRepository<Project> projectRepository, IAccessValidatorService accessValidator)
        {
            _useCaseRepository = useCaseRepository;
            _projectRepository = projectRepository;
            _accessValidator = accessValidator;
        }

        /// <summary>
        /// List all use cases for a project
        /// </summary>
        /// <param name="id">Project Id</param>
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
            if (redirect != null)
            {
                Message = "Not authorized to edit project.";
                return redirect;
            }

            ViewBag.ProjectId = id;

            return View(project.UseCases);
        }

        //
        // GET: /UseCase/Details/5
        public ActionResult Details(int id)
        {
            var useCase = _useCaseRepository.GetNullableById(id);

            if (useCase == null) return RedirectToAction("Index");

            return View(useCase);
        }

        //
        // GET: /UseCase/Create
        public ActionResult Create()
        {
			var viewModel = UseCaseViewModel.Create(Repository);
            
            return View(viewModel);
        } 

        //
        // POST: /UseCase/Create
        [HttpPost]
        public ActionResult Create(UseCase useCase)
        {
            var useCaseToCreate = new UseCase();

            TransferValues(useCase, useCaseToCreate);

            if (ModelState.IsValid)
            {
                _useCaseRepository.EnsurePersistent(useCaseToCreate);

                Message = "UseCase Created Successfully";

                return RedirectToAction("Index");
            }
            else
            {
				var viewModel = UseCaseViewModel.Create(Repository);
                viewModel.UseCase = useCase;

                return View(viewModel);
            }
        }

        //
        // GET: /UseCase/Edit/5
        public ActionResult Edit(int id)
        {
            var useCase = _useCaseRepository.GetNullableById(id);

            if (useCase == null) return RedirectToAction("Index");

			var viewModel = UseCaseViewModel.Create(Repository);
			viewModel.UseCase = useCase;

			return View(viewModel);
        }
        
        //
        // POST: /UseCase/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, UseCase useCase)
        {
            var useCaseToEdit = _useCaseRepository.GetNullableById(id);

            if (useCaseToEdit == null) return RedirectToAction("Index");

            TransferValues(useCase, useCaseToEdit);

            if (ModelState.IsValid)
            {
                _useCaseRepository.EnsurePersistent(useCaseToEdit);

                Message = "UseCase Edited Successfully";

                return RedirectToAction("Index");
            }
            else
            {
				var viewModel = UseCaseViewModel.Create(Repository);
                viewModel.UseCase = useCase;

                return View(viewModel);
            }
        }
        
        //
        // GET: /UseCase/Delete/5 
        public ActionResult Delete(int id)
        {
			var useCase = _useCaseRepository.GetNullableById(id);

            if (useCase == null) return RedirectToAction("Index");

            return View(useCase);
        }

        //
        // POST: /UseCase/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, UseCase useCase)
        {
			var useCaseToDelete = _useCaseRepository.GetNullableById(id);

            if (useCaseToDelete == null) return RedirectToAction("Index");

            _useCaseRepository.Remove(useCaseToDelete);

            Message = "UseCase Removed Successfully";

            return RedirectToAction("Index");
        }
        
        /// <summary>
        /// Transfer editable values from source to destination
        /// </summary>
        private static void TransferValues(UseCase source, UseCase destination)
        {
			//Recommendation: Use AutoMapper
			//Mapper.Map(source, destination)
            throw new NotImplementedException();
        }

    }

	/// <summary>
    /// ViewModel for the UseCase class
    /// </summary>
    public class UseCaseViewModel
	{
		public UseCase UseCase { get; set; }
 
		public static UseCaseViewModel Create(IRepository repository)
		{
			Check.Require(repository != null, "Repository must be supplied");
			
			var viewModel = new UseCaseViewModel {UseCase = new UseCase()};
 
			return viewModel;
		}
	}
}
