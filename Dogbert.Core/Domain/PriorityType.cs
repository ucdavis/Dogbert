using UCDArch.Core.DomainModel;

namespace Dogbert.Core.Domain
{
    public class PriorityType : DomainObject
    {
        public virtual string  Name { get; set; }
        public virtual bool IsActive { get; set; }
    }
}
