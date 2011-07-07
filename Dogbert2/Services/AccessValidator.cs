using System;
using System.Collections.Generic;
using Dogbert2.Core.Domain;
using Dogbert2.Services;
using System.Linq;
using UCDArch.Core.PersistanceSupport;
using UCDArch.Data.NHibernate;

namespace Dogbert2.Helpers
{
    public class AccessValidatorService : IAccessValidatorService
    {
        private readonly IRepository<Worker> _workerRepository;

        public AccessValidatorService(IRepository<Worker> workerRepository)
        {
            _workerRepository = workerRepository;
        }

        /// <summary>
        /// Returns a user's list of workgroups
        /// </summary>
        /// <param name="loginId"></param>
        /// <returns></returns>
        public IEnumerable<Workgroup> GetWorkgroupsByUser(string loginId)
        {
            var worker = _workerRepository.Queryable.Where(a => a.LoginId == loginId).Single();

            return worker.WorkgroupWorkers.Where(a=>a.Workgroup.IsActive).Select(a => a.Workgroup).ToList();
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