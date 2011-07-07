using System.ComponentModel.DataAnnotations;
using FluentNHibernate.Mapping;
using UCDArch.Core.DomainModel;

namespace Dogbert2.Core.Domain
{
    public class ProjectTerm : DomainObject
    {
        [Required]
        [StringLength(50)]
        public virtual string Term { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public virtual string Definition { get; set; }
        [StringLength(50)]
        public virtual string Src { get; set; }

        [Required]
        public virtual Project Project { get; set; }
    }

    public class ProjectTermMap : ClassMap<ProjectTerm>
    {
        public ProjectTermMap()
        {
            Id(x => x.Id);

            Map(x => x.Term);
            Map(x => x.Definition);
            Map(x => x.Src);

            References(x => x.Project);
        }
    }
}
