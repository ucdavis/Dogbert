using UCDArch.Core.DomainModel;

namespace Dogbert.Core.Domain
{
    public class Person : DomainObject
    {
        public virtual User User { get; set; }
        public virtual PersonType PersonType { get; set; }

        public virtual string FullName
        {
            get { return User.FullName; }
        }

        public virtual int UserID
        {
            get { return User.Id; }
        }
    }
}