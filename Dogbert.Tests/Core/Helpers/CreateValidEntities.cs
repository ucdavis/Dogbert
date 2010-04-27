using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dogbert.Core.Domain;

namespace Dogbert.Tests.Core.Helpers
{
    public static class CreateValidEntities
    {
        public static Project Project(int? count)
        {
            var rtValue = new Project();
            rtValue.Name = "Name" + count.Extra();
            rtValue.Contact = "Contact" + count.Extra();
            rtValue.ContactEmail = "ContactEmail@test.edu" + count.Extra();
            rtValue.Unit = "Unit" + count.Extra();
            rtValue.ProjectTexts = new List<ProjectText>();
            rtValue.RequirementCategories = new List<RequirementCategory>();
            rtValue.Requirements = new List<Requirement>();
            rtValue.UseCases = new List<UseCase>();

            return rtValue;
        }
        public static RequirementType RequirementType(int? count)
        {
            var rtValue = new RequirementType();
            rtValue.Name = "Name" + count.Extra();
            rtValue.IsActive = true;

            return rtValue;
        }

        public static Category Category(int? count)
        {
            var rtValue = new Category();
            rtValue.Name = "Name" + count.Extra();
            rtValue.IsActive = true;

            return rtValue;
        }

        public static PriorityType PriorityType(int? count)
        {
            var rtValue = new PriorityType();
            rtValue.Name = "Name" + count.Extra();
            rtValue.IsActive = true;

            return rtValue;
        }

        public static Requirement Requirement(int? count)
        {
            var rtValue = new Requirement();
            rtValue.Description = "Description" + count.Extra();
            rtValue.RequirementType = new RequirementType();
            rtValue.PriorityType = new PriorityType();
            rtValue.Project = new Project();
            rtValue.Category = new Category();
            rtValue.DateAdded = DateTime.Now;
            rtValue.LastModified = DateTime.Now;

            return rtValue;
        }

        #region Helper Extension

        private static string Extra(this int? counter)
        {
            var extraString = "";
            if (counter != null)
            {
                extraString = counter.ToString();
            }
            return extraString;
        }

        #endregion Helper Extension

        
    }
}


