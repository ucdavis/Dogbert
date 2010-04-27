using System;
using System.Collections.Generic;
using System.Linq;
using Dogbert.Core.Domain;
using UCDArch.Core.PersistanceSupport;
using UCDArch.Core.Utils;

namespace Dogbert.Controllers.ViewModels
{
    public class ProjectViewModel
    {
        public Project Project { get; set; }
        public IList<ProjectType> ProjectTypes { get; set; }
        public IList<User> Users { get; set; }
        public static ProjectViewModel Create(IRepository repository)
        {
            Check.Require(repository != null,"Repository is required");
            var viewModel = new ProjectViewModel
            {
                ProjectTypes = repository.OfType<ProjectType>().Queryable.ToList(),
                Users = repository.OfType<User>().Queryable.Where(u => u.Inactive == false).ToList()

                //Where(p => p.ProjectType.Name == "Web Application")
            };
            return viewModel;
        }

    }


}
