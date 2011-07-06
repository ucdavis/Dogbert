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
            IsActive = true;

            WorkgroupWorkers = new List<WorkgroupWorker>();
            ProjectWorkgroups = new List<ProjectWorkgroup>();
        }

        [Required]
        [StringLength(100)]
        public virtual string Name { get; set; }
        public virtual bool IsActive { get; set; }

        [Required]
        public virtual Department Department { get; set; }

        public virtual IList<WorkgroupWorker> WorkgroupWorkers { get; set; }
        public virtual IList<ProjectWorkgroup> ProjectWorkgroups { get; set; }

    }

    public class WorkgroupMap : ClassMap<Workgroup>
    {
        public WorkgroupMap()
        {
            Id(x => x.Id);

            Map(x => x.Name);
            Map(x => x.IsActive);

            References(x => x.Department);

            //HasManyToMany(x => x.Workers)
            //    .ParentKeyColumn("WorkgroupId").ChildKeyColumn("WorkerId")
            //    .Table("WorkgroupsXWorkers").Cascade.SaveUpdate();

            HasMany(x => x.WorkgroupWorkers).Inverse();
            HasMany(x => x.ProjectWorkgroups).Inverse();
        }
    }
}
