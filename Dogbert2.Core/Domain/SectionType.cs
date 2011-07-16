using System.ComponentModel.DataAnnotations;
using FluentNHibernate.Mapping;
using UCDArch.Core.DomainModel;

namespace Dogbert2.Core.Domain
{
    public class SectionType : DomainObjectWithTypedId<string>
    {
        public SectionType()
        {
            SetDefaults();
        }

        private void SetDefaults()
        {
            IsActive = true;
            IsSpecial = false;
            Order = 999;
        }

        [Required]
        [StringLength(50)]
        public virtual string Name { get; set; }
        [DataType(DataType.MultilineText)]
        public virtual string Description { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual int Order { get; set; }
        public virtual bool IsSpecial { get; set; }
    }

    public class TextTypeMap : ClassMap<SectionType>
    {
        public TextTypeMap()
        {
            Id(x => x.Id).Length(2);

            Map(x => x.Name);
            Map(x => x.Description);
            Map(x => x.IsActive);
            Map(x => x.Order).Column("`Order`");
            Map(x => x.IsSpecial);
        }
    }
}
