using Dogbert2.Core.Domain;
using Dogbert2.Services;
using System.Linq;
using UCDArch.Data.NHibernate;

namespace Dogbert2.Helpers
{
    public class AccessValidatorService : IAccessValidatorService
    {
        public AccessValidatorService()
        {
            
        }

        public AccessLevel HasAccess(string loginId, Project project)
        {
            var workgroups = project.ProjectWorkgroups.Select(x => x.Workgroup);

            // if in workgroup for project, can edit
            if (workgroups.Any(a => a.WorkgroupWorkers.Where(b=>!b.Limited).Any(b=>b.Worker.LoginId == loginId)))
            {
                return AccessLevel.Edit;
            }

            if (workgroups.Any(a => a.WorkgroupWorkers.Where(b => b.Limited).Any(b => b.Worker.LoginId == loginId)))
            {
                return AccessLevel.Read;
            }

            // fall back)
            return AccessLevel.NoAccess;
        }
    }
}