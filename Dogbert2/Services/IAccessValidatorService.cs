using System.Collections.Generic;
using System.Web.Mvc;
using Dogbert2.Core.Domain;

namespace Dogbert2.Services
{
    public interface IAccessValidatorService
    {
        IEnumerable<Workgroup> GetWorkgroupsByUser(string loginId);

        AccessLevel HasAccess(string loginId, Project project);

        /// <summary>
        /// Returns the redirect command
        /// 
        /// Minimum of edit access is required
        /// </summary>
        /// <remarks>
        /// No access gets not authorized
        /// Read only gets back to index
        /// </remarks>
        /// <param name="loginId"></param>
        /// <param name="project"></param>
        /// <returns></returns>
        RedirectToRouteResult CheckEditAccess(string loginId, Project project);

        /// <summary>
        /// Returns the redirect command
        /// 
        /// Minimum of read access is required.
        /// </summary>
        /// <remarks>
        /// Only redirects to not authorized
        /// </remarks>
        /// <param name="loginId"></param>
        /// <param name="project"></param>
        /// <returns></returns>
        RedirectToRouteResult CheckReadAccess(string loginId, Project project);

    }

    public enum AccessLevel {Read,Edit,NoAccess};
}
