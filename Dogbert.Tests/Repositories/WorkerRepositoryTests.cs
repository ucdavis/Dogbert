using System;
using System.Linq;
using Dogbert.Core.Domain;
using Dogbert.Tests.Core;
using Dogbert.Tests.Core.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UCDArch.Core.PersistanceSupport;
using UCDArch.Data.NHibernate;
using UCDArch.Testing.Extensions;

namespace Dogbert.Tests.Repositories
{
    /// <summary>
    /// Entity Name:		Worker
    /// LookupFieldName:	FullName
    /// </summary>
    [TestClass]
    public class WorkerRepositoryTests : AbstractRepositoryTests<Worker, int>
    {
        /// <summary>
        /// Gets or sets the Worker repository.
        /// </summary>
        /// <value>The Worker repository.</value>
        public IRepository<Worker> WorkerRepository { get; set; }
		
        #region Init and Overrides

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkerRepositoryTests"/> class.
        /// </summary>
        public WorkerRepositoryTests()
        {
            WorkerRepository = new Repository<Worker>();
        }

        /// <summary>
        /// Gets the valid entity of type T
        /// </summary>
        /// <param name="counter">The counter.</param>
        /// <returns>A valid entity of type T</returns>
        protected override Worker GetValid(int? counter)
        {
            var localCount = counter ?? 1;
            var rtValue = CreateValidEntities.Worker(counter);
            rtValue.User = Repository.OfType<User>().GetNullableById(localCount);            

            return rtValue;
        }

        /// <summary>
        /// A Query which will return a single record
        /// </summary>
        /// <param name="numberAtEnd"></param>
        /// <returns></returns>
        protected override IQueryable<Worker> GetQuery(int numberAtEnd)
        {
            return WorkerRepository.Queryable.Where(a => a.User.FirstName.EndsWith(numberAtEnd.ToString()));
        }

        /// <summary>
        /// A way to compare the entities that were read.
        /// For example, this would have the assert.AreEqual("Comment" + counter, entity.Comment);
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="counter"></param>
        protected override void FoundEntityComparison(Worker entity, int counter)
        {
            Assert.AreEqual("FirstName" + counter + " LastName" + counter, entity.FullName);
        }

        /// <summary>
        /// Updates , compares, restores.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="action">The action.</param>
        protected override void UpdateUtility(Worker entity, ARTAction action)
        {
            const string updateValue = "FirstName6";
            switch (action)
            {
                case ARTAction.Compare:
                    Assert.AreEqual(updateValue, entity.User.FirstName);
                    break;
                case ARTAction.Restore:
                    entity.User = Repository.OfType<User>()
                        .Queryable.Where(a => a.FirstName == RestoreValue).FirstOrDefault();
                    break;
                case ARTAction.Update:
                    RestoreValue = entity.User.FirstName;
                    entity.User = Repository.OfType<User>()
                        .Queryable.Where(a => a.FirstName == updateValue).FirstOrDefault();
                    break;
            }
        }

        /// <summary>
        /// Loads the data.
        /// </summary>
        protected override void LoadData()
        {
            LoadUsers(6);
            WorkerRepository.DbContext.BeginTransaction();
            LoadRecords(5);
            WorkerRepository.DbContext.CommitTransaction();
        }

        #endregion Init and Overrides		
		
        
    }
}