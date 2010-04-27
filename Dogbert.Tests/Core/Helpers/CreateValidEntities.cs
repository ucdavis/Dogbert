using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dogbert.Core.Abstractions;
using Dogbert.Core.Domain;

namespace Dogbert.Tests.Core.Helpers
{
    public static class CreateValidEntities
    {
        #region Catbert Classes
        public static User User(int? count)
        {
            var rtValue = new User();
            rtValue.FirstName = "FirstName" + count.Extra();
            rtValue.LastName = "LastName" + count.Extra();
            rtValue.LoginIDs = new List<Login>();
            rtValue.Units = new List<Unit>();

            return rtValue;
        }

        public static Login Login(int? count)
        {
            var rtValue = new Login();
            rtValue.User = new User();

            return rtValue;
        }
        public static Unit Unit(int? count)
        {
            var rtValue = new Unit();
            rtValue.ShortName = "ShortName" + count.Extra();

            return rtValue;
        }
        #endregion Catbert Classes

        #region Lookups

        public static Status Status(int? count)
        {
            var rtValue = new Status();
            rtValue.Name = "Name" + count.Extra();
            rtValue.IsActive = true;

            return rtValue;
        }
        public static PersonType PersonType(int? count)
        {
            var rtValue = new PersonType();
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
        public static Actor Actor(int? count)
        {
            var rtValue = new Actor();
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
        public static FileType FileType(int? count)
        {
            var rtValue = new FileType();
            rtValue.Name = "Name" + count.Extra();
            rtValue.IsActive = true;

            return rtValue;
        }

        #endregion Lookups

        public static Person Person(int? count)
        {
            var rtValue = new Person();
            rtValue.User = new User();
            rtValue.PersonType = new PersonType();

            return rtValue;
        }

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

        public static ProjectFile ProjectFile(int? count)
        {
            var rtValue = new ProjectFile();
            rtValue.FileName = "FileName" + count.Extra();
            rtValue.Type = new FileType();
            rtValue.Project = new Project();
            rtValue.DateAdded = SystemTime.Now();
            rtValue.FileContents = new byte[] {1,2,3};

            return rtValue;
        }

        public static ProjectText ProjectText(int? count)
        {
            var rtValue = new ProjectText();
            rtValue.Text = "Text" + count.Extra();
            rtValue.TextType = new TextType();
            rtValue.Project = new Project();

            return rtValue;
        }

        public static ProjectType ProjectType(int? count)
        {
            var rtValue = new ProjectType();
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
            rtValue.DateAdded = SystemTime.Now();
            rtValue.LastModified = SystemTime.Now();

            return rtValue;
        }

        public static RequirementCategory RequirementCategory(int? count)
        {
            var rtValue = new RequirementCategory();
            rtValue.Name = "Name" + count.Extra();
            rtValue.IsActive = true;
            rtValue.Project = new Project();

            return rtValue;
        }

        public static RequirementType RequirementType(int? count)
        {
            var rtValue = new RequirementType();
            rtValue.Name = "Name" + count.Extra();
            rtValue.IsActive = true;

            return rtValue;
        }

        public static StatusCode StatusCode(int? count)
        {
            var rtValue = new StatusCode();
            rtValue.Name = "Name" + count.Extra();
            rtValue.IsActive = true;

            return rtValue;
        }

        public static TextType TextType(int? count)
        {
            var rtValue = new TextType();
            rtValue.Name = "Name" + count.Extra();
            rtValue.IsActive = true;

            return rtValue;
        }

        public static UseCase UseCase(int? count)
        {
            var rtValue = new UseCase();
            rtValue.Name = "Name" + count.Extra();
            rtValue.Description = "Description" + count.Extra();
            rtValue.DateAdded = SystemTime.Now();
            rtValue.LastModified = SystemTime.Now();
            rtValue.Project = new Project();
            rtValue.RequirementCategory = new RequirementCategory();
            rtValue.Precondition = "PreCondition" + count.Extra();
            rtValue.Postcondition = "PostCondition" + count.Extra();
            rtValue.Steps = new List<UseCaseStep>();
            rtValue.Actors = new List<Actor>();
            rtValue.Requirements = new List<Requirement>();

            return rtValue;
        }

        public static UseCaseStep UseCaseStep(int? count)
        {
            var rtValue = new UseCaseStep();
            rtValue.Description = "Description" + count.Extra();

            return rtValue;
        }

        public static Worker Worker(int? count)
        {
            var rtValue = new Worker();
            rtValue.IsActive = true;
            rtValue.User = new User();

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


