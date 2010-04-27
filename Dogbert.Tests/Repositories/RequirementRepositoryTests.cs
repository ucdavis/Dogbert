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
    /// Entity Name:		Requirement
    /// LookupFieldName:	Description
    /// </summary>
    [TestClass]
    public class RequirementRepositoryTests : AbstractRepositoryTests<Requirement, int>
    {
        /// <summary>
        /// Gets or sets the Requirement repository.
        /// </summary>
        /// <value>The Requirement repository.</value>
        public IRepository<Requirement> RequirementRepository { get; set; }
        
		
        #region Init and Overrides

        /// <summary>
        /// Initializes a new instance of the <see cref="RequirementRepositoryTests"/> class.
        /// </summary>
        public RequirementRepositoryTests()
        {
            RequirementRepository = new Repository<Requirement>();
        }

        /// <summary>
        /// Gets the valid entity of type T
        /// </summary>
        /// <param name="counter">The counter.</param>
        /// <returns>A valid entity of type T</returns>
        protected override Requirement GetValid(int? counter)
        {
            var rtValue = CreateValidEntities.Requirement(counter);
            rtValue.RequirementType = RequirementTypeRepository.GetNullableByID("1");
            rtValue.PriorityType = Repository.OfType<PriorityType>().Queryable.FirstOrDefault();
            rtValue.Project = Repository.OfType<Project>().Queryable.FirstOrDefault();
            rtValue.Category = Repository.OfType<RequirementCategory>().Queryable.FirstOrDefault();

            return rtValue;
        }

        /// <summary>
        /// A Query which will return a single record
        /// </summary>
        /// <param name="numberAtEnd"></param>
        /// <returns></returns>
        protected override IQueryable<Requirement> GetQuery(int numberAtEnd)
        {
            return RequirementRepository.Queryable.Where(a => a.Description.EndsWith(numberAtEnd.ToString()));
        }

        /// <summary>
        /// A way to compare the entities that were read.
        /// For example, this would have the assert.AreEqual("Comment" + counter, entity.Comment);
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="counter"></param>
        protected override void FoundEntityComparison(Requirement entity, int counter)
        {
            Assert.AreEqual("Description" + counter, entity.Description);
        }

        /// <summary>
        /// Updates , compares, restores.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="action">The action.</param>
        protected override void UpdateUtility(Requirement entity, ARTAction action)
        {
            const string updateValue = "Updated";
            switch (action)
            {
                case ARTAction.Compare:
                    Assert.AreEqual(updateValue, entity.Description);
                    break;
                case ARTAction.Restore:
                    entity.Description = RestoreValue;
                    break;
                case ARTAction.Update:
                    RestoreValue = entity.Description;
                    entity.Description = updateValue;
                    break;
            }
        }

        /// <summary>
        /// Loads the data.
        /// </summary>
        protected override void LoadData()
        {
            LoadRequirementTypes(1);
            LoadProjects(1);
            LoadCategories(1);           
            LoadPriorityTypes(1);
            RequirementRepository.DbContext.BeginTransaction();
            LoadRecords(5);
            RequirementRepository.DbContext.CommitTransaction();
        }



        #endregion Init and Overrides		
		
        
    }
}