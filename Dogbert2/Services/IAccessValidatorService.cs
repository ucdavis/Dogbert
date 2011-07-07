using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dogbert2.Core.Domain;

namespace Dogbert2.Services
{
    public interface IAccessValidatorService
    {
        IEnumerable<Workgroup> GetWorkgroupsByUser(string loginId);

        AccessLevel HasAccess(string loginId, Project project);
    }

    public enum AccessLevel {Read,Edit,NoAccess};
}
