using System.Collections.Generic;
using System.Linq;
using NHibernate.Validator.Constraints;
using UCDArch.Core.DomainModel;
using UCDArch.Core.NHibernateValidator.Extensions;

namespace Dogbert.Core.Domain
{
    public class LookupBase : DomainObject
    {
        [Required]
        [Length(50)]
        public virtual string Name { get; set; }

        public virtual bool IsActive { get; set; }
    }

    public class Status : LookupBase
    {
        public virtual bool IsComplete { get; set; }
    }

    public class PersonType : LookupBase
    {
    }

    public class ProjectType : LookupBase
    {
        public virtual IList<Project> Projects { get; set; }

        public virtual IQueryable<Project> GetActiveProjects
        {
            get
            {
                if (Projects != null)
                {
                    return from p in Projects.AsQueryable()
                           where p.Status.IsComplete == false && p.Status.IsActive
                           orderby p.Priority
                           select p;
                }
                else
                {
                    return null;
                }
            }
        }
    }

    public class RequirementType : LookupBase
    {
    }

    public class PriorityType : LookupBase
    {
    }

    public class Actor : LookupBase
    {
    }

    public class Category : LookupBase
    {
        public virtual Project Project { get; set; }
    }

    public class FileType : LookupBase
    {
    }
}