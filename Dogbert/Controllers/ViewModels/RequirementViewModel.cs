using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dogbert.Core.Domain;
using UCDArch.Core.PersistanceSupport;
using UCDArch.Core.Utils;

namespace Dogbert.Controllers.ViewModels
{
    public class RequirementViewModel
    {
        public IEnumerable<RequirementType> RequirementTypes { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<PriorityType> PriorityType { get; set; }
        public Project Project { get; set; }
        public Requirement Requirement { get; set; }

        public static RequirementViewModel Create(IRepository repository, Project project)
        {
            Check.Require(repository != null, "Repository is required");

            var viewModel = new RequirementViewModel()
                                {
                                    RequirementTypes =
                                        repository.OfType<RequirementType>().Queryable.Where(a => a.IsActive).ToList(),
                                    Categories = repository.OfType<Category>().Queryable.Where(a => a.IsActive).ToList(),
                                    PriorityType =
                                        repository.OfType<PriorityType>().Queryable.Where(a => a.IsActive).ToList(),
                                    Project = project
                                };

            return viewModel;
        }
    }
}
