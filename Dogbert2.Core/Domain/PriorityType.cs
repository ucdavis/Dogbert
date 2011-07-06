using FluentNHibernate.Mapping;
using UCDArch.Core.DomainModel;

namespace Dogbert2.Core.Domain
{
    public class PriorityType : DomainObjectWithTypedId<char>
    {
        public virtual string Name { get; set; }
        public virtual string IsActive { get; set; }
        public virtual int Order { get; set; }
    }

    public class PriorityTypeMap : ClassMap<PriorityType>
    {
        public PriorityTypeMap()
        {
            ReadOnly();

            Id(x => x.Id);

            Map(x => x.Name);
            Map(x => x.IsActive);
            Map(x => x.Order).Column("`Order`");
        }
    }
}
