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
    /// Entity Name:		TextType
    /// LookupFieldName:	Name
    /// </summary>
    [TestClass]
    public class TextTypeRepositoryTests : AbstractRepositoryTests<TextType, string>
    {
        /// <summary>
        /// Gets or sets the TextType repository.
        /// </summary>
        /// <value>The TextType repository.</value>
        public new IRepositoryWithTypedId<TextType, string> TextTypeRepository { get; set; }
		
        #region Init and Overrides

        /// <summary>
        /// Initializes a new instance of the <see cref="TextTypeRepositoryTests"/> class.
        /// </summary>
        public TextTypeRepositoryTests()
        {
            TextTypeRepository = new RepositoryWithTypedId<TextType, string>();
        }

        /// <summary>
        /// Gets the valid entity of type T
        /// </summary>
        /// <param name="counter">The counter.</param>
        /// <returns>A valid entity of type T</returns>
        protected override TextType GetValid(int? counter)
        {
            var localCount = (counter ?? 6).ToString();
            var rtValue = CreateValidEntities.TextType(counter);
            rtValue.setID(localCount);

            return rtValue;
        }

        /// <summary>
        /// A Query which will return a single record
        /// </summary>
        /// <param name="numberAtEnd"></param>
        /// <returns></returns>
        protected override IQueryable<TextType> GetQuery(int numberAtEnd)
        {
            return TextTypeRepository.Queryable.Where(a => a.Name.EndsWith(numberAtEnd.ToString()));
        }

        /// <summary>
        /// A way to compare the entities that were read.
        /// For example, this would have the assert.AreEqual("Comment" + counter, entity.Comment);
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="counter"></param>
        protected override void FoundEntityComparison(TextType entity, int counter)
        {
            Assert.AreEqual("Name" + counter, entity.Name);
        }

        /// <summary>
        /// Updates , compares, restores.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="action">The action.</param>
        protected override void UpdateUtility(TextType entity, ARTAction action)
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
            TextTypeRepository.DbContext.BeginTransaction();
            LoadRecords(5);
            TextTypeRepository.DbContext.CommitTransaction();
        }

        protected override void LoadRecords(int entriesToAdd)
        {
            EntriesAdded += entriesToAdd;
            for (int i = 0; i < entriesToAdd; i++)
            {
                var validEntity = GetValid(i + 1);
                TextTypeRepository.EnsurePersistent(validEntity, true);
            }
        }

        #endregion Init and Overrides		
		
        
    }
}