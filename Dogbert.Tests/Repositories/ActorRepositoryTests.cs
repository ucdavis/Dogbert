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
    /// Entity Name:		Actor
    /// LookupFieldName:	Name
    /// </summary>
    [TestClass]
    public class ActorRepositoryTests : AbstractRepositoryTests<Actor, int>
    {
        /// <summary>
        /// Gets or sets the Actor repository.
        /// </summary>
        /// <value>The Actor repository.</value>
        public IRepository<Actor> ActorRepository { get; set; }
		
        #region Init and Overrides

        /// <summary>
        /// Initializes a new instance of the <see cref="ActorRepositoryTests"/> class.
        /// </summary>
        public ActorRepositoryTests()
        {
            ActorRepository = new Repository<Actor>();
        }

        /// <summary>
        /// Gets the valid entity of type T
        /// </summary>
        /// <param name="counter">The counter.</param>
        /// <returns>A valid entity of type T</returns>
        protected override Actor GetValid(int? counter)
        {
            return CreateValidEntities.Actor(counter);
        }

        /// <summary>
        /// A Query which will return a single record
        /// </summary>
        /// <param name="numberAtEnd"></param>
        /// <returns></returns>
        protected override IQueryable<Actor> GetQuery(int numberAtEnd)
        {
            return ActorRepository.Queryable.Where(a => a.Name.EndsWith(numberAtEnd.ToString()));
        }

        /// <summary>
        /// A way to compare the entities that were read.
        /// For example, this would have the assert.AreEqual("Comment" + counter, entity.Comment);
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="counter"></param>
        protected override void FoundEntityComparison(Actor entity, int counter)
        {
            Assert.AreEqual("Name" + counter, entity.Name);
        }

        /// <summary>
        /// Updates , compares, restores.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="action">The action.</param>
        protected override void UpdateUtility(Actor entity, ARTAction action)
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
            ActorRepository.DbContext.BeginTransaction();
            LoadRecords(5);
            ActorRepository.DbContext.CommitTransaction();
        }

        #endregion Init and Overrides		
		
        
    }
}