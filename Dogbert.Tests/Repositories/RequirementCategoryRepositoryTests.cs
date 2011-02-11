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
    /// Entity Name:		RequirementCategory
    /// LookupFieldName:	Name
    /// </summary>
    [TestClass]
    public class RequirementCategoryRepositoryTests : AbstractRepositoryTests<RequirementCategory, int>
    {
        /// <summary>
        /// Gets or sets the RequirementCategory repository.
        /// </summary>
        /// <value>The RequirementCategory repository.</value>
        public IRepository<RequirementCategory> RequirementCategoryRepository { get; set; }
		
        #region Init and Overrides

        /// <summary>
        /// Initializes a new instance of the <see cref="RequirementCategoryRepositoryTests"/> class.
        /// </summary>
        public RequirementCategoryRepositoryTests()
        {
            RequirementCategoryRepository = new Repository<RequirementCategory>();
        }

        /// <summary>
        /// Gets the valid entity of type T
        /// </summary>
        /// <param name="counter">The counter.</param>
        /// <returns>A valid entity of type T</returns>
        protected override RequirementCategory GetValid(int? counter)
        {
            var rtValue = CreateValidEntities.RequirementCategory(counter);
            rtValue.Project = Repository.OfType<Project>().GetNullableById(1);

            return rtValue;
        }

        /// <summary>
        /// A Query which will return a single record
        /// </summary>
        /// <param name="numberAtEnd"></param>
        /// <returns></returns>
        protected override IQueryable<RequirementCategory> GetQuery(int numberAtEnd)
        {
            return RequirementCategoryRepository.Queryable.Where(a => a.Name.EndsWith(numberAtEnd.ToString()));
        }

        /// <summary>
        /// A way to compare the entities that were read.
        /// For example, this would have the assert.AreEqual("Comment" + counter, entity.Comment);
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="counter"></param>
        protected override void FoundEntityComparison(RequirementCategory entity, int counter)
        {
            Assert.AreEqual("Name" + counter, entity.Name);
        }

        /// <summary>
        /// Updates , compares, restores.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="action">The action.</param>
        protected override void UpdateUtility(RequirementCategory entity, ARTAction action)
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
            LoadProjects(1);
            RequirementCategoryRepository.DbContext.BeginTransaction();
            LoadRecords(5);
            RequirementCategoryRepository.DbContext.CommitTransaction();
        }

        #endregion Init and Overrides		
		
        
    }
}