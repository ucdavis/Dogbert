using System;
using System.ComponentModel.DataAnnotations;
using Dogbert2.Core.Resources;
using FluentNHibernate.Mapping;
using UCDArch.Core.DomainModel;

namespace Dogbert2.Core.Domain
{
    public class Requirement : DomainObject
    {
        public Requirement()
        {
            SetDefaults();
        }

        private void SetDefaults()
        {
            DateAdded = DateTime.Now;
            LastModified = DateTime.Now;
        }

        [Required]
        [DataType(DataType.MultilineText)]
        public virtual string Description { get; set; }
        [Range(0, 10)]
        [DataType(DataTypes.Range)]
        public virtual int TechnicalDifficulty { get; set; }
        public virtual bool IsComplete { get; set; }

        [Required]
        public virtual RequirementType RequirementType { get; set; }
        [Required]
        public virtual PriorityType PriorityType { get; set; }
        [Required]
        public virtual Project Project { get; set; }
        [Required]
        public virtual RequirementCategory RequirementCategory { get; set; }

        public virtual DateTime DateAdded { get; set; }
        public virtual DateTime LastModified { get; set; }
    }

    public class RequirementMap : ClassMap<Requirement>
    {
        public RequirementMap()
        {
            Id(x => x.Id);

            Map(x => x.Description);
            Map(x => x.TechnicalDifficulty);
            Map(x => x.IsComplete);

            References(x => x.RequirementType);
            References(x => x.PriorityType);
            References(x => x.Project);
            References(x => x.RequirementCategory).Column("CategoryId");

            Map(x => x.DateAdded);
            Map(x => x.LastModified);
        }
    }
}
