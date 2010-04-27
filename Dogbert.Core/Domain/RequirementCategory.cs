using System;
using System.Collections.Generic;
using NHibernate.Validator.Constraints;
using UCDArch.Core.DomainModel;
using UCDArch.Core.NHibernateValidator.Extensions;

namespace Dogbert.Core.Domain
{
    public class RequirementCategory : DomainObject
    {
        [Length(50)]
        [Required]
        public virtual string Name { get; set; }

        public virtual bool IsActive { get; set; }
        [NotNull]
        public virtual Project Project { get; set; }
    }
}
