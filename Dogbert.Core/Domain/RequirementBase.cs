using System;
using UCDArch.Core.DomainModel;
using UCDArch.Core.NHibernateValidator.Extensions;

namespace Dogbert.Core.Domain
{
    public class RequirementBase : DomainObject
    {
        [Required]
        public virtual string Name { get; set; }

        [Required]
        public virtual RequirementType Type { get; set; }

        [Required]
        public virtual PriorityType PriorityType { get; set; }

        public virtual string Description { get; set; }

        [Required]
        public virtual Project Project { get; set; }

        public virtual Category Category { get; set; }

        [Required]
        public virtual DateTime DateAdded { get; set; }
    }
}