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
    public class ProjectSectionViewModel
    {
        public Project Project { get; set; }
        public ProjectSection ProjectSection { get; set; }
        public IEnumerable<SectionType> SectionTypes { get; set; }

        public static ProjectSectionViewModel Create(IRepository repository, Project project)
        {
            Check.Require(repository != null, "Repository must be supplied");
            Check.Require(project != null, "project is required.");

            var viewModel = new ProjectSectionViewModel
                                {
                                    Project = project,
                                    ProjectSection = new ProjectSection() {Project = project},
                                    SectionTypes = repository.OfType<SectionType>().Queryable.Where(a=>a.IsActive && !a.IsSpecial).OrderBy(a=>a.Order).ToList()
                                };

            return viewModel;
        }
    }
}