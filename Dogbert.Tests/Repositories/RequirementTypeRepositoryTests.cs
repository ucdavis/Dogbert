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
    /// Entity Name:		RequirementType
    /// LookupFieldName:	Name
    /// </summary>
    [TestClass]
    public class RequirementTypeRepositoryTests : AbstractRepositoryTests<RequirementType, string>
    {
        /// <summary>
        /// Gets or sets the RequirementType repository.
        /// </summary>
        /// <value>The RequirementType repository.</value>
        public new IRepositoryWithTypedId<RequirementType, string> RequirementTypeRepository { get; set; }
		
        #region Init and Overrides

        /// <summary>
        /// Initializes a new instance of the <see cref="RequirementTypeRepositoryTests"/> class.
        /// </summary>
        public RequirementTypeRepositoryTests()
        {
            RequirementTypeRepository = new RepositoryWithTypedId<RequirementType, string>();
        }

        /// <summary>
        /// Gets the valid entity of type T
        /// </summary>
        /// <param name="counter">The counter.</param>
        /// <returns>A valid entity of type T</returns>
        protected override RequirementType GetValid(int? counter)
        {
            var localCount = (counter ?? 6).ToString();
            var rtValue = CreateValidEntities.RequirementType(counter);
            rtValue.setID(localCount);

            return rtValue;
        }

        /// <summary>
        /// A Query which will return a single record
        /// </summary>
        /// <param name="numberAtEnd"></param>
        /// <returns></returns>
        protected override IQueryable<RequirementType> GetQuery(int numberAtEnd)
        {
            return RequirementTypeRepository.Queryable.Where(a => a.Name.EndsWith(numberAtEnd.ToString()));
        }

        /// <summary>
        /// A way to compare the entities that were read.
        /// For example, this would have the assert.AreEqual("Comment" + counter, entity.Comment);
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="counter"></param>
        protected override void FoundEntityComparison(RequirementType entity, int counter)
        {
            Assert.AreEqual("Name" + counter, entity.Name);
        }

        /// <summary>
        /// Updates , compares, restores.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="action">The action.</param>
        protected override void UpdateUtility(RequirementType entity, ARTAction action)
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
            RequirementTypeRepository.DbContext.BeginTransaction();
            LoadRecords(5);
            RequirementTypeRepository.DbContext.CommitTransaction();
        }

        [TestMethod]
        public override void CanUpdateEntity()
        {
            CanUpdateEntity(false);
        }

        protected override void LoadRecords(int entriesToAdd)
        {
            EntriesAdded += entriesToAdd;
            for (int i = 0; i < entriesToAdd; i++)
            {
                var validEntity = GetValid(i + 1);
                RequirementTypeRepository.EnsurePersistent(validEntity, true);
            }
        }

        #endregion Init and Overrides		
		
        
    }
}