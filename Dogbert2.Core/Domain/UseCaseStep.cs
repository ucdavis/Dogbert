using System;
using System.ComponentModel.DataAnnotations;
using FluentNHibernate.Mapping;
using UCDArch.Core.DomainModel;

namespace Dogbert2.Core.Domain
{
    public class UseCaseStep : DomainObject
    {
        public UseCaseStep()
        {
            SetDefaults();
        }

        private void SetDefaults()
        {
            Order = 999;
            Optional = false;

            DateAdded = DateTime.Now;
            DateModified = DateTime.Now;
        }

        [Required]
        [DataType(DataType.MultilineText)]
        public virtual string Description { get; set; }
        public virtual int Order { get; set; }
        public virtual bool Optional { get; set; }

        public virtual DateTime DateAdded { get; set; }
        public virtual DateTime DateModified { get; set; }

        [Required]
        public virtual UseCase UseCase { get; set; }
    }

    public class UseCaseStepMap : ClassMap<UseCaseStep>
    {
        public UseCaseStepMap()
        {
            Id(x => x.Id);

            Map(x => x.Description);
            Map(x => x.Order).Column("`Order`");
            Map(x => x.Optional);
            Map(x => x.DateAdded);
            Map(x => x.DateModified);
            References(x => x.UseCase);
        }
    }

}
