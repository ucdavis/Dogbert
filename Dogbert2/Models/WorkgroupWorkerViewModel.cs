using System.Collections.Generic;
using System.Linq;
using Dogbert2.Core.Domain;
using UCDArch.Core.PersistanceSupport;
using UCDArch.Core.Utils;

namespace Dogbert2.Models
{
    public class WorkgroupWorkerViewModel
    {
        public WorkgroupWorker WorkgroupWorker { get; set; }

        public IEnumerable<Worker> Workers { get; set; }

        public static WorkgroupWorkerViewModel Create(IRepository repository, Workgroup workgroup, string loginId)
        {
            Check.Require(repository != null, "Repository is required.");

            // get the current user's workgroups
            var workgroups = repository.OfType<WorkgroupWorker>().Queryable.Where(a => a.Worker.LoginId == loginId).Select(a => a.Workgroup);

            // get all workers in those workgroups
            var workers = repository.OfType<WorkgroupWorker>().Queryable.Where(a => workgroups.Contains(a.Workgroup)).Select(a => a.Worker).Distinct();

            var viewModel = new WorkgroupWorkerViewModel()
                                {
                                    WorkgroupWorker = new WorkgroupWorker(){Workgroup = workgroup},
                                    Workers = workers.ToList()
                                };

            return viewModel;
        }
    }
}