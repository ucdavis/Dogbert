using System.Collections.Generic;

namespace Dogbert.Core.Domain
{
    public class UseCase : RequirementBase
    {
        public virtual IList<UseCaseStep> Steps { get; set; }
    }
}