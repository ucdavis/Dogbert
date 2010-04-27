using System;
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
    /// Entity Name:		UseCase
    /// LookupFieldName:	Name
    /// </summary>
    [TestClass]
    public class UseCaseRepositoryTests : AbstractRepositoryTests<UseCase, int>
    {
        /// <summary>
        /// Gets or sets the UseCase repository.
        /// </summary>
        /// <value>The UseCase repository.</value>
        public IRepository<UseCase> UseCaseRepository { get; set; }
		
        #region Init and Overrides

        /// <summary>
        /// Initializes a new instance of the <see cref="UseCaseRepositoryTests"/> class.
        /// </summary>
        public UseCaseRepositoryTests()
        {
            UseCaseRepository = new Repository<UseCase>();
        }

        /// <summary>
        /// Gets the valid entity of type T
        /// </summary>
        /// <param name="counter">The counter.</param>
        /// <returns>A valid entity of type T</returns>
        protected override UseCase GetValid(int? counter)
        {
            var rtValue = CreateValidEntities.UseCase(counter);
            rtValue.Project = Repository.OfType<Project>().GetNullableByID(1);
            rtValue.RequirementCategory = Repository.OfType<RequirementCategory>().GetNullableByID(1);

            return rtValue;
        }

        /// <summary>
        /// A Query which will return a single record
        /// </summary>
        /// <param name="numberAtEnd"></param>
        /// <returns></returns>
        protected override IQueryable<UseCase> GetQuery(int numberAtEnd)
        {
            return UseCaseRepository.Queryable.Where(a => a.Name.EndsWith(numberAtEnd.ToString()));
        }

        /// <summary>
        /// A way to compare the entities that were read.
        /// For example, this would have the assert.AreEqual("Comment" + counter, entity.Comment);
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="counter"></param>
        protected override void FoundEntityComparison(UseCase entity, int counter)
        {
            Assert.AreEqual("Name" + counter, entity.Name);
        }

        /// <summary>
        /// Updates , compares, restores.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="action">The action.</param>
        protected override void UpdateUtility(UseCase entity, ARTAction action)
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
            LoadRequirementCategories(1);
            UseCaseRepository.DbContext.BeginTransaction();
            LoadRecords(5);
            UseCaseRepository.DbContext.CommitTransaction();
        }

        

        #endregion Init and Overrides		
		
        
    }
}