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
    /// Entity Name:		PriorityType
    /// LookupFieldName:	Name
    /// </summary>
    [TestClass]
    public class PriorityTypeRepositoryTests : AbstractRepositoryTests<PriorityType, int>
    {
        /// <summary>
        /// Gets or sets the PriorityType repository.
        /// </summary>
        /// <value>The PriorityType repository.</value>
        public IRepository<PriorityType> PriorityTypeRepository { get; set; }
		
        #region Init and Overrides

        /// <summary>
        /// Initializes a new instance of the <see cref="PriorityTypeRepositoryTests"/> class.
        /// </summary>
        public PriorityTypeRepositoryTests()
        {
            PriorityTypeRepository = new Repository<PriorityType>();
        }

        /// <summary>
        /// Gets the valid entity of type T
        /// </summary>
        /// <param name="counter">The counter.</param>
        /// <returns>A valid entity of type T</returns>
        protected override PriorityType GetValid(int? counter)
        {
            return CreateValidEntities.PriorityType(counter);
        }

        /// <summary>
        /// A Query which will return a single record
        /// </summary>
        /// <param name="numberAtEnd"></param>
        /// <returns></returns>
        protected override IQueryable<PriorityType> GetQuery(int numberAtEnd)
        {
            return PriorityTypeRepository.Queryable.Where(a => a.Name.EndsWith(numberAtEnd.ToString()));
        }

        /// <summary>
        /// A way to compare the entities that were read.
        /// For example, this would have the assert.AreEqual("Comment" + counter, entity.Comment);
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="counter"></param>
        protected override void FoundEntityComparison(PriorityType entity, int counter)
        {
            Assert.AreEqual("Name" + counter, entity.Name);
        }

        /// <summary>
        /// Updates , compares, restores.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="action">The action.</param>
        protected override void UpdateUtility(PriorityType entity, ARTAction action)
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
            PriorityTypeRepository.DbContext.BeginTransaction();
            LoadRecords(5);
            PriorityTypeRepository.DbContext.CommitTransaction();
        }

        #endregion Init and Overrides		
		
        
    }
}