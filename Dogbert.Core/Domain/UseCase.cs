using System;
using System.Collections.Generic;
using NHibernate.Validator.Constraints;
using UCDArch.Core.DomainModel;
using UCDArch.Core.NHibernateValidator.Extensions;

namespace Dogbert.Core.Domain
{
    public class UseCase : DomainObject
    {

        public virtual IList<UseCase> Parents { get; set; }
        public virtual IList<UseCase> Children { get; set; }
        public virtual IList<Actor> Actors { get; set; }
        public virtual IList<UseCaseStep> Steps { get; set; }
        public virtual IList<Requirement> Requirements { get; set; }

        [Required]
        [Length(100)]
        public virtual string Name { get; set; }
        [Required]
        public virtual string Description { get; set; }
  
        public virtual DateTime DateAdded { get; set; }
        public virtual DateTime LastModified { get; set; }
        [NotNull]
        public virtual Project Project { get; set; }
        [NotNull]
        public virtual RequirementCategory RequirementCategory { get; set; }
        [Required]
        public virtual string Precondition { get; set; }
        [Required]
        public virtual string Postcondition { get; set; }


        public UseCase()
        {
            Steps = new List<UseCaseStep>();
            Actors = new List<Actor>();
            Requirements = new List<Requirement>();
            Children = new List<UseCase>();
            Parents = new List<UseCase>();
        }

        public virtual void addChild(UseCase Child)
        {
            this.Children.Add(Child);
        }

        public virtual void AddActors(Actor Actor)
        {
            this.Actors.Add(Actor);
        }

       

        public virtual void AddSteps(UseCaseStep UseCaseStep)
        {
            UseCaseStep.UseCase = this;
            this.Steps.Add(UseCaseStep);
        }



    }
}
