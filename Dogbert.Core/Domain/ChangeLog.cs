using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UCDArch.Core.DomainModel;
using NHibernate.Validator.Constraints;
using UCDArch.Core.NHibernateValidator.Extensions;

namespace Dogbert.Core.Domain
{
    public class ChangeLog : DomainObject
    {
        public ChangeLog()
        {
            Created = DateTime.Now;
            Updated = DateTime.Now;
        }
       
        [Length(50)]
        public virtual string Reason { get; set; }
        [Required]
        [Length(50)]
        public virtual string RequestedBy { get; set; }
 
        [Required]
        public virtual string Change { get; set; }
        public virtual DateTime Created { get; set; }
        public virtual DateTime Updated { get; set; }
        public virtual Project Project { get; set; }

        public ChangeLog(string change, string reason, string requestedBy, Project project)
        {
            Change = change;
            Reason = reason;
            RequestedBy = requestedBy;
            Project = project;
        }
    }
}
