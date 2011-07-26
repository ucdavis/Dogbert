using System;
using System.Linq;
using System.Web.Mvc;
using Dogbert2.App_GlobalResources;
using Dogbert2.Core.Domain;
using Dogbert2.Models;
using Dogbert2.Services;
using UCDArch.Core.PersistanceSupport;
using MvcContrib;
using UCDArch.Web.Helpers;

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

        /// <summary>
        /// Create a new use case
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
            var redirect = _accessValidator.CheckReadAccess(CurrentUser.Identity.Name, project);
            if (redirect != null)
            {
                Message = "Not authorized to edit project.";
                return redirect;
            }

			var viewModel = UseCaseViewModel.Create(Repository, project);
            
            return View(viewModel);
        } 

        //
        // POST: /UseCase/Create
        [HttpPost]
        public ActionResult Create(int projectId, UseCase useCase)
        {
            var project = _projectRepository.GetNullableById(projectId);

            if (project == null)
            {
                Message = string.Format(Messages.NotFound, "Project", projectId);
                return this.RedirectToAction<ProjectController>(a => a.Index());
            }

            // validate access
            var redirect = _accessValidator.CheckReadAccess(CurrentUser.Identity.Name, project);
            if (redirect != null)
            {
                Message = "Not authorized to edit project.";
                return redirect;
            }

            ModelState.Clear();

            useCase.Project = project;
            useCase.TransferValidationMessagesTo(ModelState);

            foreach (var a in useCase.UseCaseSteps)
            {
                a.UseCase = useCase;
                a.TransferValidationMessagesTo(ModelState);
            }

           
            if (ModelState.IsValid)
            {
                _useCaseRepository.EnsurePersistent(useCase);

                Message = "UseCase Created Successfully";

                return RedirectToAction("Index", new {id=projectId});
            }
			
            var viewModel = UseCaseViewModel.Create(Repository, project);
            viewModel.UseCase = useCase;

            return View(viewModel);
        }

        //
        // GET: /UseCase/Edit/5
        public ActionResult Edit(int id)
        {
            var useCase = _useCaseRepository.GetNullableById(id);

            if (useCase == null) return RedirectToAction("Index");

			var viewModel = UseCaseViewModel.Create(Repository, null);
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
				var viewModel = UseCaseViewModel.Create(Repository, null);
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

            var projectId = useCaseToDelete.Project.Id;

            _useCaseRepository.Remove(useCaseToDelete);

            Message = "UseCase Removed Successfully";

            return this.RedirectToAction(a => a.Index(projectId));
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
}
