using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Dogbert2.App_GlobalResources;
using Dogbert2.Core.Domain;
using Dogbert2.Models;
using Dogbert2.Services;
using UCDArch.Core.PersistanceSupport;
using MvcContrib;
using UCDArch.Web.ActionResults;
using UCDArch.Web.Helpers;

namespace Dogbert2.Controllers
{
    /// <summary>
    /// Controller for the Task class
    /// </summary>
    [Authorize]
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
            var viewModel = MyTasksViewModel.Create(Repository, CurrentUser.Identity.Name);

            return View(viewModel);
        }

        /// <summary>
        /// Worker's interface to update the task, can make comments and mark complete
        /// </summary>
        /// <param name="id"></param>
        /// <param name="comments"></param>
        /// <param name="complete"></param>
        /// <returns></returns>
        public JsonNetResult UpdateTask(int id, string description, string comments, bool complete = false)
        {
            var task = _taskRepository.GetNullableById(id);

            // validate user's permission
            if (task.Worker.LoginId != CurrentUser.Identity.Name)
            {
                return new JsonNetResult(false);
            }

            // update task
            task.Description = description;
            task.Comments = comments;
            task.Complete = complete;

            // save
            _taskRepository.EnsurePersistent(task);

            // just return true
            return new JsonNetResult(true);
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
        public ActionResult Create(int projectId, Task task, List<int> requirements)
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
            TransferValues(task, taskToCreate, requirements, project);

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

			var viewModel = TaskViewModel.Create(Repository, project, taskToCreate);

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

			var viewModel = TaskViewModel.Create(Repository, project, task);

			return View(viewModel);
        }
        
        //
        // POST: /Task/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Task task, List<int> requirements)
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

            //AutoMapper.Mapper.Map(task, taskToEdit);
            TransferValues(task, taskToEdit, requirements);

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

        /// <summary>
        /// Loads a list of requirements in a category
        /// </summary>
        /// <param name="projectId">Project Id</param>
        /// <param name="categoryId">Category Id</param>
        /// <returns></returns>
        public JsonNetResult LoadRequirements(int projectId, int categoryId)
        {
            var project = _projectRepository.GetNullableById(projectId);

            if (_accessValidator.HasAccess(CurrentUser.Identity.Name, project) == AccessLevel.Edit)
            {

                var requirements = project.Requirements.Where(a => a.RequirementCategory.Id == categoryId);

                return new JsonNetResult(requirements.Select(a => new {a.Id, a.RequirementId, a.Description}));

            }

            // nothing to return no access
            return new JsonNetResult(false);

        }

        private void TransferValues(Task src, Task dest, List<int> requirementIds, Project project = null)
        {
            AutoMapper.Mapper.Map(src, dest);

            if (project != null)
            {
                dest.Project = project;
            }

            // requirements not in the lsit of requirement ids
            var deletes = dest.Requirements.Where(a => !requirementIds.Contains(a.Id)).Select(a => a.Id).ToList();
            foreach (var rid in deletes)
            {
                var requirement = dest.Requirements.Where(a => a.Id == rid).FirstOrDefault();
                dest.Requirements.Remove(requirement);
            }

            project = dest.Project;


            requirementIds = requirementIds ?? new List<int>();

            // add in the new ones
            foreach (var rid in requirementIds)
            {
                // not in the list, go ahead and add
                if (!dest.Requirements.Any(a => a.Id == rid))
                {
                    var requirement = project.Requirements.Where(a => a.Id == rid).FirstOrDefault();

                    if (requirement != null) dest.Requirements.Add(requirement);
                }
            }

        }
    }
}
