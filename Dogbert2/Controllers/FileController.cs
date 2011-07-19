using System.Linq;
using System.Security;
using System.Web.Mvc;
using Dogbert2.App_GlobalResources;
using Dogbert2.Core.Domain;
using Dogbert2.Filters;
using Dogbert2.Models;
using Dogbert2.Services;
using UCDArch.Core.PersistanceSupport;
using MvcContrib;
using UCDArch.Web.Helpers;
using File = Dogbert2.Core.Domain.File;

namespace Dogbert2.Controllers
{
    /// <summary>
    /// Controller for the File class
    /// </summary>
    [AllRoles]
    public class FileController : ApplicationController
    {
	    private readonly IRepository<File> _fileRepository;
        private readonly IRepository<Project> _projectRepository;
        private readonly IAccessValidatorService _accessValidator;

        public FileController(IRepository<File> fileRepository, IRepository<Project> projectRepository, IAccessValidatorService accessValidator)
        {
            _fileRepository = fileRepository;
            _projectRepository = projectRepository;
            _accessValidator = accessValidator;
        }

        /// <summary>
        /// Project files
        /// </summary>
        /// <param name="id">Project Id</param>
        /// <returns></returns>
        public ActionResult Index(int id)
        {
            //var fileList = _fileRepository.Queryable;

            var project = _projectRepository.GetNullableById(id);

            if (project == null)
            {
                Message = string.Format(Messages.NotFound, "project", id);
                return this.RedirectToAction<ProjectController>(a => a.Index());
            }

            // validate access
            var redirect = _accessValidator.CheckReadAccess(CurrentUser.Identity.Name, project);
            if (redirect != null) return redirect;

            ViewBag.ProjectId = id;

            return View(project.Files);
        }

        //
        // GET: /File/Create
        public ActionResult Create(int id)
        {
            var project = _projectRepository.GetNullableById(id);

            if (project == null)
            {
                Message = string.Format(Messages.NotFound, "project", id);
                return this.RedirectToAction<ProjectController>(a => a.Index());
            }

            // validate access
            var redirect = _accessValidator.CheckEditAccess(CurrentUser.Identity.Name, project);
            if (redirect != null)
            {
                Message = "Not authorized to edit project.";
                return redirect;
            }

			var viewModel = FileViewModel.Create(Repository, project);
            viewModel.Files.Add(new File());
            return View(viewModel);
        } 

        //
        // POST: /File/Create
        [HttpPost]
        public ActionResult Create(int id, FilePostModel[] files)
        {
            var project = _projectRepository.GetNullableById(id);

            if (project == null)
            {
                Message = string.Format(Messages.NotFound, "project", id);
                return this.RedirectToAction<ProjectController>(a => a.Index());
            }

            // validate access
            var redirect = _accessValidator.CheckEditAccess(CurrentUser.Identity.Name, project);
            if (redirect != null)
            {
                Message = "Not authorized to edit project.";
                return redirect;
            }

            for (var i = 0; i < files.Count(); i++)
            {
                var postedFile = files[i];

                if (postedFile.File != null && postedFile.File.ContentLength > 0)
                {
                    var newFile = new File();
                    AutoMapper.Mapper.Map(postedFile, newFile);
                    newFile.Project = project;

                    ModelState.Clear();
                    newFile.TransferValidationMessagesTo(ModelState);

                    if (ModelState.IsValid)
                    {
                        _fileRepository.EnsurePersistent(newFile);
                    }
                }
            }

            return this.RedirectToAction(a => a.Index(id));
        }

        //
        // GET: /File/Edit/5
        public ActionResult Edit(int id)
        {
            var file = _fileRepository.GetNullableById(id);

            if (file == null) return this.RedirectToAction<ProjectController>(a=>a.Index());

            // validate access
            var redirect = _accessValidator.CheckEditAccess(CurrentUser.Identity.Name, file.Project);
            if (redirect != null)
            {
                Message = "Not authorized to edit project.";
                return redirect;
            }

			return View(file);
        }
        
        //
        // POST: /File/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FilePostModel file)
        {
            var fileToEdit = _fileRepository.GetNullableById(id);

            if (fileToEdit == null) return this.RedirectToAction<ProjectController>(a => a.Index());

            // validate access
            var redirect = _accessValidator.CheckEditAccess(CurrentUser.Identity.Name, fileToEdit.Project);
            if (redirect != null)
            {
                Message = "Not authorized to edit project.";
                return redirect;
            }

            // no file update, preserve old information
            if (file.File == null)
            {
                // save the old values
                var contents = fileToEdit.Contents;
                var type = fileToEdit.ContentType;
                var name = fileToEdit.FileName;

                AutoMapper.Mapper.Map(file, fileToEdit);

                // copy back in the values
                fileToEdit.Contents = contents;
                fileToEdit.ContentType = type;
                fileToEdit.FileName = name;
            }
            // file was uploaded, perform standard transfer
            else
            {
                AutoMapper.Mapper.Map(file, fileToEdit);    
            }

            

            if (ModelState.IsValid)
            {
                _fileRepository.EnsurePersistent(fileToEdit);

                Message = "File Edited Successfully";

                return this.RedirectToAction(a => a.Index(fileToEdit.Project.Id));
            }

            return View(fileToEdit);
        }
        
        //
        // GET: /File/Delete/5 
        public ActionResult Delete(int id)
        {
			var file = _fileRepository.GetNullableById(id);

            if (file == null) return this.RedirectToAction<ProjectController>(a=>a.Index());

            // validate access
            var redirect = _accessValidator.CheckEditAccess(CurrentUser.Identity.Name, file.Project);
            if (redirect != null)
            {
                Message = "Not authorized to edit project.";
                return redirect;
            }

            return View(file);
        }

        //
        // POST: /File/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, File file)
        {
			var fileToDelete = _fileRepository.GetNullableById(id);

            if (fileToDelete == null) return this.RedirectToAction<ProjectController>(a => a.Index());

            // validate access
            var redirect = _accessValidator.CheckEditAccess(CurrentUser.Identity.Name, fileToDelete.Project);
            if (redirect != null)
            {
                Message = "Not authorized to edit project.";
                return redirect;
            }

            var projectId = fileToDelete.Project.Id;

            _fileRepository.Remove(fileToDelete);

            Message = "File Removed Successfully";

            return this.RedirectToAction(a=>a.Index(projectId));
        }

        public FileResult Download(int id)
        {
            var file = _fileRepository.GetNullableById(id);

            // validate access
            var redirect = _accessValidator.CheckReadAccess(CurrentUser.Identity.Name, file.Project);
            if (redirect != null) throw new SecurityException("Not authorized to download file");

            return File(file.Contents, file.ContentType, file.FileName);
        }

    }


}
