using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FluentNHibernate.Mapping;
using UCDArch.Core.DomainModel;

namespace Dogbert2.Core.Domain
{
    public class Workgroup : DomainObject
    {
        public Workgroup()
        {
            SetDefaults();
        }

        private void SetDefaults()
        {
            Workers = new List<Worker>();
        }

        [Required]
        [StringLength(100)]
        public virtual string Name { get; set; }
        public virtual bool IsActive { get; set; }

        [Required]
        public virtual Department Department { get; set; }

        public virtual IList<Worker> Workers { get; set; }
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
