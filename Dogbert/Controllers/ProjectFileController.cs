using System.IO;
using System.Web;
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
    [Authorize(Roles = "User")]
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
        /// </summary>
        /// <param name="projectFile">The project file.</param>
        /// <param name="projectId">The project id.</param>
        /// <param name="fileUpload">The file upload.</param>
        /// <returns></returns>
        [AcceptPost]
        public ActionResult Create(ProjectFile projectFile, int projectId, HttpPostedFileBase fileUpload)
        {
            var project = Repository.OfType<Project>().GetNullableByID(projectId);

            if (project == null)
            {
                Message = string.Format(NotificationMessages.STR_ObjectNotFound, "Project");
                return this.RedirectToAction<ProjectController>(a => a.Index());
            }
            
            LoadFileContents(projectFile, fileUpload);
           
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
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="projectFile">The project file.</param>
        /// <param name="fileUpload">The file upload.</param>
        /// <returns></returns>
        [AcceptPost]
        public ActionResult Edit(int id, ProjectFile projectFile, HttpPostedFileBase fileUpload)
        {
            var dest = Repository.OfType<ProjectFile>().GetNullableByID(id);

            if (dest == null)
            {
                Message = string.Format(NotificationMessages.STR_ObjectNotFound, "ProjectFile");
                return this.RedirectToAction<ProjectController>(a => a.Index());
            }
            Copiers.CopyProjectFile(projectFile, dest);
            LoadFileContents(dest, fileUpload);
            
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

            Repository.OfType<ProjectFile>().Remove(projectFileToRemove);
            Message = string.Format(NotificationMessages.STR_ObjectRemoved, "ProjectFile");
            return Redirect(Url.EditProjectUrl(saveProjectId, StaticValues.Tab_ProjectFiles));
            
        }

        public ActionResult ViewFile(int id)
        {
            var projectFile = Repository.OfType<ProjectFile>().GetNullableByID(id);
            Check.Require(projectFile != null, "Invalid ProjectFile identifier");

            // ReSharper disable PossibleNullReferenceException
            return File(projectFile.FileContents, projectFile.FileContentType, projectFile.FileName);
            // ReSharper restore PossibleNullReferenceException
        }

/*
        /// <summary>
        /// Loads the file contents.
        /// I'm just keeping this here for now as an example of another way to get the upload file.
        /// </summary>
        /// <param name="projectFile">The project file.</param>
        [System.Obsolete("The Other LoadFileContents is better.")]
        private void LoadFileContents(ProjectFile projectFile)
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
*/

        /// <summary>
        /// Loads the file contents.
        /// </summary>
        /// <param name="projectFile">The project file.</param>
        /// <param name="fileUpload">The file upload.</param>
        private static void LoadFileContents(ProjectFile projectFile, HttpPostedFileBase fileUpload)
        {
            if (fileUpload != null && fileUpload.ContentLength != 0)
            {
                var reader = new BinaryReader(fileUpload.InputStream);
                projectFile.FileContents = reader.ReadBytes(fileUpload.ContentLength);
                projectFile.FileName = fileUpload.FileName;
                projectFile.FileContentType = fileUpload.ContentType;
            }           
        }
    }
}
