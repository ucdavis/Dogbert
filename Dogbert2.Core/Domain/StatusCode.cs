using System.ComponentModel.DataAnnotations;
using FluentNHibernate.Mapping;
using UCDArch.Core.DomainModel;

namespace Dogbert2.Core.Domain
{
    public class StatusCode : DomainObjectWithTypedId<char>
    {
        [Required]
        [StringLength(50)]
        public virtual string Name { get; set; }
    }

    public class StatusCodeMap : ClassMap<StatusCode>
    {
        public StatusCodeMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
        }
    }
}
