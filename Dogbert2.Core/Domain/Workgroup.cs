using System.Collections.Generic;
using FluentNHibernate.Mapping;
using UCDArch.Core.DomainModel;

namespace Dogbert2.Core.Domain
{
    public class Workgroup : DomainObject
    {
        public virtual string Name { get; set; }
        public virtual bool IsActive { get; set; }

        public virtual Department Department { get; set; }

        public IList<Worker> Workers { get; set; }
    }

    public class WorkgroupMap : ClassMap<Workgroup>
    {
        public WorkgroupMap()
        {
            Id(x => x.Id);

            Map(x => x.Name);
            Map(x => x.IsActive);

            References(x => x.Department);

            HasManyToMany(x => x.Workers)
                .ParentKeyColumn("WorkgroupId").ChildKeyColumn("WorkerId")
                .Table("WorkgroupsXWorkers").Cascade.SaveUpdate();
        }
    }
}
