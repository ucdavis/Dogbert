using System;
using NHibernate.Validator.Constraints;
using UCDArch.Core.DomainModel;
using UCDArch.Core.NHibernateValidator.Extensions;

namespace Dogbert.Core.Domain
{
    public class ProjectFile : DomainObject
    {
        [Required]
        [Length(100)]
        public virtual string FileName { get; set; }

        [NotNull]
        public virtual FileType Type { get; set; }

        
        public virtual DateTime DateAdded { get; set; }

        [NotNull]
        public virtual Project Project { get; set; }
    }
}