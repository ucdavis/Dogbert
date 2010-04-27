using UCDArch.Core.DomainModel;

namespace Dogbert.Core.Domain
{
    public class UseCaseStep : DomainObject
    {
        public virtual string Name { get; set; }
        public virtual UseCase UseCase { get; set; }
        public virtual int Order { get; set; }
    }
}