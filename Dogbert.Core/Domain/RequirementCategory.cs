using NHibernate.Validator.Constraints;
using UCDArch.Core.DomainModel;
using UCDArch.Core.NHibernateValidator.Extensions;

namespace Dogbert.Core.Domain
{
    public class RequirementCategory : DomainObject
    {
        public RequirementCategory()
        {
            SetDefaults();
        }

        public RequirementCategory(string name)
        {
            Name = name;
        }

        private void SetDefaults()
        {
            IsActive = true;
        }

        [Length(50)]
        [Required]
        public virtual string Name { get; set; }

        public virtual bool IsActive { get; set; }
        [NotNull]
        public virtual Project Project { get; set; }
    }
}
