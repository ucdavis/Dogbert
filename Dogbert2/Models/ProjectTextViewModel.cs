using System.Collections.Generic;
using Dogbert2.Core.Domain;
using UCDArch.Core.PersistanceSupport;
using UCDArch.Core.Utils;
using System.Linq;

namespace Dogbert2.Models
{
    /// <summary>
    /// ViewModel for the ProjectText class
    /// </summary>
    public class ProjectTextViewModel
    {
        public Project Project { get; set; }
        public ProjectText ProjectText { get; set; }
        public IEnumerable<TextType> TextTypes { get; set; }

        public static ProjectTextViewModel Create(IRepository repository, Project project)
        {
            Check.Require(repository != null, "Repository must be supplied");
            Check.Require(project != null, "project is required.");

            var viewModel = new ProjectTextViewModel
                                {
                                    Project = project,
                                    ProjectText = new ProjectText() {Project = project},
                                    TextTypes = repository.OfType<TextType>().Queryable.Where(a=>a.IsActive).OrderBy(a=>a.Order).ToList()
                                };

            return viewModel;
        }
    }
}