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

    public class PriorityType : LookupBase
    {
    }

    public class Actor : LookupBase
    {
        public virtual IList<UseCase> UseCases { get; set; }
    }

    public class FileType : LookupBase
    {
    }
}