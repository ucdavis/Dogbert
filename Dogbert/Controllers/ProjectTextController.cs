using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Dogbert.Controllers.Helpers;
using Dogbert.Core.Domain;
using Dogbert.Core.Resources;
using MvcContrib.Attributes;
using UCDArch.Core.PersistanceSupport;
using UCDArch.Core.Utils;
using UCDArch.Web.Controller;
using UCDArch.Web.Helpers;
using MvcContrib;

namespace Dogbert.Controllers
{
    public class ProjectTextController : SuperController
    {
        public ActionResult Create(int id)
        {
            var project = Repository.OfType<Project>().GetNullableByID(id);

            if (project == null)
            {
                return this.RedirectToAction<ProjectController>(a => a.DynamicIndex());
            }

            var viewmodel = CreateProjectTextViewModel.Create(Repository, project, true);

            return View(viewmodel);
        }

        [ValidateInput(false)]
        [AcceptPost]
        public ActionResult Create(int id, [Bind(Exclude="Id")]ProjectText projectText)
        {
            var project = Repository.OfType<Project>().GetNullableByID(id);

            if (project == null)
            {
                return this.RedirectToAction<ProjectController>(a=>a.DynamicIndex());
            }

            var newProjectText = new ProjectText();
            newProjectText.Text = projectText.Text;
            newProjectText.TextType = projectText.TextType;

            if (project.ProjectTexts.Any(a => a.TextType == newProjectText.TextType))
            {
                ModelState.AddModelError("Text Type", "Text type already exists in this project");
            }
            else if (newProjectText.Text.Length < 1)
            {
                ModelState.AddModelError("Text Type", "No text entered");
            }

            project.AddProjectTexts(newProjectText);

            newProjectText.TransferValidationMessagesTo(ModelState);

            if (ModelState.IsValid)
            {
                Repository.OfType<Project>().EnsurePersistent(project);
                Message = "New Text Created Successfully";
                return Redirect(Url.EditProjectUrl(id, StaticValues.Tab_ProjectText));
            }

            var viewModel = CreateProjectTextViewModel.Create(Repository, project, true);
            return View(viewModel);
            
        }

        public ActionResult Edit(int id)
        {
            var existingProjectText = Repository.OfType<ProjectText>().GetNullableByID(id);

            if (existingProjectText == null) return RedirectToAction("Create");//?Need to redirect to edit screen, but don't have projId.

            var viewModel = CreateProjectTextViewModel.Create(Repository, existingProjectText.Project, false);
            viewModel.ProjectText = existingProjectText;
            return View(viewModel);
        }

        [AcceptPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(int id, ProjectText projectText)
        {
            var pt = Repository.OfType<ProjectText>().GetNullableByID(id);

            Check.Require(pt != null, "Project Text not found.");

            TransferValuesTo(pt, projectText);

            pt.TransferValidationMessagesTo(ModelState);

            var proj = Repository.OfType<Project>().GetNullableByID(pt.Project.Id);

            if (proj.ProjectTexts.Any(a => a.TextType == pt.TextType && a.Id != pt.Id))
            {//Ensure project text does not already exist for record other than self

                ////NEED to update.  TextType can be the same if it's on the ProjectText being updated.
                ModelState.AddModelError("Text Type", "Text type already exists in this project");

            }
            else if (pt.Text.Length < 1)
            {
                ModelState.AddModelError("Text Type", "No text entered");
            }


            if (ModelState.IsValid)
            {
                Repository.OfType<ProjectText>().EnsurePersistent(pt);
                Message = "Project text edited successfully";
                return Redirect(Url.EditProjectUrl(id, StaticValues.Tab_ProjectText));
            }

            var viewModel = CreateProjectTextViewModel.Create(Repository, pt.Project, false);
            viewModel.ProjectText = pt;
            return View(viewModel);
        }

        private static void TransferValuesTo(ProjectText projectTextToUpdate, ProjectText projectText)
        {
            projectTextToUpdate.TextType = projectText.TextType;
            projectTextToUpdate.Text = projectText.Text;
        }
    }

    public class CreateProjectTextViewModel
    {
        public IEnumerable<TextType> TextType { get; set; }
        public ProjectText ProjectText { get; set; }
        public Project Project { get; set; }

        public static CreateProjectTextViewModel Create(IRepository repository, Project project, bool create)
        {
            Check.Require(repository != null, "Repository is required.");

            var viewModel = new CreateProjectTextViewModel()
                                {
                                    //TextType = repository.OfType<TextType>().Queryable.Where(a => a.IsActive).OrderBy(a => a.Priority).ToList(),
                                    Project = project
                                };

            if (create)
            {
                var usedTypes = project.ProjectTexts.Select(a => a.TextType).ToList();
                var allTypes = repository.OfType<TextType>().Queryable.Where(a => a.IsActive).ToList();

                viewModel.TextType = allTypes.Where(a => !usedTypes.Contains(a)).ToList();
            }
            else
            {
                viewModel.TextType = repository.OfType<TextType>().Queryable.Where(a => a.IsActive).ToList();

            }

            return viewModel;
        }

    }
}
