using System.Collections.Generic;
using Dogbert2.Clients;
using Dogbert2.Core.Domain;
using UCDArch.Core.PersistanceSupport;
using UCDArch.Core.Utils;

namespace Dogbert2.Models
{
    /// <summary>
    /// ViewModel for the Workgroup class
    /// </summary>
    public class WorkgroupViewModel
    {
        public Workgroup Workgroup { get; set; }
        public IEnumerable<Department> Departments { get; set; }

        public static WorkgroupViewModel Create(IRepository repository)
        {
            Check.Require(repository != null, "Repository must be supplied");

            var viewModel = new WorkgroupViewModel {Workgroup = new Workgroup(), Departments = repository.OfType<Department>().GetAll()};

            return viewModel;
        }
    }
}