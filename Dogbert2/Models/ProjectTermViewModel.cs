using Dogbert2.Core.Domain;
using UCDArch.Core.PersistanceSupport;
using UCDArch.Core.Utils;

namespace Dogbert2.Models
{
    /// <summary>
    /// ViewModel for the ProjectTerm class
    /// </summary>
    public class ProjectTermViewModel
    {
        public ProjectTerm ProjectTerm { get; set; }

        public static ProjectTermViewModel Create(IRepository repository, Project project)
        {
            Check.Require(repository != null, "Repository must be supplied");
            Check.Require(project != null, "project is required.");

            var viewModel = new ProjectTermViewModel { ProjectTerm = new ProjectTerm() {Project = project} };

            return viewModel;
        }
    }
}