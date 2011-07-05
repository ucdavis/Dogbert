using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dogbert2.Core.Domain;

namespace Dogbert2.Clients
{
    public interface IDepartmentClient
    {
        IList<Department> GetAvailable();
    }
}
