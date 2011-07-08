using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dogbert2.App_GlobalResources;
using Dogbert2.Core.Domain;
using Dogbert2.Models;
using UCDArch.Core.PersistanceSupport;
using MvcContrib;
using UCDArch.Web.Helpers;
using File = Dogbert2.Core.Domain.File;

namespace Dogbert2.Controllers
{
    /// <summary>
    /// Controller for the File class
    /// </summary>
    public class FileController : ApplicationController
    {
	    private readonly IRepository<File> _fileRepository;
        private readonly IRepository<Project> _projectRepository;

        public FileController(IRepository<File> fileRepository, IRepository<Project> projectRepository)
        {
            _fileRepository = fileRepository;
            _projectRepository = projectRepository;
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

            ViewBag.ProjectId = id;

            return View(project.Files);
        }


        //
        // GET: /File/Details/5
        public ActionResult Details(int id)
        {
            var file = _fileRepository.GetNullableById(id);

            if (file == null) return RedirectToAction("Index");

            return View(file);
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

            if (file == null) return RedirectToAction("Index");

			var viewModel = FileViewModel.Create(Repository, file.Project);
			//viewModel.File = file;

			return View(viewModel);
        }
        
        //
        // POST: /File/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, File file)
        {
            var fileToEdit = _fileRepository.GetNullableById(id);

            if (fileToEdit == null) return RedirectToAction("Index");

            TransferValues(file, fileToEdit);

            if (ModelState.IsValid)
            {
                _fileRepository.EnsurePersistent(fileToEdit);

                Message = "File Edited Successfully";

                return RedirectToAction("Index");
            }
            else
            {
				var viewModel = FileViewModel.Create(Repository, file.Project);
                //viewModel.File = file;

                return View(viewModel);
            }
        }
        
        //
        // GET: /File/Delete/5 
        public ActionResult Delete(int id)
        {
			var file = _fileRepository.GetNullableById(id);

            if (file == null) return RedirectToAction("Index");

            return View(file);
        }

        //
        // POST: /File/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, File file)
        {
			var fileToDelete = _fileRepository.GetNullableById(id);

            if (fileToDelete == null) return RedirectToAction("Index");

            _fileRepository.Remove(fileToDelete);

            Message = "File Removed Successfully";

            return RedirectToAction("Index");
        }
        
        /// <summary>
        /// Transfer editable values from source to destination
        /// </summary>
        private static void TransferValues(File source, File destination)
        {
			//Recommendation: Use AutoMapper
			//Mapper.Map(source, destination)
            throw new NotImplementedException();
        }


    }


}
