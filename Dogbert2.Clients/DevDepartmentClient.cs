using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dogbert2.Core.Domain;

namespace Dogbert2.Clients
{
    public class DevDepartmentClient : IDepartmentClient
    {
        public IList<Department> GetAvailable()
        {
            var depts = new List<Department>();

            depts.Add(new Department("ADNO", "CA&ES Dean's Office"));
            depts.Add(new Department("AANS", "Animal Science"));
            depts.Add(new Department("AENT", "Entomology" ));
            depts.Add(new Department("LAWR", "Land, Air & Water Resources" ));
            depts.Add(new Department("CLAS", "L&S Dean's Office" ));
            depts.Add(new Department("STAM", "Statistics" ));
            depts.Add(new Department("GELM", "Geology" ));

            return depts.OrderBy(a => a.Name).ToList();
        }
    }
}
