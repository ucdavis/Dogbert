using System.Collections.Generic;
using Dogbert2.Core.Domain;
using UCDArch.Core.PersistanceSupport;
using UCDArch.Core.Utils;
using System.Linq;

namespace Dogbert2.Models
{
    /// <summary>
    /// ViewModel for the Project class
    /// </summary>
    public class ProjectViewModel
    {
        public Project Project { get; set; }
        public Workgroup Workgroup { get; set; }

        public IEnumerable<StatusCode> StatusCodes { get; set; }
        public IEnumerable<PriorityType> PriorityTypes { get; set; }
        public IEnumerable<ProjectType> ProjectTypes { get; set; }
        public IEnumerable<Workgroup> Workgroups { get; set; }
        public IEnumerable<Worker> Workers { get; set; }

        public static ProjectViewModel Create(IRepository repository, string login)
        {
            Check.Require(repository != null, "Repository must be supplied");

            var worker = repository.OfType<Worker>().Queryable.Where(a => a.LoginId == login).First();

            var viewModel = new ProjectViewModel {
                Project = new Project(),
                StatusCodes = repository.OfType<StatusCode>().GetAll(),
                ProjectTypes = repository.OfType<ProjectType>().Queryable.Where(a=>a.IsActive).OrderBy(a=>a.Order).ToList(),
                PriorityTypes = repository.OfType<PriorityType>().Queryable.OrderBy(a=>a.Order).ToList(),
                Workgroups = worker.Workgroups,
                //Workers = new List<Worker>()
            };

            var workers = new List<Worker>();
            foreach (var a in viewModel.Workgroups)
            {
                workers.AddRange(a.Workers);
            }
            viewModel.Workers = workers.Distinct().OrderBy(a => a.LastName).ToList();

            return viewModel;
        }
    }
}