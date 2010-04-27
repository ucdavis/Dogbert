using UCDArch.Core.DomainModel;

namespace Dogbert.Core.Domain
{
    public class ProjectType :DomainObjectWithTypedId<string>
    {
        public virtual string Name { get; set; }
        public virtual bool IsActive { get; set; }

        public virtual void setID(string id)
        {
            this.Id = id;
        }
    
    }
}
