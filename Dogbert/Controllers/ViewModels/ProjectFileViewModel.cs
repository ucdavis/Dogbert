using System.Collections.Generic;
using System.Linq;
using Dogbert.Core.Domain;
using Elmah;
using UCDArch.Core.PersistanceSupport;
using UCDArch.Core.Utils;

namespace Dogbert.Controllers.ViewModels
{
    public class ProjectFileViewModel
    {
        public IEnumerable<FileType> FileTypes { get; set; }
        public Project Project { get; set; }
        public ProjectFile ProjectFile { get; set; }

        public static ProjectFileViewModel Create(IRepository repository, Project project)
        {
            Check.Require(repository != null, "Repository is required");
            // ReSharper disable PossibleNullReferenceException
            var viewModel = new ProjectFileViewModel
                                {
                                    FileTypes = repository.OfType<FileType>().Queryable.Where(a => a.IsActive).ToList(),
                                    Project = project
                                };
            // ReSharper restore PossibleNullReferenceException
            if (viewModel.ProjectFile == null)
            {
                viewModel.ProjectFile = new ProjectFile();
            }

            return viewModel;
        }
    }
}
