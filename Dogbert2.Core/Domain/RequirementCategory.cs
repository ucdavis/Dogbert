using System.ComponentModel.DataAnnotations;
using FluentNHibernate.Mapping;
using UCDArch.Core.DomainModel;

namespace Dogbert2.Core.Domain
{
    public class RequirementCategory : DomainObject
    {
        public RequirementCategory()
        {
            SetDefaults();
        }

        public RequirementCategory(string name, Project project)
        {
            Name = name;
            Project = project;

            SetDefaults();
        }

        private void SetDefaults()
        {
            IsActive = true;
        }

        [StringLength(50)]
        [Required]
        public virtual string Name { get; set; }
        public virtual bool IsActive { get; set; }
        [Required]
        public virtual Project Project { get; set; }
    }

    public class RequirementCategoryMap : ClassMap<RequirementCategory>
    {
        public RequirementCategoryMap()
        {
            Id(x => x.Id);

            Map(x => x.Name);
            Map(x => x.IsActive);
            References(x => x.Project);
        }
    }
}
