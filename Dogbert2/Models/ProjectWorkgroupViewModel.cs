using System.Collections.Generic;
using System.Linq;
using Dogbert2.Core.Domain;
using UCDArch.Core.PersistanceSupport;
using UCDArch.Core.Utils;

namespace Dogbert2.Models
{
    /// <summary>
    /// ViewModel for the ProjectWorkgroup class
    /// </summary>
    public class ProjectWorkgroupViewModel
    {
        public ProjectWorkgroup ProjectWorkgroup { get; set; }
        public IEnumerable<Workgroup> Workgroups { get; set; }

        public static ProjectWorkgroupViewModel Create(IRepository repository, string loginId, ProjectWorkgroup projectWorkgroup = null)
        {
            Check.Require(repository != null, "Repository must be supplied");
			
            var viewModel = new ProjectWorkgroupViewModel
                                {
                                    ProjectWorkgroup = projectWorkgroup ?? new ProjectWorkgroup(),
                                    Workgroups = repository.OfType<Worker>().Queryable.Where(a=>a.LoginId == loginId).FirstOrDefault().WorkgroupWorkers.Select(a=>a.Workgroup).ToList()
                                };
 
            return viewModel;
        }
    }
}