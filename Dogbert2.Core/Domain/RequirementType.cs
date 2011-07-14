using System.ComponentModel.DataAnnotations;
using FluentNHibernate.Mapping;
using UCDArch.Core.DomainModel;

namespace Dogbert2.Core.Domain
{
    public class RequirementType : DomainObject
    {
        public RequirementType()
        {
            SetDefaults();
        }

        public RequirementType(string name)
        {
            Name = name;

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
    }

    public class RequirementTypeMap : ClassMap<RequirementType>
    {
        public RequirementTypeMap()
        {
            Id(x => x.Id);

            Map(x => x.Name);
            Map(x => x.IsActive);
        }
    }
}
