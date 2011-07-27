using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FluentNHibernate.Mapping;
using UCDArch.Core.DomainModel;

namespace Dogbert2.Core.Domain
{
    public class UseCase : DomainObject
    {
        public UseCase()
        {
            SetDefaults();
        }

        private void SetDefaults()
        {
            DateAdded = DateTime.Now;
            DateModified = DateTime.Now;

            UseCaseSteps = new List<UseCaseStep>();
            Requirements = new List<Requirement>();
        }

        [Required]
        [StringLength(100)]
        public virtual string Name { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public virtual string Description { get; set; }
        [DataType(DataType.MultilineText)]
        public virtual string Precondition { get; set; }
        [DataType(DataType.MultilineText)]
        public virtual string Postcondition { get; set; }

        public virtual DateTime DateAdded { get; set; }
        public virtual DateTime DateModified { get; set; }

        [Required]
        public virtual Project Project { get; set; }
        [Required]
        [Display(Name="Category")]
        public virtual RequirementCategory RequirementCategory { get; set; }

        public virtual IList<UseCaseStep> UseCaseSteps { get; set; }
        public virtual IList<Requirement> Requirements { get; set; }

        public virtual void AddStep(UseCaseStep useCaseStep)
        {
            useCaseStep.UseCase = this;
            UseCaseSteps.Add(useCaseStep);
        }
    }

    public class UseCaseMap : ClassMap<UseCase>
    {
        public UseCaseMap()
        {
            Id(x => x.Id);

            Map(x => x.Name);
            Map(x => x.Description);
            Map(x => x.Precondition);
            Map(x => x.Postcondition);
            Map(x => x.DateAdded);
            Map(x => x.DateModified);

            References(x => x.Project);
            References(x => x.RequirementCategory);

            HasMany(x => x.UseCaseSteps).Inverse().Cascade.AllDeleteOrphan();

            HasManyToMany(x => x.Requirements).ParentKeyColumn("UseCaseId").ChildKeyColumn("RequirementId").Table("UseCaseXRequirements").Cascade.SaveUpdate();
        }
    }
}
