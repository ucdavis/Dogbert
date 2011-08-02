using System;
using System.ComponentModel.DataAnnotations;
using FluentNHibernate.Mapping;
using UCDArch.Core.DomainModel;

namespace Dogbert2.Core.Domain
{
    public class Task : DomainObject
    {
        public Task()
        {
            Complete = false;

            DateCreated = DateTime.Now;
            LastUpdate = DateTime.Now;
        }

        [StringLength(5)]
        public virtual string TaskId { get; set; }

        [DataType(DataType.MultilineText)]
        [Required]
        public virtual string Description { get; set; }
        [DataType(DataType.MultilineText)]
        public virtual string Comments { get; set; }
        public virtual bool Complete { get; set; }

        [Required]
        public virtual Project Project { get; set; }
        [Required]
        public virtual Requirement Requirement { get; set; }
        [Required]
        public virtual Worker Worker { get; set; }

        public virtual DateTime DateCreated { get; set; }
        public virtual DateTime LastUpdate { get; set; }
    }

    public class TaskMap : ClassMap<Task>
    {
        public TaskMap()
        {
            Id(x => x.Id);

            Map(x => x.TaskId);
            Map(x => x.Description);
            Map(x => x.Comments);
            Map(x => x.Complete);

            References(x => x.Project);
            References(x => x.Requirement);
            References(x => x.Worker);

            Map(x => x.DateCreated);
            Map(x => x.LastUpdate);
        }
    }
}
