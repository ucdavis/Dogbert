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

            Preconditions = new List<UseCasePrecondition>();
            Postconditions = new List<UseCasePostcondition>();
        }

        [Required]
        [StringLength(100)]
        public virtual string Name { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public virtual string Description { get; set; }
        [StringLength(100)]
        public virtual string Roles { get; set; }

        public virtual DateTime DateAdded { get; set; }
        public virtual DateTime DateModified { get; set; }

        [Required]
        public virtual Project Project { get; set; }
        [Required]
        [Display(Name="Category")]
        public virtual RequirementCategory RequirementCategory { get; set; }

        public virtual IList<UseCaseStep> UseCaseSteps { get; set; }
        public virtual IList<Requirement> Requirements { get; set; }

        public virtual IList<UseCasePrecondition> Preconditions { get; set; }
        public virtual IList<UseCasePostcondition> Postconditions { get; set; }

        /// <summary>
        /// Add a use case step
        /// </summary>
        /// <param name="useCaseStep"></param>
        public virtual void AddStep(UseCaseStep useCaseStep)
        {
            useCaseStep.UseCase = this;
            UseCaseSteps.Add(useCaseStep);
        }

        /// <summary>
        /// Add a precondition
        /// </summary>
        /// <param name="precondition"></param>
        public virtual void AddPrecondition(UseCasePrecondition precondition)
        {
            precondition.UseCase = this;
            Preconditions.Add(precondition);
        }

        /// <summary>
        /// Add a postcondition
        /// </summary>
        /// <param name="postcondition"></param>
        public virtual void AddPostcondition(UseCasePostcondition postcondition)
        {
            postcondition.UseCase = this;
            Postconditions.Add(postcondition);
        }
    }

    public class UseCaseMap : ClassMap<UseCase>
    {
        public UseCaseMap()
        {
            Id(x => x.Id);

            Map(x => x.Name);
            Map(x => x.Description);
            Map(x => x.DateAdded);
            Map(x => x.DateModified);
            Map(x => x.Roles);

            References(x => x.Project);
            References(x => x.RequirementCategory);

            HasMany(x => x.UseCaseSteps).Inverse().Cascade.AllDeleteOrphan();
            HasManyToMany(x => x.Requirements).ParentKeyColumn("UseCaseId").ChildKeyColumn("RequirementId").Table("UseCaseXRequirements").Cascade.SaveUpdate();

            HasMany(x => x.Preconditions).Inverse().Cascade.AllDeleteOrphan();
            HasMany(x => x.Postconditions).Inverse().Cascade.AllDeleteOrphan();
        }
    }
}
