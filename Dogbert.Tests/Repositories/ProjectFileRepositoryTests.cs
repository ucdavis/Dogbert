using System.Linq;
using Dogbert.Core.Domain;
using Dogbert.Tests.Core;
using Dogbert.Tests.Core.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UCDArch.Core.PersistanceSupport;
using UCDArch.Data.NHibernate;

namespace Dogbert.Tests.Repositories
{
    /// <summary>
    /// Entity Name:		ProjectFile
    /// LookupFieldName:	FileName
    /// </summary>
    [TestClass]
    public class ProjectFileRepositoryTests : AbstractRepositoryTests<ProjectFile, int>
    {
        /// <summary>
        /// Gets or sets the ProjectFile repository.
        /// </summary>
        /// <value>The ProjectFile repository.</value>
        public IRepository<ProjectFile> ProjectFileRepository { get; set; }
		
        #region Init and Overrides

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectFileRepositoryTests"/> class.
        /// </summary>
        public ProjectFileRepositoryTests()
        {
            ProjectFileRepository = new Repository<ProjectFile>();
        }

        /// <summary>
        /// Gets the valid entity of type T
        /// </summary>
        /// <param name="counter">The counter.</param>
        /// <returns>A valid entity of type T</returns>
        protected override ProjectFile GetValid(int? counter)
        {
            var rtValue = CreateValidEntities.ProjectFile(counter);
            rtValue.Project = Repository.OfType<Project>().GetNullableByID(1);
            rtValue.Type = Repository.OfType<FileType>().GetNullableByID(1);
            return rtValue;
        }

        /// <summary>
        /// A Query which will return a single record
        /// </summary>
        /// <param name="numberAtEnd"></param>
        /// <returns></returns>
        protected override IQueryable<ProjectFile> GetQuery(int numberAtEnd)
        {
            return ProjectFileRepository.Queryable.Where(a => a.FileName.EndsWith(numberAtEnd.ToString()));
        }

        /// <summary>
        /// A way to compare the entities that were read.
        /// For example, this would have the assert.AreEqual("Comment" + counter, entity.Comment);
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="counter"></param>
        protected override void FoundEntityComparison(ProjectFile entity, int counter)
        {
            Assert.AreEqual("FileName" + counter, entity.FileName);
        }

        /// <summary>
        /// Updates , compares, restores.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="action">The action.</param>
        protected override void UpdateUtility(ProjectFile entity, ARTAction action)
        {
            const string updateValue = "Updated";
            switch (action)
            {
                case ARTAction.Compare:
                    Assert.AreEqual(updateValue, entity.FileName);
                    break;
                case ARTAction.Restore:
                    entity.FileName = RestoreValue;
                    break;
                case ARTAction.Update:
                    RestoreValue = entity.FileName;
                    entity.FileName = updateValue;
                    break;
            }
        }

        /// <summary>
        /// Loads the data.
        /// </summary>
        protected override void LoadData()
        {
            LoadProjects(1);
            LoadFileTypes(1);
            ProjectFileRepository.DbContext.BeginTransaction();
            LoadRecords(5);
            ProjectFileRepository.DbContext.CommitTransaction();
        }

        #endregion Init and Overrides		
		
        
    }
}