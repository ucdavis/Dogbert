using System.Collections.Generic;
using System.Linq;
using Dogbert2.Core.Domain;
using UCDArch.Core.PersistanceSupport;
using UCDArch.Core.Utils;

namespace Dogbert2.Models
{
    /// <summary>
    /// ViewModel for the UseCase class
    /// </summary>
    public class UseCaseViewModel
    {
        public UseCase UseCase { get; set; }
        public Project Project { get; set; }
        
        public IEnumerable<RequirementCategory> RequirementCategories { get; set; }

        public static UseCaseViewModel Create(IRepository repository, Project project)
        {
            Check.Require(repository != null, "Repository must be supplied");
            Check.Require(project != null, "project is required.");

            var viewModel = new UseCaseViewModel
                                {
                                    UseCase = new UseCase(),
                                    Project = project,
                                    RequirementCategories = project.RequirementCategories.Where(a => a.IsActive).OrderBy(a=>a.Name).ToList()
                                };
 
            return viewModel;
        }
    }
}