using System;
using System.ComponentModel.DataAnnotations;
using FluentNHibernate.Mapping;
using UCDArch.Core.DomainModel;

namespace Dogbert2.Core.Domain
{
    public class ProjectSection : DomainObject
    {
        public ProjectSection()
        {
            DateCreated = DateTime.Now;
            LastUpdate = DateTime.Now;
        }

        [Required]
        [DataType(DataType.MultilineText)]
        public virtual string Text { get; set; }
        [Required]
        public virtual Project Project { get; set; }
        [Required]
        [Display(Name = "Text Type")]
        public virtual SectionType SectionType { get; set; }

        public virtual DateTime DateCreated{ get; set; }
        public virtual DateTime LastUpdate { get; set; }
    }

    public class ProjectTextMap : ClassMap<ProjectSection>
    {
        public ProjectTextMap()
        {
            Id(x => x.Id);

            Map(x => x.Text);
            References(x => x.Project);
            References(x => x.SectionType);

            Map(x => x.DateCreated);
            Map(x => x.LastUpdate);
        }
    }
}
