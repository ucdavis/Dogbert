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

        [Required]
        public virtual FileType Type { get; set; }

        [Required]
        public virtual DateTime DateAdded { get; set; }

        [Required]
        public virtual Project Project { get; set; }
    }
}