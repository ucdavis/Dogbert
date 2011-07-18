using System.Linq;
using Dogbert2.Core.Domain;
using UCDArch.Core.PersistanceSupport;
using UCDArch.Core.Utils;
using UCDArch.Data.NHibernate;

namespace Dogbert2.Models
{
    public class ProjectListViewModel
    {
        /// <summary>
        /// Projects that the user has admin status on
        /// </summary>
        public IQueryable<Project> Projects { get; set; }
        /// <summary>
        /// Workgroups that the user has admin status on
        /// </summary>
        public IQueryable<Workgroup> Workgroups { get; set; }

        public static ProjectListViewModel Create(IRepository repository, string loginId)
        {
            Check.Require(repository != null, "Repository is required.");

            // get all workgroups for which use has admin
            var workgroups = repository.OfType<WorkgroupWorker>().Queryable.Where(a => a.Admin && a.Worker.LoginId == loginId).Select(a => a.Workgroup);

            // get projects
            var projects = repository.OfType<ProjectWorkgroup>().Queryable.Where(a => !a.Project.Hide && workgroups.Contains(a.Workgroup)).Select(a => a.Project).Distinct();

            var viewModel = new ProjectListViewModel(){Projects = projects, Workgroups = workgroups};

            return viewModel;
        }
    }
}