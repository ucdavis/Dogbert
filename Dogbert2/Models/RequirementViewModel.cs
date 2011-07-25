using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<RequirementType> RequirementTypes { get; set; }
        public IEnumerable<PriorityType> PriorityTypes { get; set; }
        public IEnumerable<RequirementCategory> RequirementCategories { get; set; }

        public static RequirementViewModel Create(IRepository repository, Project project)
        {
            Check.Require(repository != null, "Repository must be supplied");
            Check.Require(project != null, "project is required.");
			
            var viewModel = new RequirementViewModel
                                {
                                    Requirement = new Requirement(),
                                    Project = project,
                                    RequirementTypes = repository.OfType<RequirementType>().GetAll(),
                                    PriorityTypes = repository.OfType<PriorityType>().GetAll(),
                                    RequirementCategories = project.RequirementCategories.Where(a => a.IsActive).ToList()
                                };
 
            return viewModel;
        }
    }
}