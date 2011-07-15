using System;
using System.Linq;
using System.Web.Mvc;
using Dogbert2.Core.Domain;
using Dogbert2.Models;
using UCDArch.Core.PersistanceSupport;
using UCDArch.Web.Helpers;
using MvcContrib;

namespace Dogbert2.Controllers
{
    /// <summary>
    /// Controller for the Requirement class
    /// </summary>
    public class RequirementController : ApplicationController
    {
	    private readonly IRepository<Requirement> _requirementRepository;
        private readonly IRepository<Project> _projectRepository;

        public RequirementController(IRepository<Requirement> requirementRepository, IRepository<Project> projectRepository)
        {
            _requirementRepository = requirementRepository;
            _projectRepository = projectRepository;
        }

        /// <summary>
        /// GET: /Requirement/
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Index(int id)
        {
            var requirementList = _requirementRepository.Queryable.Where(a => a.Project.Id == id);

            ViewBag.ProjectId = id;

            return View(requirementList.ToList());
        }

        //
        // GET: /Requirement/Create
        public ActionResult Create(int id)
        {
            var project = _projectRepository.GetNullableById(id);

			var viewModel = RequirementViewModel.Create(Repository, project);
            
            return View(viewModel);
        } 

        //
        // POST: /Requirement/Create
        [HttpPost]
        public ActionResult Create(int id, [Bind(Exclude="Project, RequirementId")]Requirement requirement)
        {
            var project = _projectRepository.GetNullableById(id);

            requirement.RequirementId = string.Format("R{0}", project.Requirements.Count() + 1);
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

            return View(requirement);
        }

        //
        // POST: /Requirement/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Requirement requirement)
        {
			var requirementToDelete = _requirementRepository.GetNullableById(id);

            if (requirementToDelete == null) return this.RedirectToAction<ProjectController>(a => a.Index());

            var projectId = requirementToDelete.Project.Id;

            _requirementRepository.Remove(requirementToDelete);

            Message = "Requirement Removed Successfully";

            return RedirectToAction("Index", new {id = projectId});
        }
    }
}
