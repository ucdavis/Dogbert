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

            var viewModel = new WorkgroupWorkerViewModel()
                                {
                                    WorkgroupWorker = new WorkgroupWorker(){Workgroup = workgroup},
                                    Workers = repository.OfType<Worker>().GetAll()
                                };

            return viewModel;
        }
    }
}