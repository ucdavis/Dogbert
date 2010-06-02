using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Dogbert.Core.Domain;
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

            var viewmodel = CreateProjectTextViewModel.Create(Repository, project);

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

            if (project.ProjectTexts.Any(a => a.TextType == projectText.TextType))
            {
                ModelState.AddModelError("Text Type", "Text type already exists in this project");
            }
            else if (projectText.Text.Length < 1)
            {
                ModelState.AddModelError("Text Type", "No text entered");
            }

            project.AddProjectTexts(projectText);

            projectText.TransferValidationMessagesTo(ModelState);

            if (ModelState.IsValid)
            {
                Repository.OfType<ProjectText>().EnsurePersistent(projectText);
                Message = "New Text Created Successfully";
            }

            var viewModel = CreateProjectTextViewModel.Create(Repository, project);
            return this.RedirectToAction(a => a.Edit(id));
        }

        public ActionResult Edit(int id)
        {
            var existingProjectText = Repository.OfType<ProjectText>().GetNullableByID(id);

            if (existingProjectText == null) return RedirectToAction("Create");//?Need to redirect to edit screen, but don't have projId.

            var viewModel = CreateProjectTextViewModel.Create(Repository, existingProjectText.Project);
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
                return this.RedirectToAction<ProjectController>(a => a.Edit(pt.Project.Id));
            }

            var viewModel = CreateProjectTextViewModel.Create(Repository, pt.Project);
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

        public static CreateProjectTextViewModel Create(IRepository repository, Project project)
        {
            Check.Require(repository != null, "Repository is required.");

            var viewModel = new CreateProjectTextViewModel()
                                {
                                    TextType = repository.OfType<TextType>().Queryable.Where(a => a.IsActive).OrderBy(a => a.Priority).ToList(),
                                    Project = project
                                };

            return viewModel;
        }

    }
}
