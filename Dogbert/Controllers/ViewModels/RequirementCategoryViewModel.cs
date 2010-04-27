using Dogbert.Core.Domain;
using UCDArch.Core.PersistanceSupport;
using UCDArch.Core.Utils;

namespace Dogbert.Controllers.ViewModels
{
    public class RequirementCategoryViewModel
    {
        public Project Project { get; set; }
        public RequirementCategory RequirementCategory { get; set; }

        public static RequirementCategoryViewModel Create(IRepository repository, Project project)
        {
            Check.Require(repository != null, "Repository is required");
            // ReSharper disable PossibleNullReferenceException            
            var viewModel = new RequirementCategoryViewModel() {Project = project};

            // ReSharper restore PossibleNullReferenceException
            if (viewModel.Project == null)
            {
                viewModel.Project = new Project();
            }

            return viewModel;
        }
    }
}
