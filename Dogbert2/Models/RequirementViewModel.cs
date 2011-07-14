using Dogbert2.Core.Domain;
using UCDArch.Core.PersistanceSupport;
using UCDArch.Core.Utils;

namespace Dogbert2.Models
{
    /// <summary>
    /// ViewModel for the Requirement class
    /// </summary>
    public class RequirementViewModel
    {
        public Project Project { get; set; }
        public Requirement Requirement { get; set; }
 
        public static RequirementViewModel Create(IRepository repository, Project project = null)
        {
            Check.Require(repository != null, "Repository must be supplied");
			
            var viewModel = new RequirementViewModel
                                {
                                    Requirement = new Requirement(),
                                    Project = project ?? new Project()
                                };
 
            return viewModel;
        }
    }
}