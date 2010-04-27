using System.IO;
using System.Web.Mvc;
using Dogbert.Controllers.Helpers;
using Dogbert.Controllers.ViewModels;
using Dogbert.Core.Domain;
using Dogbert.Core.Resources;
using MvcContrib;
using MvcContrib.Attributes;
using UCDArch.Core.Utils;
using UCDArch.Web.Controller;
using UCDArch.Web.Helpers;

namespace Dogbert.Controllers
{
    public class ProjectFileController : SuperController
    {
        //
        // GET: /ProjectFile/

        public ActionResult Index()
        {
            return this.RedirectToAction<ProjectController>(a => a.Index());
        }

        //
        // GET: /ProjectFile/Create

        public ActionResult Create(int projectId)
        {
            var project = Repository.OfType<Project>().GetNullableByID(projectId);

            if (project == null)
            {
                Message = string.Format(NotificationMessages.STR_ObjectNotFound, "Project");
                return this.RedirectToAction<ProjectController>(a => a.Index());
            }

            var viewModel = ProjectFileViewModel.Create(Repository, project);

            return View(viewModel);
        } 

        /// <summary>
        /// Creates the specified project file.
        /// POST: /ProjectFile/Create
        /// If no file with content is found, the domain validation will prevent the create.
        /// If more than one file with content is found (maybe not possible) a 
        /// manual validation error will be added
        /// </summary>
        /// <param name="projectFile">The project file.</param>
        /// <param name="projectId">The project id.</param>
        /// <returns></returns>
        [AcceptPost]
        public ActionResult Create(ProjectFile projectFile, int projectId)
        {
            var project = Repository.OfType<Project>().GetNullableByID(projectId);

            if (project == null)
            {
                Message = string.Format(NotificationMessages.STR_ObjectNotFound, "Project");
                return this.RedirectToAction<ProjectController>(a => a.Index());
            }
            LoadFileContents(projectFile);
            //var foundFile = false;
            //for (int i = 0; i < Request.Files.Count; i++)
            //{
            //    if (Request.Files[i].ContentLength > 0)
            //    {
            //        if (foundFile)
            //        {
            //            ModelState.AddModelError("FileName", "More than 1 upload file was detected.");
            //            break;
            //        }
            //        foundFile = true;
            //        var file = Request.Files[i];
            //        var reader = new BinaryReader(file.InputStream);
            //        projectFile.FileContents = reader.ReadBytes(file.ContentLength);
            //        projectFile.FileName = file.FileName;
            //        projectFile.FileContentType = file.ContentType;
            //    }
            //}

            projectFile.Project = project;

            projectFile.TransferValidationMessagesTo(ModelState);
            if (ModelState.IsValid)
            {
                Repository.OfType<ProjectFile>().EnsurePersistent(projectFile);
                Message = string.Format(NotificationMessages.STR_ObjectCreated, "ProjectFile");
                return Redirect(Url.EditProjectUrl(projectId, StaticValues.Tab_ProjectFiles));
            }

            var viewModel = ProjectFileViewModel.Create(Repository, project);
            viewModel.ProjectFile = projectFile;
            return View(viewModel);
        }

        /// <summary>
        /// Edits the specified id.
        /// GET: /ProjectFile/Edit/5
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            var projectFile = Repository.OfType<ProjectFile>().GetNullableByID(id);

            if (projectFile == null)
            {
                Message = string.Format(NotificationMessages.STR_ObjectNotFound, "ProjectFile");
                return this.RedirectToAction<ProjectController>(a => a.Index());
            }
            var projectFileViewModel = ProjectFileViewModel.Create(Repository, projectFile.Project);
            projectFileViewModel.ProjectFile = projectFile;


            return View(projectFileViewModel);
        }

        /// <summary>
        /// Edits the specified id.
        /// POST: /ProjectFile/Edit/
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="projectFile">The project file.</param>
        /// <returns></returns>
        [AcceptPost]
        public ActionResult Edit(int id, ProjectFile projectFile)
        {
            var dest = Repository.OfType<ProjectFile>().GetNullableByID(id);

            if (dest == null)
            {
                Message = string.Format(NotificationMessages.STR_ObjectNotFound, "ProjectFile");
                return this.RedirectToAction<ProjectController>(a => a.Index());
            }
            Copiers.CopyProjectFile(projectFile, dest);
            LoadFileContents(dest);
            //var foundFile = false;
            //for (int i = 0; i < Request.Files.Count; i++)
            //{
            //    if (Request.Files[i].ContentLength > 0)
            //    {
            //        if (foundFile)
            //        {
            //            ModelState.AddModelError("FileName", "More than 1 upload file was detected.");
            //            break;
            //        }
            //        foundFile = true;
            //        var file = Request.Files[i];
            //        var reader = new BinaryReader(file.InputStream);
            //        dest.FileContents = reader.ReadBytes(file.ContentLength);
            //        dest.FileName = file.FileName;
            //        dest.FileContentType = file.ContentType;
            //    }
            //}

            dest.TransferValidationMessagesTo(ModelState);

            if (ModelState.IsValid)
            {
                Repository.OfType<ProjectFile>().EnsurePersistent(dest);
                Message = string.Format(NotificationMessages.STR_ObjectUpdated, "ProjectFile");
                return Redirect(Url.EditProjectUrl(dest.Project.Id, StaticValues.Tab_ProjectFiles));
            }

            var viewModel = ProjectFileViewModel.Create(Repository, dest.Project);
            viewModel.ProjectFile = projectFile;

            return View(viewModel);
        }

        /// <summary>
        /// Gets the specified id to remove.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public ActionResult Remove(int id)
        {
            var projectFile = Repository.OfType<ProjectFile>().GetNullableByID(id);

            if (projectFile == null)
            {
                Message = string.Format(NotificationMessages.STR_ObjectNotFound, "ProjectFile");
                return this.RedirectToAction<ProjectController>(a => a.Index());
            }
            var projectFileViewModel = ProjectFileViewModel.Create(Repository, projectFile.Project);
            projectFileViewModel.ProjectFile = projectFile;

            return View(projectFileViewModel);
        }

        /// <summary>
        /// Removes the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="projectFile">The project file.</param>
        /// <returns></returns>
        [AcceptPost]
        public ActionResult Remove(int id, ProjectFile projectFile)
        {
            var projectFileToRemove = Repository.OfType<ProjectFile>().GetNullableByID(id);

            if (projectFileToRemove == null)
            {
                Message = string.Format(NotificationMessages.STR_ObjectNotFound, "ProjectFile");
                return this.RedirectToAction<ProjectController>(a => a.Index());
            }

            var saveProjectId = projectFileToRemove.Project.Id;

            projectFileToRemove.TransferValidationMessagesTo(ModelState);

            if (ModelState.IsValid)
            {
                Repository.OfType<ProjectFile>().Remove(projectFileToRemove);
                Message = string.Format(NotificationMessages.STR_ObjectRemoved, "ProjectFile");
                return Redirect(Url.EditProjectUrl(saveProjectId, StaticValues.Tab_ProjectFiles));
            }

            var viewModel = ProjectFileViewModel.Create(Repository, projectFileToRemove.Project);
            viewModel.ProjectFile = projectFile;

            return View(viewModel);
        }

        public ActionResult ViewFile(int id)
        {
            var projectFile = Repository.OfType<ProjectFile>().GetNullableByID(id);
            Check.Require(projectFile != null, "Invalid ProjectFile identifier");

            // ReSharper disable PossibleNullReferenceException
            return File(projectFile.FileContents, projectFile.FileContentType, projectFile.FileName);
            // ReSharper restore PossibleNullReferenceException
        }

        /// <summary>
        /// Loads the file contents.
        /// </summary>
        /// <param name="projectFile">The project file.</param>
        public virtual void LoadFileContents(ProjectFile projectFile)
        {
            var foundFile = false;
            for (int i = 0; i < Request.Files.Count; i++)
            {
                if (Request.Files[i].ContentLength > 0)
                {
                    if (foundFile)
                    {
                        ModelState.AddModelError("FileName", "More than 1 upload file was detected.");
                        break;
                    }
                    foundFile = true;
                    var file = Request.Files[i];
                    var reader = new BinaryReader(file.InputStream);
                    projectFile.FileContents = reader.ReadBytes(file.ContentLength);
                    projectFile.FileName = file.FileName;
                    projectFile.FileContentType = file.ContentType;
                }
            }
        }
    }
}
