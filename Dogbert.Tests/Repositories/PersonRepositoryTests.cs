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
    /// Entity Name:		Person
    /// LookupFieldName:	FullName
    /// </summary>
    [TestClass]
    public class PersonRepositoryTests : AbstractRepositoryTests<Person, int>
    {
        /// <summary>
        /// Gets or sets the Person repository.
        /// </summary>
        /// <value>The Person repository.</value>
        public IRepository<Person> PersonRepository { get; set; }
		
        #region Init and Overrides

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonRepositoryTests"/> class.
        /// </summary>
        public PersonRepositoryTests()
        {
            PersonRepository = new Repository<Person>();
        }

        /// <summary>
        /// Gets the valid entity of type T
        /// </summary>
        /// <param name="counter">The counter.</param>
        /// <returns>A valid entity of type T</returns>
        protected override Person GetValid(int? counter)
        {
            var localCount = counter ?? 1;
            var rtValue = CreateValidEntities.Person(counter);
            rtValue.User = Repository.OfType<User>().GetNullableById(localCount);
            rtValue.PersonType = Repository.OfType<PersonType>().GetNullableById(1);

            return rtValue;
        }

        /// <summary>
        /// A Query which will return a single record
        /// </summary>
        /// <param name="numberAtEnd"></param>
        /// <returns></returns>
        protected override IQueryable<Person> GetQuery(int numberAtEnd)
        {
            return PersonRepository.Queryable.Where(a => a.User.FirstName.EndsWith(numberAtEnd.ToString()));
        }

        /// <summary>
        /// A way to compare the entities that were read.
        /// For example, this would have the assert.AreEqual("Comment" + counter, entity.Comment);
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="counter"></param>
        protected override void FoundEntityComparison(Person entity, int counter)
        {
            Assert.AreEqual("FirstName" + counter + " LastName" + counter, entity.FullName);
        }

        /// <summary>
        /// Updates , compares, restores.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="action">The action.</param>
        protected override void UpdateUtility(Person entity, ARTAction action)
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
            LoadPersonTypes(1);
            PersonRepository.DbContext.BeginTransaction();
            LoadRecords(5);
            PersonRepository.DbContext.CommitTransaction();
        }

        #endregion Init and Overrides		
		
        
    }
}