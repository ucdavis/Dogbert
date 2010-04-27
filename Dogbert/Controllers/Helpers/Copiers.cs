using System;
using System.Collections.Generic;
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
            //TODO: Comment out/remove fields that we don't want updated.
            destination.Description = source.Description;
            destination.RequirementType = source.RequirementType;
            destination.PriorityType = source.PriorityType;
            destination.TechnicalDifficulty = source.TechnicalDifficulty;
            destination.Category = source.Category;
            //destination.Project = source.Project;
            destination.IsComplete = source.IsComplete;
            //destination.DateAdded = source.DateAdded;
            destination.LastModified = DateTime.Now;

            return destination;
        }
    }
}
