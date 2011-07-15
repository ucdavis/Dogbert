using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using Dogbert2.Core.Domain;
using Dogbert2.Services;
using System.Linq;
using UCDArch.Core.PersistanceSupport;

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

        public RedirectToRouteResult CheckEditAccess(string loginId, Project project)
        {
            var access = HasAccess(loginId, project);

            if (access == AccessLevel.NoAccess)
            {
                return new RedirectToRouteResult(new RouteValueDictionary{{"controller", "Error"}, {"action", "NotAuthorzied"} });
            }
            
            if (access == AccessLevel.Read)
            {
                var dictionary = new RouteValueDictionary {{"action", "Index"}};
                dictionary["id"] = project.Id;

                return new RedirectToRouteResult(dictionary);
            }

            return null;
        }

        public RedirectToRouteResult CheckReadAccess(string loginId, Project project)
        {
            var access = HasAccess(loginId, project);

            if (access == AccessLevel.NoAccess)
            {
                return new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Error" }, { "action", "NotAuthorzied" } });
            }

            return null;
        }
    }
}