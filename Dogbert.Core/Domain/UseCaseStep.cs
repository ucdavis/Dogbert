using System;
using System.Collections.Generic;
using NHibernate.Validator.Constraints;
using UCDArch.Core.DomainModel;
using UCDArch.Core.NHibernateValidator.Extensions;

namespace Dogbert.Core.Domain
{
    public class UseCaseStep : DomainObject
    {
        [Required]
        public virtual string Description { get; set; }
        public virtual UseCase UseCase { get; set; }
        public virtual int Order { get; set; }
        public virtual bool Optional { get; set; }
        public virtual DateTime? DateAdded { get; set; }
        public virtual DateTime? LastModified { get; set; }
    }
}
