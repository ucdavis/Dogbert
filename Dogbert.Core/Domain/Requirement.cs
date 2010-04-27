using NHibernate.Validator.Constraints;

namespace Dogbert.Core.Domain
{
    public class Requirement : RequirementBase
    {
        public Requirement()
        {
            Name = string.Empty;
        }

        [Range(0, 10)]
        public virtual int TechnicalDifficulty { get; set; }

        public virtual bool IsComplete { get; set; }
    }
}