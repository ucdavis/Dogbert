using FluentNHibernate.Mapping;
using UCDArch.Core.DomainModel;

namespace Dogbert2.Core.Domain
{
    public class ProjectWorkgroups : DomainObject
    {
        public virtual Project Project { get; set; }
        public virtual Workgroup Workgroup { get; set; }
        public virtual int? Order { get; set; }
    }

    public class ProjectWorkgroupsMap : ClassMap<ProjectWorkgroups>
    {
        public ProjectWorkgroupsMap()
        {
            Table("ProjectsXWorkgroups");

            Id(x => x.Id);

            References(x => x.Project);
            References(x => x.Workgroup);
            Map(x => x.Order);
        }
    }
}
