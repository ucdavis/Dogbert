using System.ComponentModel.DataAnnotations;
using FluentNHibernate.Mapping;
using UCDArch.Core.DomainModel;

namespace Dogbert2.Core.Domain
{
    public class UseCasePostcondition : DomainObject
    {
        [Required]
        public virtual string Description { get; set; }
        [Required]
        public virtual UseCase UseCase { get; set; }
    }

    public class UseCasePostconditionMap : ClassMap<UseCasePostcondition>
    {
        public UseCasePostconditionMap()
        {
            Id(x => x.Id);

            Map(x => x.Description);
            References(x => x.UseCase);
        }
    }
}
