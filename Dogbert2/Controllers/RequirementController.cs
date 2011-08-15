using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Dogbert2.App_GlobalResources;
using Dogbert2.Core.Domain;
using Dogbert2.Filters;
using Dogbert2.Models;
using Dogbert2.Services;
using UCDArch.Core.PersistanceSupport;
using UCDArch.Web.ActionResults;
using UCDArch.Web.Helpers;
using MvcContrib;

namespace Dogbert2.Controllers
{
    /// <summary>
    /// Controller for the Requirement class
    /// </summary>
    [Authorize]
    public class RequirementController : ApplicationController
    {
	    private readonly IRepository<Requirement> _requirementRepository;
        private readonly IRepository<Project> _projectRepository;
        private readonly IAccessValidatorService _accessValidator;

        public RequirementController(IRepository<Requirement> requirementRepository, IRepository<Project> projectRepository, IAccessValidatorService accessValidator)
        {
            _requirementRepository = requirementRepository;
            _projectRepository = projectRepository;
            _accessValidator = accessValidator;
        }

        /// <summary>
        /// GET: /Requirement/
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

            return View(project.Requirements.OrderBy(a => a.RequirementCategory.Name).ThenBy(a => a.Order).ToList());
        }

        /// <summary>
        /// Reorder requirements inside their categories
        /// </summary>
        /// <param name="id">Project Id</param>
        /// <returns></returns>
        public ActionResult Reorder(int id)
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

            return View(project.Requirements.OrderBy(a => a.RequirementCategory.Name).ThenBy(a => a.Order).ToList());
        }

        [HttpPost]
        public JsonResult UpdateOrder(int projectId, int categoryId, List<int> requirementIds)
        {
            try
            {
                var project = _projectRepository.GetNullableById(projectId);
                var category = Repository.OfType<RequirementCategory>().GetNullableById(categoryId);

                var requirements = project.Requirements.Where(a => a.RequirementCategory == category);

                for (var i = 0; i < requirementIds.Count; i++)
                {
                    var requirement = requirements.Where(a => a.Id == requirementIds[i]).FirstOrDefault();

                    requirement.Order = i + 1;

                    _requirementRepository.EnsurePersistent(requirement);
                }

                return Json(true);
            }
            catch 
            {
                return Json(false);
            }
        }

        //
        // GET: /Requirement/Create
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
            if (redirect != null)
            {
                Message = "Not authorized to edit project.";
                return redirect;
            }

			var viewModel = RequirementViewModel.Create(Repository, project);
            
            return View(viewModel);
        } 

        //
        // POST: /Requirement/Create
        [HttpPost]
        public ActionResult Create(int id, [Bind(Exclude="Project, RequirementId")]Requirement requirement)
        {
            var project = _projectRepository.GetNullableById(id);

            if (project == null)
            {
                Message = string.Format(Messages.NotFound, "Project", id);
                return this.RedirectToAction<ProjectController>(a => a.Index());
            }

            // validate access
            var redirect = _accessValidator.CheckEditAccess(CurrentUser.Identity.Name, project);
            if (redirect != null)
            {
                Message = "Not authorized to edit project.";
                return redirect;
            }

            if (project.Requirements.Count > 0)
            {
                // get the highest requirement id
                var rid = project.Requirements.Select(a => a.Id).Max();
                var maxId = project.Requirements.Where(a => a.Id == rid).Select(a => a.RequirementId).FirstOrDefault();
                // parse the int
                var mid = maxId.Substring(1, maxId.Length - 1);
                requirement.RequirementId = string.Format("R{0}", Convert.ToInt32(mid) + 1);    
            }
            else
            {
                requirement.RequirementId = "R1";
            }

            requirement.Project = project;

            ModelState.Clear();
            requirement.TransferValidationMessagesTo(ModelState);

            if (ModelState.IsValid)
            {
                _requirementRepository.EnsurePersistent(requirement);

                Message = "Requirement Created Successfully";

                return RedirectToAction("Index", new {id=id});
            }
            else
            {
				var viewModel = RequirementViewModel.Create(Repository, project);
                viewModel.Requirement = requirement;

                return View(viewModel);
            }
        }

        //
        // GET: /Requirement/Edit/5
        public ActionResult Edit(int id)
        {
            var requirement = _requirementRepository.GetNullableById(id);

            if (requirement == null) return RedirectToAction("Index");

            // validate access
            var redirect = _accessValidator.CheckEditAccess(CurrentUser.Identity.Name, requirement.Project);
            if (redirect != null)
            {
                Message = "Not authorized to edit project.";
                return redirect;
            }

			var viewModel = RequirementViewModel.Create(Repository, requirement.Project);
			viewModel.Requirement = requirement;

			return View(viewModel);
        }
        
        //
        // POST: /Requirement/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Requirement requirement)
        {
            var requirementToEdit = _requirementRepository.GetNullableById(id);

            if (requirementToEdit == null) return RedirectToAction("Index", new {id=id});

            // validate access
            var redirect = _accessValidator.CheckEditAccess(CurrentUser.Identity.Name, requirementToEdit.Project);
            if (redirect != null)
            {
                Message = "Not authorized to edit project.";
                return redirect;
            }

            AutoMapper.Mapper.Map(requirement, requirementToEdit);
            requirementToEdit.LastModified = DateTime.Now;

            ModelState.Clear();
            requirementToEdit.TransferValidationMessagesTo(ModelState);

            if (ModelState.IsValid)
            {
                _requirementRepository.EnsurePersistent(requirementToEdit);

                Message = "Requirement Edited Successfully";

                return RedirectToAction("Index", new {id=requirementToEdit.Project.Id});
            }
			
            var viewModel = RequirementViewModel.Create(Repository, requirementToEdit.Project);
            viewModel.Requirement = requirement;

            return View(viewModel);
        }
        
        //
        // GET: /Requirement/Delete/5 
        public ActionResult Delete(int id)
        {
			var requirement = _requirementRepository.GetNullableById(id);

            if (requirement == null) return this.RedirectToAction<ProjectController>(a => a.Index());

            // validate access
            var redirect = _accessValidator.CheckEditAccess(CurrentUser.Identity.Name, requirement.Project);
            if (redirect != null)
            {
                Message = "Not authorized to edit project.";
                return redirect;
            }

            return View(requirement);
        }

        //
        // POST: /Requirement/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Requirement requirement)
        {
			var requirementToDelete = _requirementRepository.GetNullableById(id);

            if (requirementToDelete == null) return this.RedirectToAction<ProjectController>(a => a.Index());

            // validate access
            var redirect = _accessValidator.CheckEditAccess(CurrentUser.Identity.Name, requirementToDelete.Project);
            if (redirect != null)
            {
                Message = "Not authorized to edit project.";
                return redirect;
            }

            var projectId = requirementToDelete.Project.Id;

            _requirementRepository.Remove(requirementToDelete);

            Message = "Requirement Removed Successfully";

            return RedirectToAction("Index", new {id = projectId});
        }
    }
}
