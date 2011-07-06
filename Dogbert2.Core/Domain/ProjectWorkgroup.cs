using System.ComponentModel.DataAnnotations;
using FluentNHibernate.Mapping;
using UCDArch.Core.DomainModel;

namespace Dogbert2.Core.Domain
{
    public class ProjectWorkgroup : DomainObject
    {
        [Required]
        public virtual Project Project { get; set; }
        [Required]
        public virtual Workgroup Workgroup { get; set; }
        public virtual int? Order { get; set; }
    }

    public class ProjectWorkgroupMap : ClassMap<ProjectWorkgroup>
    {
        public ProjectWorkgroupMap()
        {
            Table("ProjectsXWorkgroups");

            Id(x => x.Id);

            References(x => x.Project);
            References(x => x.Workgroup);
            Map(x => x.Order).Column("`Order`");
        }
    }
}
