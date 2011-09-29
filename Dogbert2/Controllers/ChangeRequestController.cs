using System;
using System.Linq;
using System.Web.Mvc;
using Dogbert2.App_GlobalResources;
using Dogbert2.Core.Domain;
using Dogbert2.Models;
using Dogbert2.Services;
using UCDArch.Core.PersistanceSupport;
using MvcContrib;

namespace Dogbert2.Controllers
{
    /// <summary>
    /// Controller for the ChangeRequest class
    /// </summary>
    public class ChangeRequestController : ApplicationController
    {
	    private readonly IRepository<ChangeRequest> _changeRequestRepository;
        private readonly IRepository<Project> _projectRepository;
        private readonly IAccessValidatorService _accessValidator;

        public ChangeRequestController(IRepository<ChangeRequest> changeRequestRepository, IRepository<Project> projectRepository, IAccessValidatorService accessValidator )
        {
            _changeRequestRepository = changeRequestRepository;
            _projectRepository = projectRepository;
            _accessValidator = accessValidator;
        }

        /// <summary>
        /// List of all Change Requests per Project
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
            if (redirect != null) return redirect;

            var changeRequestList = _changeRequestRepository.Queryable.Where(a => a.Project.Id == id);

            ViewBag.ProjectId = id;

            return View(changeRequestList.ToList());
        }

        //
        // GET: /ChangeRequest/Details/5
        public ActionResult Details(int id)
        {
            var changeRequest = _changeRequestRepository.GetNullableById(id);

            if (changeRequest == null) return RedirectToAction("Index");

            return View(changeRequest);
        }

        /// <summary>
        /// Create a change request for a project
        /// </summary>
        /// <param name="id">Project Id</param>
        /// <returns></returns>
        public ActionResult Create(int id)
        {
            var project = _projectRepository.GetNullableById(id);

            if (project == null)
            {
                Message = string.Format(Messages.NotFound, "Project", id);
                return this.RedirectToAction<ProjectController>(a => a.Index());
            }

            // validate access
            var redirect = _accessValidator.CheckEditAccess(CurrentUser.Identity.Name, project);
            if (redirect != null) return redirect;

            var viewModel = ChangeRequestViewModel.Create(Repository, project);
            
            return View(viewModel);
        } 

        /// <summary>
        /// Create a change request
        /// </summary>
        /// <param name="id">Project Id</param>
        /// <param name="changeRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(int id, ChangeRequest changeRequest)
        {
            var project = _projectRepository.GetNullableById(id);

            if (project == null)
            {
                Message = string.Format(Messages.NotFound, "Project", id);
                return this.RedirectToAction<ProjectController>(a => a.Index());
            }

            // validate access
            var redirect = _accessValidator.CheckEditAccess(CurrentUser.Identity.Name, project);
            if (redirect != null) return redirect;

            var changeRequestToCreate = new ChangeRequest() {Project = project};

            AutoMapper.Mapper.Map(changeRequest, changeRequestToCreate);

            if (ModelState.IsValid)
            {
                _changeRequestRepository.EnsurePersistent(changeRequestToCreate);

                Message = "ChangeRequest Created Successfully";

                return RedirectToAction("Index", new {id=id});
            }
			
            var viewModel = ChangeRequestViewModel.Create(Repository, project, changeRequestToCreate);
            viewModel.ChangeRequest = changeRequest;

            return View(viewModel);
        }

        //
        // GET: /ChangeRequest/Edit/5
        public ActionResult Edit(int id)
        {
            var changeRequest = _changeRequestRepository.GetNullableById(id);

            if (changeRequest == null) return RedirectToAction("Index", new {id=changeRequest.Project.Id});

			var viewModel = ChangeRequestViewModel.Create(Repository, changeRequest.Project, changeRequest);

			return View(viewModel);
        }
        
        //
        // POST: /ChangeRequest/Edit/5
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(int id, ChangeRequest changeRequest)
        {
            var changeRequestToEdit = _changeRequestRepository.GetNullableById(id);

            if (changeRequestToEdit == null) if (changeRequest == null) return RedirectToAction("Index", "Project");

            AutoMapper.Mapper.Map(changeRequest, changeRequestToEdit);

            if (ModelState.IsValid)
            {
                _changeRequestRepository.EnsurePersistent(changeRequestToEdit);

                Message = "Change Request Edited Successfully";

                return RedirectToAction("Index", new {id=changeRequestToEdit.Project.Id});
            }
			
            var viewModel = ChangeRequestViewModel.Create(Repository, changeRequestToEdit.Project, changeRequest);

            return View(viewModel);
        }
        
        //
        // GET: /ChangeRequest/Delete/5 
        public ActionResult Delete(int id)
        {
			var changeRequest = _changeRequestRepository.GetNullableById(id);

            if (changeRequest == null) return RedirectToAction("Index", "Project");

            return View(changeRequest);
        }

        //
        // POST: /ChangeRequest/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, ChangeRequest changeRequest)
        {
			var changeRequestToDelete = _changeRequestRepository.GetNullableById(id);

            if (changeRequestToDelete == null) return RedirectToAction("Index", "Project");

            var projectId = changeRequestToDelete.Project.Id;

            _changeRequestRepository.Remove(changeRequestToDelete);

            Message = "Change Request Removed Successfully";

            return RedirectToAction("Index", new {id=projectId});
        }
        
        /// <summary>
        /// Transfer editable values from source to destination
        /// </summary>
        private static void TransferValues(ChangeRequest source, ChangeRequest destination)
        {
			//Recommendation: Use AutoMapper
			//Mapper.Map(source, destination)
            throw new NotImplementedException();
        }

    }
}
