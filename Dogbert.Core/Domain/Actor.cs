using System.Collections.Generic;
using UCDArch.Core.DomainModel;

namespace Dogbert.Core.Domain
{
    public class Actor : DomainObject
    {
        public virtual string Name { get; set; }
        public virtual bool IsActive{ get; set; }
        public virtual IList<UseCase> UseCases {get; set;}
    }
}
