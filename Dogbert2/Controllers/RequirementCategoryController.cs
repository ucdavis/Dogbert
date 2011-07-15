using System;
using System.Linq;
using System.Web.Mvc;
using Dogbert2.App_GlobalResources;
using Dogbert2.Core.Domain;
using Dogbert2.Models;
using UCDArch.Core.PersistanceSupport;
using MvcContrib;
using UCDArch.Web.Helpers;

namespace Dogbert2.Controllers
{
    /// <summary>
    /// Controller for the RequirementCategory class
    /// </summary>
    public class RequirementCategoryController : ApplicationController
    {
	    private readonly IRepository<RequirementCategory> _requirementCategoryRepository;
        private readonly IRepository<Project> _projectRepository;

        public RequirementCategoryController(IRepository<RequirementCategory> requirementCategoryRepository, IRepository<Project> projectRepository)
        {
            _requirementCategoryRepository = requirementCategoryRepository;
            _projectRepository = projectRepository;
        }

        /// <summary>
        /// List of requirement categories by projects
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

            ViewBag.ProjectId = project.Id;

            return View(project.RequirementCategories.Where(a => a.IsActive).ToList());
        }

        /// <summary>
        /// Create a project requirement category
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

			var viewModel = RequirementCategoryViewModel.Create(Repository, project);
            
            return View(viewModel);
        } 

        //
        // POST: /RequirementCategory/Create
        [HttpPost]
        public ActionResult Create(int id, RequirementCategory requirementCategory)
        {
            var project = _projectRepository.GetNullableById(id);

            if (project == null)
            {
                Message = string.Format(Messages.NotFound, "Project", id);
                return this.RedirectToAction<ProjectController>(a => a.Index());
            }

            var existing = project.RequirementCategories.Where(a => a.Name == requirementCategory.Name).FirstOrDefault();

            if (existing != null)
            {
                if (existing.IsActive)
                {
                    ModelState.AddModelError("", "Requirement Cateogry already exists.");
                }
                else
                {
                    requirementCategory = existing;
                    requirementCategory.IsActive = true;
                }  
            }
            else
            {
                requirementCategory.Project = project;

                ModelState.Clear();
                requirementCategory.TransferValidationMessagesTo(ModelState);    
            }

            if (ModelState.IsValid)
            {
                _requirementCategoryRepository.EnsurePersistent(requirementCategory);

                Message = "RequirementCategory Created Successfully";

                return RedirectToAction("Index", new {id = id});
            }
			
            var viewModel = RequirementCategoryViewModel.Create(Repository, project);
            viewModel.RequirementCategory = requirementCategory;

            return View(viewModel);
        }

        //
        // GET: /RequirementCategory/Edit/5
        public ActionResult Edit(int id)
        {
            var requirementCategory = _requirementCategoryRepository.GetNullableById(id);

            if (requirementCategory == null) return this.RedirectToAction<ProjectController>(a=>a.Index());

			var viewModel = RequirementCategoryViewModel.Create(Repository, requirementCategory.Project);
			viewModel.RequirementCategory = requirementCategory;

			return View(viewModel);
        }
        
        //
        // POST: /RequirementCategory/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, RequirementCategory requirementCategory)
        {
            var requirementCategoryToEdit = _requirementCategoryRepository.GetNullableById(id);

            if (requirementCategoryToEdit == null) return this.RedirectToAction<ProjectController>(a => a.Index());

            requirementCategoryToEdit.Name = requirementCategory.Name;
            
            if (ModelState.IsValid)
            {
                _requirementCategoryRepository.EnsurePersistent(requirementCategoryToEdit);

                Message = "RequirementCategory Edited Successfully";

                return RedirectToAction("Index", requirementCategory.Project.Id);
            }
			
            var viewModel = RequirementCategoryViewModel.Create(Repository, requirementCategoryToEdit.Project);
            viewModel.RequirementCategory = requirementCategory;

            return View(viewModel);
        }
        
        //
        // GET: /RequirementCategory/Delete/5 
        public ActionResult Delete(int id)
        {
			var requirementCategory = _requirementCategoryRepository.GetNullableById(id);

            if (requirementCategory == null) return this.RedirectToAction<ProjectController>(a => a.Index());

            return View(requirementCategory);
        }

        //
        // POST: /RequirementCategory/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, RequirementCategory requirementCategory)
        {
			var requirementCategoryToDelete = _requirementCategoryRepository.GetNullableById(id);

            if (requirementCategoryToDelete == null) return this.RedirectToAction<ProjectController>(a => a.Index());

            requirementCategoryToDelete.IsActive = false;
            _requirementCategoryRepository.EnsurePersistent(requirementCategoryToDelete);

            Message = "RequirementCategory Removed Successfully";

            return this.RedirectToAction(a => a.Index(requirementCategoryToDelete.Project.Id));
        }
    }
}
