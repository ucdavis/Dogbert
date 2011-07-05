using System;
using System.ComponentModel.DataAnnotations;
using FluentNHibernate.Mapping;
using UCDArch.Core.DomainModel;

namespace Dogbert2.Core.Domain
{
    public class Project : DomainObject
    {
        public Project()
        {
            SetDefaults();
        }

        private void SetDefaults()
        {
            DateAdded = DateTime.Now;
            LastUpdate = DateTime.Now;
        }

        [Required]
        [StringLength(50)]
        public virtual string Name { get; set; }
        public virtual char Priority { get; set; }
        public virtual DateTime? Deadline { get; set; }
        public virtual DateTime? ProjectedBegin { get; set; }
        public virtual DateTime? Begin { get; set; }
        public virtual DateTime? ProjectedEnd { get; set; }
        public virtual DateTime? End { get; set; }
        public virtual Worker ProjectManager { get; set; }
        public virtual Worker LeadProgrammer { get; set; }

        // Client information
        [Required]
        [StringLength(100)]
        public virtual string Contact { get; set; }
        [StringLength(100)]
        [DataType(DataType.EmailAddress)]
        public virtual string ContactEmail { get; set; }
        [Required]
        [StringLength(50)]
        public virtual string Unit { get; set; }

        // tracking information
        [Required]
        public virtual StatusCode StatusCode { get; set; }
        public virtual DateTime DateAdded { get; set; }
        public virtual DateTime LastUpdate { get; set; }
    }

    public class ProjectMap : ClassMap<Project>
    {
        public ProjectMap()
        {
            Id(x => x.Id);

            Map(x => x.Name);
            Map(x => x.Priority);
            Map(x => x.Deadline);
            Map(x => x.ProjectedBegin);
            Map(x => x.Begin).Column("`Begin`");
            Map(x => x.ProjectedEnd);
            Map(x => x.End).Column("`End`");
            References(x => x.ProjectManager).Column("ProjectManagerId");
            References(x => x.LeadProgrammer).Column("LeadProgrammerId");

            Map(x => x.Contact);
            Map(x => x.ContactEmail);
            Map(x => x.Unit);

            References(x => x.StatusCode);
            Map(x => x.DateAdded);
            Map(x => x.LastUpdate);
        }
    }
}
