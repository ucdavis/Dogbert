using System;
using System.Collections.Generic;
using UCDArch.Core.DomainModel;

namespace Dogbert.Core.Domain
{
    public class User : DomainObject
    {
        public virtual IList<Login> LoginIDs { get; set; }
        public virtual IList<Unit> Units { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string EmployeeID { get; set; }
        public virtual string StudentID { get; set; }
        public virtual string SID { get; set; }
        public virtual Guid UserKey { get; set; }
        public virtual bool Inactive { get; set; }

        public virtual string FullName
        {
            get { return FirstName + " " + LastName; }
        }

        public virtual string FullNameLastNameFirst
        {
            get { return LastName + ", " + FirstName; }
        }

        public static List<string> FindUCDKerberosIDs(string nameToMatch)
        {
            throw new NotImplementedException();
        }
    }

    public class Login : DomainObjectWithTypedId<string>
    {
        public virtual User User { get; set; }
    }

    public class Unit : DomainObjectWithTypedId<string>
    {
        public virtual string ShortName { get; set; }
        public virtual string FullName { get; set; }
        public virtual string PPSCode { get; set; }
        public virtual string FISCode { get; set; }
        public virtual int UnitID { get; set; }
    }
}