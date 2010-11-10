using System;
using System.Collections.Generic;
using Dogbert.Core.Abstractions;
using Dogbert.Core.Domain;
using UCDArch.Core.PersistanceSupport;
using Check = UCDArch.Core.Utils.Check;
using System.Linq;


namespace Dogbert.Controllers.Helpers
{
    public class Copiers
    {
        /// <summary>
        /// Copies the requirement.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <returns></returns>
        public static Requirement CopyRequirement(Requirement source, Requirement destination)
        {
            destination.Description = source.Description;
            destination.RequirementType = source.RequirementType;
            destination.PriorityType = source.PriorityType;
            destination.TechnicalDifficulty = source.TechnicalDifficulty;
            destination.Category = source.Category;
            //destination.Project = source.Project;
            destination.IsComplete = source.IsComplete;
            //destination.DateAdded = source.DateAdded;
            destination.LastModified = SystemTime.Now();
            destination.VersionCompleted = source.VersionCompleted;

            return destination;
        }

        /// <summary>
        /// Copies the project file.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <returns></returns>
        public static ProjectFile CopyProjectFile(ProjectFile source, ProjectFile destination)
        {
            //destination.DateAdded = source.DateAdded;
            destination.DateChanged = SystemTime.Now();
            //destination.FileContents = source.FileContents;   //Grabbed from the Request
            //destination.FileName = source.FileName;           //Grabbed from the Request
            //destination.Project = source.Project;
            // destination.FileContentType = source.FileContentType; //Grabbed from the Request
            destination.Type = source.Type;

            return destination;
        }
    }
}
