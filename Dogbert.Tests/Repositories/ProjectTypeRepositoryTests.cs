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
    /// Entity Name:		ProjectType
    /// LookupFieldName:	Name
    /// </summary>
    [TestClass]
    public class ProjectTypeRepositoryTests : AbstractRepositoryTests<ProjectType, string>
    {
        /// <summary>
        /// Gets or sets the ProjectType repository.
        /// </summary>
        /// <value>The ProjectType repository.</value>
        public IRepositoryWithTypedId<ProjectType, string> ProjectTypeRepository { get; set; }
		
        #region Init and Overrides

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectTypeRepositoryTests"/> class.
        /// </summary>
        public ProjectTypeRepositoryTests()
        {
            ProjectTypeRepository = new RepositoryWithTypedId<ProjectType, string>();
        }

        /// <summary>
        /// Gets the valid entity of type T
        /// </summary>
        /// <param name="counter">The counter.</param>
        /// <returns>A valid entity of type T</returns>
        protected override ProjectType GetValid(int? counter)
        {
            var localCount = (counter ?? 6).ToString();
            var rtValue = CreateValidEntities.ProjectType(counter);
            rtValue.setID(localCount);

            return rtValue;
        }

        /// <summary>
        /// A Query which will return a single record
        /// </summary>
        /// <param name="numberAtEnd"></param>
        /// <returns></returns>
        protected override IQueryable<ProjectType> GetQuery(int numberAtEnd)
        {
            return ProjectTypeRepository.Queryable.Where(a => a.Name.EndsWith(numberAtEnd.ToString()));
        }

        /// <summary>
        /// A way to compare the entities that were read.
        /// For example, this would have the assert.AreEqual("Comment" + counter, entity.Comment);
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="counter"></param>
        protected override void FoundEntityComparison(ProjectType entity, int counter)
        {
            Assert.AreEqual("Name" + counter, entity.Name);
        }

        /// <summary>
        /// Updates , compares, restores.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="action">The action.</param>
        protected override void UpdateUtility(ProjectType entity, ARTAction action)
        {
            const string updateValue = "Updated";
            switch (action)
            {
                case ARTAction.Compare:
                    Assert.AreEqual(updateValue, entity.Name);
                    break;
                case ARTAction.Restore:
                    entity.Name = RestoreValue;
                    break;
                case ARTAction.Update:
                    RestoreValue = entity.Name;
                    entity.Name = updateValue;
                    break;
            }
        }

        /// <summary>
        /// Loads the data.
        /// </summary>
        protected override void LoadData()
        {
            ProjectTypeRepository.DbContext.BeginTransaction();
            LoadRecords(5);
            ProjectTypeRepository.DbContext.CommitTransaction();
        }

        protected override void LoadRecords(int entriesToAdd)
        {
            EntriesAdded += entriesToAdd;
            for (int i = 0; i < entriesToAdd; i++)
            {
                var validEntity = GetValid(i + 1);
                Repository.OfType<ProjectType>().EnsurePersistent(validEntity, true);
            }
        }

        /// <summary>
        /// Determines whether this instance [can update entity].
        /// Defaults to true unless overridden
        /// This is muteable = "0" (Can't update)
        /// </summary>
        [TestMethod]
        public override void CanUpdateEntity()
        {
            CanUpdateEntity(false);
        }

        #endregion Init and Overrides		
		
        
    }
}