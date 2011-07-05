using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FluentNHibernate.Mapping;
using UCDArch.Core.DomainModel;

namespace Dogbert2.Core.Domain
{
    public class Worker : DomainObjectWithTypedId<string>
    {
        public Worker()
        {
            SetDefaults();
        }

        public Worker(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;

            SetDefaults();
        }

        private void SetDefaults()
        {
            IsActive = true;
        }

        [Required]
        [StringLength(50)]
        public virtual string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public virtual string LastName { get; set; }
        public virtual bool IsActive { get; set; }

        public virtual IList<Workgroup> Workgroups { get; set; }
    }

    public class WorkerMap : ClassMap<Worker>
    {
        public WorkerMap()
        {
            Id(x => x.Id).Length(10);

            Map(x => x.FirstName);
            Map(x => x.LastName);
            Map(x => x.IsActive);

            HasManyToMany(x => x.Workgroups)
                .ParentKeyColumn("WorkerId").ChildKeyColumn("WorkgroupId")
                .Table("WorkgroupsXWorkers").Cascade.SaveUpdate();
        }
    }
}
