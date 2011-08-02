using System.Collections.Generic;
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

        public static TaskViewModel Create(IRepository repository, Project project)
        {
            Check.Require(repository != null, "Repository must be supplied");
			
            var viewModel = new TaskViewModel {Task = new Task(), Project = project};

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
}