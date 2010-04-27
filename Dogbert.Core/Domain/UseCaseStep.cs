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
        [Required]
        public virtual UseCase UseCase { get; set; }
        [Required]
        public virtual int Order { get; set; }
        [Required]
        public virtual bool Optional { get; set; }
    }
}
