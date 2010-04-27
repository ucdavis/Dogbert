using System.Collections.Generic;
using System.Linq;
using Dogbert.Core.Domain;
using Elmah;
using UCDArch.Core.PersistanceSupport;
using UCDArch.Core.Utils;

namespace Dogbert.Controllers.ViewModels
{
    public class UseCaseViewModel
    {
        //used for use cases
        public UseCase UseCase { get; set; }
        public IList<UseCase> UseCases { get; set; }
        public IList<Actor> Actors { get; set; }
        public UseCaseStep UseCaseStep { get; set; }
        public IList<RequirementCategory> RequirementCategories { get; set; }
        public Project Project { get; set; }

        public static UseCaseViewModel Create(IRepository repository, Project project)
        {
            var viewModel = new UseCaseViewModel
            {
                //populate the stuff needed for use cases
                Actors = repository.OfType<Actor>().Queryable.ToList(),
                RequirementCategories = repository.OfType<RequirementCategory>().Queryable.ToList(),
                Project = project
            };
            return viewModel;
        }
    }
}
