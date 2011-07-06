using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Dogbert2.Filters
{
    public class UserOnlyAttribute : AuthorizeAttribute
    {
        public UserOnlyAttribute()
        {
            Roles = RoleNames.User;
        }
    }

    public class AdminOnlyAttribute : AuthorizeAttribute
    {
        public AdminOnlyAttribute()
        {
            Roles = RoleNames.Admin;
        }
    }

    public class AllRolesAttribute : AuthorizeAttribute
    {
        public AllRolesAttribute()
        {
            Roles = RoleNames.User + "," + RoleNames.Admin;
        }
    }

    public static class RoleNames
    {
        public static readonly string User = "User";
        public static readonly string Admin = "Admin";
    }
}