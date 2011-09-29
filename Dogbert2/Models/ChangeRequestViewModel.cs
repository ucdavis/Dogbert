using Dogbert2.Core.Domain;
using UCDArch.Core.PersistanceSupport;
using UCDArch.Core.Utils;

namespace Dogbert2.Models
{
    /// <summary>
    /// ViewModel for the ChangeRequest class
    /// </summary>
    public class ChangeRequestViewModel
    {
        public Project Project { get; set; }
        public ChangeRequest ChangeRequest { get; set; }
 
        public static ChangeRequestViewModel Create(IRepository repository, Project project, ChangeRequest changeRequest = null)
        {
            Check.Require(repository != null, "Repository must be supplied");
			
            var viewModel = new ChangeRequestViewModel
                                {
                                    Project = project,
                                    ChangeRequest = changeRequest ?? new ChangeRequest()
                                };
 
            return viewModel;
        }
    }
}