using System.Collections.Generic;
using System.Linq;
using Dogbert.Core.Domain;
using UCDArch.Core.PersistanceSupport;
using UCDArch.Core.Utils;

namespace Dogbert.Controllers.ViewModels
{
    public class ChangeLogViewModel
    {
        public Project Project { get; set; }
        public ChangeLog ChangeLog { get; set; }

        public static ChangeLogViewModel Create(IRepository repository, Project project)
        {
            Check.Require(repository != null, "Repository is required");
            // ReSharper disable PossibleNullReferenceException            
            var viewModel = new ChangeLogViewModel
            {
                Project = project
            };

            // ReSharper restore PossibleNullReferenceException
            if (viewModel.ChangeLog == null)
            {
                viewModel.ChangeLog = new ChangeLog();
            }

            return viewModel;
        }
    }
}
