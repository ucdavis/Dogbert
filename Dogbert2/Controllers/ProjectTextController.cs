using System;
using System.Linq;
using System.Web.Mvc;
using Dogbert2.App_GlobalResources;
using Dogbert2.Core.Domain;
using Dogbert2.Filters;
using Dogbert2.Models;
using Dogbert2.Services;
using UCDArch.Core.PersistanceSupport;
using MvcContrib;
using UCDArch.Web.Helpers;

namespace Dogbert2.Controllers
{
    /// <summary>
    /// Controller for the ProjectText class
    /// </summary>
    [AllRoles]
    public class ProjectTextController : ApplicationController
    {
        private readonly IRepository<Project> _projectRepository;
        private readonly IRepository<ProjectText> _projectTextRepository;
        private readonly IRepositoryWithTypedId<TextType, string> _textTypeRepository;
        private readonly IAccessValidatorService _accessValidator;

        public ProjectTextController(IRepository<Project> projectRepository, IRepository<ProjectText> projectTextRepository, IRepositoryWithTypedId<TextType, string> textTypeRepository, IAccessValidatorService accessValidator)
        {
            _projectRepository = projectRepository;
            _projectTextRepository = projectTextRepository;
            _textTypeRepository = textTypeRepository;
            _accessValidator = accessValidator;
        }

        /// <summary>
        /// GET: /ProjectText/ 
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

            ViewBag.ProjectId = project.Id;

            return View(project.ProjectTexts.OrderBy(a=>a.TextType.Order).ToList());
        }


        //
        // GET: /ProjectText/Details/5
        public ActionResult Details(int id)
        {
            throw new NotImplementedException();

            var projectText = _projectTextRepository.GetNullableById(id);

            if (projectText == null) return this.RedirectToAction<HomeController>(a => a.Index());

            return View(projectText);
        }

        //
        // GET: /ProjectText/Create
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

            ViewBag.ProjectId = project.Id;

			var viewModel = ProjectTextViewModel.Create(Repository, project);
            return View(viewModel);
        } 

        //
        // POST: /ProjectText/Create
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(int id, ProjectText projectText)
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

            projectText.Project = project;

            ModelState.Clear();
            projectText.TransferValidationMessagesTo(ModelState);

            // make sure we're not adding a project text that already exists for this project
            if (_projectTextRepository.Queryable.Where(a => a.Project == project && a.TextType == projectText.TextType).Any())
            {
                ModelState.AddModelError("", "Project Text of this type already exists for this project.");
            }

            if (ModelState.IsValid)
            {
                _projectTextRepository.EnsurePersistent(projectText);

                Message = "Project Text Created Successfully";

                return this.RedirectToAction(a => a.Index(projectText.Project.Id));
            }

            var viewModel = ProjectTextViewModel.Create(Repository, projectText.Project);
            viewModel.ProjectText = projectText;

            return View(viewModel);
        }

        //
        // GET: /ProjectText/Edit/5
        public ActionResult Edit(int id)
        {
            var projectText = _projectTextRepository.GetNullableById(id);

            if (projectText == null) return this.RedirectToAction<ProjectController>(a => a.Index());

            // validate access
            var redirect = _accessValidator.CheckEditAccess(CurrentUser.Identity.Name, projectText.Project);
            if (redirect != null)
            {
                Message = "Not authorized to edit project.";
                return redirect;
            }

			var viewModel = ProjectTextViewModel.Create(Repository, projectText.Project);
			viewModel.ProjectText = projectText;

			return View(viewModel);
        }
        
        //
        // POST: /ProjectText/Edit/5
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(int id, ProjectText projectText)
        {
            var projectTextToEdit = _projectTextRepository.GetNullableById(id);

            if (projectTextToEdit == null) return this.RedirectToAction<ProjectController>(a => a.Index());

            // validate access
            var redirect = _accessValidator.CheckEditAccess(CurrentUser.Identity.Name, projectTextToEdit.Project);
            if (redirect != null)
            {
                Message = "Not authorized to edit project.";
                return redirect;
            }

            AutoMapper.Mapper.Map(projectText, projectTextToEdit);

            ModelState.Clear();
            projectTextToEdit.TransferValidationMessagesTo(ModelState);

            if (ModelState.IsValid)
            {
                _projectTextRepository.EnsurePersistent(projectTextToEdit);

                Message = "ProjectText Edited Successfully";

                return this.RedirectToAction(a=>a.Index(projectTextToEdit.Project.Id));
            }

            var viewModel = ProjectTextViewModel.Create(Repository, projectTextToEdit.Project);
            viewModel.ProjectText = projectTextToEdit;
            return View(viewModel);
        }
        
        //
        // GET: /ProjectText/Delete/5 
        public ActionResult Delete(int id)
        {
			var projectText = _projectTextRepository.GetNullableById(id);

            if (projectText == null) return this.RedirectToAction<ProjectController>(a=>a.Index());

            // validate access
            var redirect = _accessValidator.CheckEditAccess(CurrentUser.Identity.Name, projectText.Project);
            if (redirect != null)
            {
                Message = "Not authorized to edit project.";
                return redirect;
            }

            return View(projectText);
        }

        //
        // POST: /ProjectText/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, ProjectText projectText)
        {
			var projectTextToDelete = _projectTextRepository.GetNullableById(id);

            if (projectTextToDelete == null) return this.RedirectToAction<HomeController>(a => a.Index());

            // validate access
            var redirect = _accessValidator.CheckEditAccess(CurrentUser.Identity.Name, projectTextToDelete.Project);
            if (redirect != null)
            {
                Message = "Not authorized to edit project.";
                return redirect;
            }

            var projectId = projectTextToDelete.Project.Id;
            
            _projectTextRepository.Remove(projectTextToDelete);

            Message = "ProjectText Removed Successfully";

            return this.RedirectToAction(a => a.Index(projectId));
        }
       
    }
}
