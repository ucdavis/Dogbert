using System;
using System.Collections.Generic;
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

            Requirements = new List<Requirement>();
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
        [Display(Name="Category")]
        public virtual RequirementCategory RequirementCategory { get; set; }
        public virtual Worker Worker { get; set; }

        public virtual DateTime DateCreated { get; set; }
        public virtual DateTime LastUpdate { get; set; }

        public virtual IList<Requirement> Requirements { get; set; }
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
            References(x => x.RequirementCategory);
            References(x => x.Worker);

            Map(x => x.DateCreated);
            Map(x => x.LastUpdate);

            HasManyToMany(x => x.Requirements).ParentKeyColumn("TaskId").ChildKeyColumn("RequirementId").Table("TasksXRequirements").Cascade.SaveUpdate();
        }
    }
}
