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
    /// Entity Name:		FileType
    /// LookupFieldName:	Name
    /// </summary>
    [TestClass]
    public class FileTypeRepositoryTests : AbstractRepositoryTests<FileType, int>
    {
        /// <summary>
        /// Gets or sets the FileType repository.
        /// </summary>
        /// <value>The FileType repository.</value>
        public IRepository<FileType> FileTypeRepository { get; set; }
		
        #region Init and Overrides

        /// <summary>
        /// Initializes a new instance of the <see cref="FileTypeRepositoryTests"/> class.
        /// </summary>
        public FileTypeRepositoryTests()
        {
            FileTypeRepository = new Repository<FileType>();
        }

        /// <summary>
        /// Gets the valid entity of type T
        /// </summary>
        /// <param name="counter">The counter.</param>
        /// <returns>A valid entity of type T</returns>
        protected override FileType GetValid(int? counter)
        {
            return CreateValidEntities.FileType(counter);
        }

        /// <summary>
        /// A Query which will return a single record
        /// </summary>
        /// <param name="numberAtEnd"></param>
        /// <returns></returns>
        protected override IQueryable<FileType> GetQuery(int numberAtEnd)
        {
            return FileTypeRepository.Queryable.Where(a => a.Name.EndsWith(numberAtEnd.ToString()));
        }

        /// <summary>
        /// A way to compare the entities that were read.
        /// For example, this would have the assert.AreEqual("Comment" + counter, entity.Comment);
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="counter"></param>
        protected override void FoundEntityComparison(FileType entity, int counter)
        {
            Assert.AreEqual("Name" + counter, entity.Name);
        }

        /// <summary>
        /// Updates , compares, restores.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="action">The action.</param>
        protected override void UpdateUtility(FileType entity, ARTAction action)
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
            FileTypeRepository.DbContext.BeginTransaction();
            LoadRecords(5);
            FileTypeRepository.DbContext.CommitTransaction();
        }

        #endregion Init and Overrides		
		
        
    }
}