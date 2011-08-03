using System;
using System.Collections.Generic;
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

            UseCases = new List<UseCase>();
            Tasks = new List<Task>();
        }

        [Required]
        [DataType(DataType.MultilineText)]
        public virtual string Description { get; set; }
        [Range(0, 10)]
        [DataType(DataTypes.Range)]
        [Display(Name="Technical Difficulty")]
        public virtual int TechnicalDifficulty { get; set; }
        public virtual bool IsComplete { get; set; }
        [Required]
        public virtual string RequirementId { get; set; }

        [Required]
        [Display(Name="Requirement Type")]
        public virtual RequirementType RequirementType { get; set; }
        [Required]
        [Display(Name="Priority Type")]
        public virtual PriorityType PriorityType { get; set; }
        [Required]
        public virtual Project Project { get; set; }
        [Required]
        [Display(Name="Category")]
        public virtual RequirementCategory RequirementCategory { get; set; }

        public virtual DateTime DateAdded { get; set; }
        public virtual DateTime LastModified { get; set; }

        public virtual IList<UseCase> UseCases { get; set; }
        public virtual IList<Task> Tasks { get; set; }
    }

    public class RequirementMap : ClassMap<Requirement>
    {
        public RequirementMap()
        {
            Id(x => x.Id);

            Map(x => x.Description);
            Map(x => x.TechnicalDifficulty);
            Map(x => x.IsComplete);

            Map(x => x.RequirementId);

            References(x => x.RequirementType);
            References(x => x.PriorityType);
            References(x => x.Project);
            References(x => x.RequirementCategory).Column("CategoryId");

            Map(x => x.DateAdded);
            Map(x => x.LastModified);

            HasManyToMany(x => x.UseCases)
                .ParentKeyColumn("RequirementId")
                .ChildKeyColumn("UseCaseId")
                .Table("UseCaseXRequirements").Cascade.SaveUpdate();

            HasManyToMany(x => x.Tasks).ParentKeyColumn("RequirementId").ChildKeyColumn("TaskId").Table("TasksXRequirements").Cascade.SaveUpdate();
        }
    }
}
