using System.Collections.Generic;
using Dogbert.Core.Domain;
using UCDArch.Core.PersistanceSupport;
using UCDArch.Core.Utils;


namespace Dogbert.Controllers.ViewModels
{
    public class ActorViewModel
    {
        public Actor Actor { get; set; }
        public int projectId { get; set; } //Actors are added once in a project

        public static ActorViewModel Create(IRepository repository, int projectID)
        {
            Check.Require(repository != null, "Repository is required");
            var viewModel = new ActorViewModel();
            viewModel.projectId = projectID;
            return viewModel;
        }
    }
}
