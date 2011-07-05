using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FluentNHibernate.Mapping;
using UCDArch.Core.DomainModel;

namespace Dogbert2.Core.Domain
{
    public class Department : DomainObjectWithTypedId<string>
    {
        [StringLength(50)]
        public virtual string Name { get; set; }

        public virtual IList<Workgroup> Workgroups { get; set; }
    }

    public class DepartmentMap : ClassMap<Department>
    {
        public DepartmentMap()
        {
            Id(x => x.Id).Length(4);
            Map(x => x.Name);

            HasMany(x => x.Workgroups).Inverse().Cascade.AllDeleteOrphan();
        }
    }
}
