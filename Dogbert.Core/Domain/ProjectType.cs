using System;
using System.Collections.Generic;
using NHibernate.Validator.Constraints;
using UCDArch.Core.DomainModel;
using UCDArch.Core.NHibernateValidator.Extensions;

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
