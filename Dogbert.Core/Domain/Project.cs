using System;
using System.Collections.Generic;
using NHibernate.Validator.Constraints;
using UCDArch.Core.DomainModel;
using UCDArch.Core.NHibernateValidator.Extensions;

namespace Dogbert.Core.Domain
{
    public class Project : DomainObject
    {
        [Required]
        [Length(50)]
        public virtual string Name { get; set; }

        public virtual int? Priority { get; set; }
        public virtual DateTime? Deadline { get; set; }

        [Length(100)]
        public virtual string Contact { get; set; }

        [Length(50)]
        public virtual string ContactEmail { get; set; }

        [Length(50)]
        public virtual string Unit { get; set; }

        public virtual int? Complexity { get; set; }
        public virtual DateTime? ProjectedStart { get; set; }
        public virtual DateTime? ProjectedEnd { get; set; }
        public virtual DateTime? BeginDate { get; set; }
        public virtual DateTime? EndDate { get; set; }

        public virtual string StartDateString
        {
            get
            {
                if (BeginDate.HasValue)
                {
                    return BeginDate.Value.ToShortDateString();
                }

                if (ProjectedStart.HasValue)
                {
                    return string.Format("{0} (proj.)", ProjectedStart.Value.ToShortDateString());
                }

                return "N/A";
            }
        }

        public virtual string EndDateString
        {
            get
            {
                if (EndDate.HasValue)
                {
                    return EndDate.Value.ToShortDateString();
                }

                if (ProjectedEnd.HasValue)
                {
                    return string.Format("{0} (proj.)", ProjectedEnd.Value.ToShortDateString());
                }

                return "N/A";
            }
        }

        [Required]
        public virtual Status Status { get; set; }

        public virtual User ProjectManager { get; set; }
        public virtual User LeadProgrammer { get; set; }

        public virtual string Description { get; set; }
        public virtual string Purpose { get; set; }

        public virtual ProjectType ProjectType { get; set; }

        public virtual IList<Requirement> Requirements { get; set; }
        public virtual IList<UseCase> UseCases { get; set; }
        public virtual IList<Category> Categories { get; set; }
        public virtual IList<ProjectFile> Files { get; set; }
    }
}