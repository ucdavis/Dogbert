using System.ComponentModel.DataAnnotations;
using FluentNHibernate.Mapping;
using UCDArch.Core.DomainModel;

namespace Dogbert2.Core.Domain
{
    public class WorkgroupWorker : DomainObject
    {
        [Required]
        public virtual Workgroup Workgroup { get; set; }
        [Required]
        public virtual Worker Worker { get; set; }

        public virtual bool Limited { get; set; }
    }

    public class WorkgroupWorkerMap : ClassMap<WorkgroupWorker>
    {
        public WorkgroupWorkerMap()
        {
            Table("WorkGroupsXWorkers");

            Id(x => x.Id);

            References(x => x.Workgroup);
            References(x => x.Worker);
            Map(x => x.Limited);

        }
    }
}
