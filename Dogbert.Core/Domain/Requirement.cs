using System;
using NHibernate.Validator.Constraints;
using UCDArch.Core.DomainModel;
using UCDArch.Core.NHibernateValidator.Extensions;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Dogbert.Core.Domain
{
    public class Requirement : DomainObject
    {
        public Requirement()
        {
            SetDefaults();
        }

        public Requirement(string description, RequirementType requirementType, PriorityType priorityType, int technicalDifficulty, Project project, RequirementCategory category)
        {
            Description = description;
            RequirementType = requirementType;
            PriorityType = priorityType;
            TechnicalDifficulty = technicalDifficulty;
            Project = project;
            Category = category;
        }

        private void SetDefaults()
        {
            IsComplete = false;

            DateAdded = DateTime.Now;
            LastModified = DateTime.Now;
        }

        [Required]
        public virtual string Description { get; set; }
        [NotNull]
        public virtual RequirementType RequirementType { get; set; }
        [NotNull]
        public virtual PriorityType PriorityType { get; set; }
        public virtual int TechnicalDifficulty { get; set; }
        [NotNull]
        public virtual Project Project { get; set; }
        [NotNull]
        public virtual RequirementCategory Category { get; set; }
        public virtual bool IsComplete { get; set; }
        public virtual DateTime DateAdded { get; set; }
        public virtual DateTime LastModified { get; set; }
        public virtual string VersionCompleted { get; set; }
        public virtual IList<UseCase> UseCases { get; set; }

        public virtual string ShortDescription
        {
            get
            {
                 var len = 50;
                //using System.Text.RegularExpressions;
               // Regex regex = new Regex("<p>");
                //string result = regex.Replace(Description, " ");
                var result = Description.Replace("<p>", " ");
                result = result.Replace("</p>", " ");
                return result.Substring(0, Math.Min(len, result.Length));
            }
        }


    }
}