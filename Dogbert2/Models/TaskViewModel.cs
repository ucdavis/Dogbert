﻿using System.Collections.Generic;
using System.Linq;
using Dogbert2.Core.Domain;
using UCDArch.Core.PersistanceSupport;
using UCDArch.Core.Utils;

namespace Dogbert2.Models
{
    /// <summary>
    /// ViewModel for the Task class
    /// </summary>
    public class TaskViewModel
    {
        public Task Task { get; set; }
        public Project Project { get; set; }

        public IEnumerable<Worker> Workers { get; set; }
        public IEnumerable<RequirementCategory> RequirementCategories { get; set; }
        public IEnumerable<Requirement> Requirements { get; set; }

        public static TaskViewModel Create(IRepository repository, Project project, Task task = null)
        {
            Check.Require(repository != null, "Repository must be supplied");
			
            var viewModel = new TaskViewModel
                                {
                                    Task = task ?? new Task(), 
                                    Project = project, 
                                    RequirementCategories = project.RequirementCategories.Where(a => a.IsActive),
                                    Requirements = task != null ? project.Requirements.Where(a => a.RequirementCategory == task.RequirementCategory).ToList() : new List<Requirement>()
                                };

            // build the list of workers
            var workers = new List<Worker>();
            var workgroups = project.ProjectWorkgroups.Select(a => a.Workgroup);
            foreach (var a in workgroups)
            {
                workers.AddRange(a.WorkgroupWorkers.Select(b => b.Worker).ToList());
            }
            viewModel.Workers = workers.Distinct().OrderBy(a => a.LastName).ToList();

            return viewModel;
        }
    }

    public class MyTasksViewModel
    {
        public IEnumerable<Task> Tasks { get; set; }
        public IEnumerable<Project> Projects { get; set; }

        public static MyTasksViewModel Create(IRepository repository, string loginId)
        {
            Check.Require(repository != null, "repository is required.");

            var tasks = repository.OfType<Task>().Queryable.Where(a => a.Worker.LoginId == loginId && !a.Complete);
            var projects = tasks.Where(a => !a.Complete ).Select(a => a.Project).Distinct();

            var viewModel = new MyTasksViewModel() {Tasks = tasks, Projects = projects};

            return viewModel;
        }
    }
}