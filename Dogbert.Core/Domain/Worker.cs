using UCDArch.Core.DomainModel;

namespace Dogbert.Core.Domain
{
    public class Worker : DomainObject
    {
        public Worker()
        {
            this.IsActive = true;
        }

        public virtual User User { get; set; }
        public virtual bool IsActive { get; set; }

        public virtual string FullName { get { return this.User.FullName; } }
        public virtual int UserID { get { return this.User.Id; }}
    }
}
