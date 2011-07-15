using Dogbert2.Core.Domain;
using UCDArch.Core.PersistanceSupport;
using UCDArch.Core.Utils;

namespace Dogbert2.Models
{
    /// <summary>
    /// ViewModel for the RequirementCategory class
    /// </summary>
    public class RequirementCategoryViewModel
    {
        public RequirementCategory RequirementCategory { get; set; }
        public Project Project { get; set; }

        public static RequirementCategoryViewModel Create(IRepository repository, Project project, RequirementCategory requirementCateogry = null)
        {
            Check.Require(repository != null, "Repository must be supplied");
			
            var viewModel = new RequirementCategoryViewModel
                                {
                                    RequirementCategory = requirementCateogry ?? new RequirementCategory(),
                                    Project = project
                                };
 
            return viewModel;
        }
    }
}