using System.Collections.Generic;
using Dogbert2.Core.Domain;
using Dogbert2.Services;
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

        public static AccessRequestViewModel Create(IRepository repository, IDirectorySearchService directorySearchService, string loginId)
        {
            Check.Require(repository != null, "Repository must be supplied");
			
            // execute the search
            var directoryUser = directorySearchService.FindUser(loginId);

            var accessRequest = new AccessRequest()
                                    {
                                        LoginId = loginId,
                                        FirstName = directoryUser.FirstName, LastName = directoryUser.LastName,
                                        Email = directoryUser.EmailAddress
                                    };

            var viewModel = new AccessRequestViewModel
                                {
                                    AccessRequest = accessRequest, 
                                    Departments = repository.OfType<Department>().GetAll()
                                };
 
            return viewModel;
        }
    }
}