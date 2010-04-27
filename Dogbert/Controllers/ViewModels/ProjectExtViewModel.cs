using System;
using System.Collections.Generic;
using System.Linq;
using Dogbert.Core.Domain;
using UCDArch.Core.PersistanceSupport;
using UCDArch.Core.Utils;

namespace Dogbert.Controllers.ViewModels
{
  
    public class ProjectExtViewModel: ProjectViewModel
    {   //add text (purpose, description, etc) to project
        public ProjectText ProjectText { get; set; }
        public IList<TextType> TextTypes { get; set; }
        public Project Project { get; set; }
        public static ProjectExtViewModel Create(IRepository repository)
        {
            Check.Require(repository != null, "Repository is required");
            var viewModel = new ProjectExtViewModel
            {
                TextTypes = repository.OfType<TextType>().Queryable.ToList(),
            };
            return viewModel;
        }

    }


}
