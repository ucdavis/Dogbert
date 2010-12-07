using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UCDArch.Core.DomainModel;

namespace Dogbert.Core.Domain
{
    public class ChangeLog : DomainObject
    {
        public ChangeLog()
        {
            Created = DateTime.Now;
            Updated = DateTime.Now;
        }

        public virtual string Change { get; set; }
        public virtual DateTime Created { get; set; }
        public virtual DateTime Updated { get; set; }
        public virtual Project Project { get; set; }
    }
}
