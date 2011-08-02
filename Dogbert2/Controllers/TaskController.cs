using System;
using System.Linq;
using System.Web.Mvc;
using Dogbert2.App_GlobalResources;
using Dogbert2.Core.Domain;
using Dogbert2.Models;
using Dogbert2.Services;
using UCDArch.Core.PersistanceSupport;
using MvcContrib;
using UCDArch.Web.Helpers;

namespace Dogbert2.Controllers
{
    /// <summary>
    /// Controller for the Task class
    /// </summary>
    public class TaskController : ApplicationController
    {
	    private readonly IRepository<Task> _taskRepository;
        private readonly IRepository<Project> _projectRepository;
        private readonly IAccessValidatorService _accessValidator;

        public TaskController(IRepository<Task> taskRepository, IRepository<Project> projectRepository, IAccessValidatorService accessValidator)
        {
            _taskRepository = taskRepository;
            _projectRepository = projectRepository;
            _accessValidator = accessValidator;
        }

        /// <summary>
        /// View a list of tasks assigned to user
        /// </summary>
        /// <returns></returns>
        public ActionResult MyTasks()
        {
            return View();
        }

        /// <summary>
        /// View list of tasks for project
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public ActionResult Index(int projectId)
        {
            var project = _projectRepository.GetNullableById(projectId);

            if (project == null)
            {
                Message = string.Format(Messages.NotFound, "Project", projectId);
                return this.RedirectToAction<ProjectController>(a => a.Index());
            }

            // validate access
            var redirect = _accessValidator.CheckEditAccess(CurrentUser.Identity.Name, project);
            if (redirect != null)
            {
                Message = "Not authorized to edit project.";
                return redirect;
            }

            ViewBag.ProjectId = projectId;

            var tasks = _taskRepository.Queryable.Where(a => a.Project == project);

            return View(tasks.ToList());
        }

        //
        // GET: /Task/Details/5
        public ActionResult Details(int id)
        {
            var task = _taskRepository.GetNullableById(id);

            if (task == null) return RedirectToAction("Index");

            // validate access
            var redirect = _accessValidator.CheckEditAccess(CurrentUser.Identity.Name, task.Project);
            if (redirect != null)
            {
                Message = "Not authorized to edit project.";
                return redirect;
            }

            return View(task);
        }

        //
        // GET: /Task/Create
        public ActionResult Create(int projectId)
        {
            var project = _projectRepository.GetNullableById(projectId);

            if (project == null)
            {
                Message = string.Format(Messages.NotFound, "Project", projectId);
                return this.RedirectToAction<ProjectController>(a => a.Index());
            }

            // validate access
            var redirect = _accessValidator.CheckEditAccess(CurrentUser.Identity.Name, project);
            if (redirect != null)
            {
                Message = "Not authorized to edit project.";
                return redirect;
            }

			var viewModel = TaskViewModel.Create(Repository, project);
            
            return View(viewModel);
        } 

        //
        // POST: /Task/Create
        [HttpPost]
        public ActionResult Create(int projectId, Task task)
        {
            var project = _projectRepository.GetNullableById(projectId);

            if (project == null)
            {
                Message = string.Format(Messages.NotFound, "Project", projectId);
                return this.RedirectToAction<ProjectController>(a => a.Index());
            }

            // validate access
            var redirect = _accessValidator.CheckEditAccess(CurrentUser.Identity.Name, project);
            if (redirect != null)
            {
                Message = "Not authorized to edit project.";
                return redirect;
            }

            var taskToCreate = new Task();

            AutoMapper.Mapper.Map(task, taskToCreate);
            taskToCreate.Project = project;
            taskToCreate.Requirement = task.Requirement;

            if (project.Tasks.Count > 0)
            {
                // set the task id
                // get the highest task id
                var maxId = project.Tasks.Select(a => a.TaskId).Max();
                // parse the int
                var mid = maxId.Substring(1, maxId.Length - 1);
                taskToCreate.TaskId = string.Format("T{0}", Convert.ToInt32(mid) + 1);    
            }
            else
            {
                taskToCreate.TaskId = "T1";
            }
            

            ModelState.Clear();
            taskToCreate.TransferValidationMessagesTo(ModelState);

            if (ModelState.IsValid)
            {
                _taskRepository.EnsurePersistent(taskToCreate);

                Message = "Task Created Successfully";

                return this.RedirectToAction(a => a.Index(projectId));
            }

			var viewModel = TaskViewModel.Create(Repository, project);
            viewModel.Task = taskToCreate;

            return View(viewModel);
        }

        //
        // GET: /Task/Edit/5
        public ActionResult Edit(int id)
        {
            var task = _taskRepository.GetNullableById(id);

            if (task == null) return RedirectToAction("Index", "Project");

            var project = task.Project;

            // validate access
            var redirect = _accessValidator.CheckEditAccess(CurrentUser.Identity.Name, project);
            if (redirect != null)
            {
                Message = "Not authorized to edit project.";
                return redirect;
            }

			var viewModel = TaskViewModel.Create(Repository, project);
			viewModel.Task = task;

			return View(viewModel);
        }
        
        //
        // POST: /Task/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Task task)
        {
            var taskToEdit = _taskRepository.GetNullableById(id);

            if (taskToEdit == null) return RedirectToAction("Index", "Project");

            var project = taskToEdit.Project;

            // validate access
            var redirect = _accessValidator.CheckEditAccess(CurrentUser.Identity.Name, project);
            if (redirect != null)
            {
                Message = "Not authorized to edit project.";
                return redirect;
            }

            AutoMapper.Mapper.Map(task, taskToEdit);
            taskToEdit.Requirement = task.Requirement;

            ModelState.Clear();
            taskToEdit.TransferValidationMessagesTo(ModelState);

            if (ModelState.IsValid)
            {
                _taskRepository.EnsurePersistent(taskToEdit);

                Message = "Task Edited Successfully";

                return this.RedirectToAction(a => a.Index(project.Id));
            }
			
            var viewModel = TaskViewModel.Create(Repository, project);
            viewModel.Task = task;

            return View(viewModel);
        }
        
        //
        // GET: /Task/Delete/5 
        public ActionResult Delete(int id)
        {
			var task = _taskRepository.GetNullableById(id);

            if (task == null) return RedirectToAction("Index", "Project");

            var project = task.Project;

            // validate access
            var redirect = _accessValidator.CheckEditAccess(CurrentUser.Identity.Name, project);
            if (redirect != null)
            {
                Message = "Not authorized to edit project.";
                return redirect;
            }

            return View(task);
        }

        //
        // POST: /Task/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Task task)
        {
			var taskToDelete = _taskRepository.GetNullableById(id);

            if (taskToDelete == null) return RedirectToAction("Index", "Project");

            var project = taskToDelete.Project;

            // validate access
            var redirect = _accessValidator.CheckEditAccess(CurrentUser.Identity.Name, project);
            if (redirect != null)
            {
                Message = "Not authorized to edit project.";
                return redirect;
            }

            _taskRepository.Remove(taskToDelete);

            Message = "Task Removed Successfully";

            return this.RedirectToAction(a=>a.Index(project.Id));
        }

    }
}
