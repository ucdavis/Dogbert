using System.Collections.Generic;
using Dogbert2.Core.Domain;
using UCDArch.Core.PersistanceSupport;
using UCDArch.Core.Utils;

namespace Dogbert2.Models
{
    /// <summary>
    /// ViewModel for the AccessRequest class
    /// </summary>
    public class AccessRequestViewModel
    {
        public AccessRequest AccessRequest { get; set; }
        public IEnumerable<Department> Departments { get; set; }

        public static AccessRequestViewModel Create(IRepository repository)
        {
            Check.Require(repository != null, "Repository must be supplied");
			
            var viewModel = new AccessRequestViewModel {AccessRequest = new AccessRequest(), Departments = repository.OfType<Department>().GetAll()};
 
            return viewModel;
        }
    }
}