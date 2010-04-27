using System;
using System.Linq;
using Dogbert.Core.Domain;
using UCDArch.Core.PersistanceSupport;

namespace Dogbert.Controllers.ViewModels
{
    public class ProjectsViewModel
    {
        public IQueryable<Project> WebApplications { get; set; }

        public static ProjectsViewModel Create(IRepository repository)
        {
            throw new NotImplementedException();
        }
    }
}
