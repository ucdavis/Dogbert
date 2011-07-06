using System.ComponentModel.DataAnnotations;
using FluentNHibernate.Mapping;
using UCDArch.Core.DomainModel;

namespace Dogbert2.Core.Domain
{
    public class ProjectType : DomainObjectWithTypedId<string>
    {
        [Required]
        [StringLength(50)]
        public virtual string Name { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual int Order { get; set; }
    }

    public class ProjectTypeMap : ClassMap<ProjectType>
    {
        public ProjectTypeMap()
        {
            Id(x => x.Id);

            Map(x => x.Name);
            Map(x => x.IsActive);
            Map(x => x.Order).Column("`Order`");
        }
    }
}
