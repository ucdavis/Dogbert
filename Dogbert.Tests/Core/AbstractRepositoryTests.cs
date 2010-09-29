using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dogbert.Core.Domain;
using Dogbert.Tests.Core.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UCDArch.Core.DomainModel;
using UCDArch.Core.PersistanceSupport;
using UCDArch.Data.NHibernate;
using UCDArch.Testing;

namespace Dogbert.Tests.Core
{
    [TestClass]
    // ReSharper disable InconsistentNaming
    public abstract class AbstractRepositoryTests<T, IdT> : RepositoryTestBase where T : DomainObjectWithTypedId<IdT>
    // ReSharper restore InconsistentNaming
    {
        protected int EntriesAdded;
        protected string RestoreValue;
        protected bool BoolRestoreValue;

        public IRepositoryWithTypedId<RequirementType, string> RequirementTypeRepository { get; set; }
        public IRepositoryWithTypedId<TextType, string> TextTypeRepository { get; set; }

        #region Init

        public AbstractRepositoryTests()
        {
            RequirementTypeRepository = new RepositoryWithTypedId<RequirementType, string>();
            TextTypeRepository = new RepositoryWithTypedId<TextType, string>();
        }

        /// <summary>
        /// Gets the valid entity of type T
        /// </summary>
        /// <param name="counter">The counter.</param>
        /// <returns>A valid entity of type T</returns>
        protected abstract T GetValid(int? counter);

        /// <summary>
        /// A way to compare the entities that were read.
        /// For example, this would have the assert.AreEqual("Comment" + counter, entity.Comment);
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="counter"></param>
        protected abstract void FoundEntityComparison(T entity, int counter);

        /// <summary>
        /// Updates , compares, restores.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="action">The action.</param>
        protected abstract void UpdateUtility(T entity, ARTAction action);

        /// <summary>
        /// A Qury which will return a single record
        /// </summary>
        /// <param name="numberAtEnd"></param>
        /// <returns></returns>
        protected abstract IQueryable<T> GetQuery(int numberAtEnd);

        /// <summary>
        /// Loads the records for CRUD Tests.
        /// </summary>
        /// <returns></returns>
        protected virtual void LoadRecords(int entriesToAdd)
        {
            EntriesAdded += entriesToAdd;
            for (int i = 0; i < entriesToAdd; i++)
            {
                var validEntity = GetValid(i + 1);
                Repository.OfType<T>().EnsurePersistent(validEntity);
            }
        }

        #endregion Init

        #region CRUD Tests

        /// <summary>
        /// Determines whether this instance [can save valid entity].
        /// </summary>
        [TestMethod]
        public void CanSaveValidEntity()
        {
            var validEntity = GetValid(null);
            Repository.OfType<T>().EnsurePersistent(validEntity, true);

            Assert.AreEqual(false, validEntity.IsTransient());
        }


        /// <summary>
        /// Determines whether this instance [can commit valid entity].
        /// </summary>
        [TestMethod]
        public void CanCommitValidEntity()
        {
            var validEntity = GetValid(null);
            Repository.OfType<T>().DbContext.BeginTransaction();
            Repository.OfType<T>().EnsurePersistent(validEntity, true);
            Assert.IsFalse(validEntity.IsTransient());
            Repository.OfType<T>().DbContext.CommitTransaction();
        }


        /// <summary>
        /// Determines whether this instance [can get all entities].
        /// </summary>
        [TestMethod]
        public void CanGetAllEntities()
        {
            var foundEntities = Repository.OfType<T>().GetAll().ToList();
            Assert.AreEqual(EntriesAdded, foundEntities.Count, "GetAll() returned a different number of records");
            for (int i = 0; i < EntriesAdded; i++)
            {
                FoundEntityComparison(foundEntities[i], i + 1);
            }
        }

        /// <summary>
        /// Determines whether this instance [can query entities].
        /// </summary>
        [TestMethod]
        public void CanQueryEntities()
        {
            List<T> foundEntry = GetQuery(3).ToList();
            Assert.AreEqual(1, foundEntry.Count);
            FoundEntityComparison(foundEntry[0], 3);
        }

        /// <summary>
        /// Determines whether this instance [can get entity using get by id].
        /// </summary>
        [TestMethod]
        public virtual void CanGetEntityUsingGetById()
        {
            if (typeof(IdT) == typeof(int))
            {
                Assert.IsTrue(EntriesAdded >= 2, "There are not enough entries to complete this test.");
                var foundEntity = Repository.OfType<T>().GetByID(2);
                FoundEntityComparison(foundEntity, 2);
            }
            else
            {
                IRepositoryWithTypedId<T, string> typedStringRepository = new RepositoryWithTypedId<T, string>();
                Assert.IsTrue(EntriesAdded >= 2, "There are not enough entries to complete this test.");
                var foundEntity = typedStringRepository.GetByID("2");
                FoundEntityComparison(foundEntity, 2);
            }
        }

        /// <summary>
        /// Determines whether this instance [can get entity using get by nullable with valid id].
        /// </summary>
        [TestMethod]
        public virtual void CanGetEntityUsingGetByNullableWithValidId()
        {
            if (typeof(IdT) == typeof(int))
            {
                Assert.IsTrue(EntriesAdded >= 2, "There are not enough entries to complete this test.");
                var foundEntity = Repository.OfType<T>().GetNullableByID(2);
                FoundEntityComparison(foundEntity, 2);
            }
            else
            {
                IRepositoryWithTypedId<T, string> typedStringRepository = new RepositoryWithTypedId<T, string>();
                Assert.IsTrue(EntriesAdded >= 2, "There are not enough entries to complete this test.");
                var foundEntity = typedStringRepository.GetNullableByID("2");
                FoundEntityComparison(foundEntity, 2);
            }
        }

        /// <summary>
        /// Determines whether this instance [can get null value using get by nullable with invalid id].
        /// </summary>
        [TestMethod]
        public virtual void CanGetNullValueUsingGetByNullableWithInvalidId()
        {
            if (typeof(IdT) == typeof(int))
            {
                var foundEntity = Repository.OfType<T>().GetNullableByID(EntriesAdded + 1);
                Assert.IsNull(foundEntity);
            }
            else
            {
                IRepositoryWithTypedId<T, string> typedStringRepository = new RepositoryWithTypedId<T, string>();
                var foundEntity = typedStringRepository.GetNullableByID((EntriesAdded + 1).ToString());
                Assert.IsNull(foundEntity);
            }
        }

        public void CanUpdateEntity(bool doesItAllowUpdate)
        {
            if (typeof(IdT) == typeof(int))
            {
                //Get an entity to update
                var foundEntity = Repository.OfType<T>().GetAll()[2];

                //Update and commit entity
                Repository.OfType<T>().DbContext.BeginTransaction();
                UpdateUtility(foundEntity, ARTAction.Update);
                Repository.OfType<T>().EnsurePersistent(foundEntity);
                Repository.OfType<T>().DbContext.CommitTransaction();

                NHibernateSessionManager.Instance.GetSession().Evict(foundEntity);

                if (doesItAllowUpdate)
                {
                    //Compare entity
                    var compareEntity = Repository.OfType<T>().GetAll()[2];
                    UpdateUtility(compareEntity, ARTAction.Compare);

                    //Restore entity, do not commit, then get entity to make sure it isn't restored.            
                    UpdateUtility(compareEntity, ARTAction.Restore);
                    NHibernateSessionManager.Instance.GetSession().Evict(compareEntity);
                    //For testing at least, this is required to clear the changes from memory.
                    var checkNotUpdatedEntity = Repository.OfType<T>().GetAll()[2];
                    UpdateUtility(checkNotUpdatedEntity, ARTAction.Compare);
                }
                else
                {
                    //Compare entity
                    var compareEntity = Repository.OfType<T>().GetAll()[2];
                    UpdateUtility(compareEntity, ARTAction.CompareNotUpdated);
                }
            }
            else
            {
                IRepositoryWithTypedId<T, string> typedStringRepository = new RepositoryWithTypedId<T, string>();
                //Get an entity to update
                var foundEntity = typedStringRepository.GetAll()[2];

                //Update and commit entity
                typedStringRepository.DbContext.BeginTransaction();
                UpdateUtility(foundEntity, ARTAction.Update);
                typedStringRepository.EnsurePersistent(foundEntity, true);
                typedStringRepository.DbContext.CommitTransaction();

                NHibernateSessionManager.Instance.GetSession().Evict(foundEntity);

                if (doesItAllowUpdate)
                {
                    //Compare entity
                    var compareEntity = typedStringRepository.GetAll()[2];
                    UpdateUtility(compareEntity, ARTAction.Compare);

                    //Restore entity, do not commit, then get entity to make sure it isn't restored.            
                    UpdateUtility(compareEntity, ARTAction.Restore);
                    NHibernateSessionManager.Instance.GetSession().Evict(compareEntity);
                    //For testing at least, this is required to clear the changes from memory.
                    var checkNotUpdatedEntity = typedStringRepository.GetAll()[2];
                    UpdateUtility(checkNotUpdatedEntity, ARTAction.Compare);
                }
                else
                {
                    //Compare entity
                    var compareEntity = typedStringRepository.GetAll()[2];
                    UpdateUtility(compareEntity, ARTAction.CompareNotUpdated);
                }
            }
        }

        /// <summary>
        /// Determines whether this instance [can update entity].
        /// Defaults to true unless overridden
        /// </summary>
        [TestMethod]
        public virtual void CanUpdateEntity()
        {
            CanUpdateEntity(true);
        }


        /// <summary>
        /// Determines whether this instance [can delete entity].
        /// </summary>
        [TestMethod]
        public virtual void CanDeleteEntity()
        {
            var count = Repository.OfType<T>().GetAll().ToList().Count();
            var foundEntity = Repository.OfType<T>().GetAll().ToList()[2];

            //Update and commit entity
            Repository.OfType<T>().DbContext.BeginTransaction();
            Repository.OfType<T>().Remove(foundEntity);
            Repository.OfType<T>().DbContext.CommitTransaction();
            Assert.AreEqual(count - 1, Repository.OfType<T>().GetAll().ToList().Count());
            if (typeof(IdT) == typeof(int))
            {
                foundEntity = Repository.OfType<T>().GetNullableByID(3);
                Assert.IsNull(foundEntity);
            }
        }

        #endregion CRUD Tests

        #region Utilities

        /// <summary>
        /// Loads the requirement types.        
        /// </summary>
        /// <param name="entriesToAdd">The entries to add.</param>
        protected void LoadRequirementTypes(int entriesToAdd)
        {
            RequirementTypeRepository.DbContext.BeginTransaction();
            for (int i = 0; i < entriesToAdd; i++)
            {
                var validEntity = CreateValidEntities.RequirementType(i + 1);
                validEntity.SetIdTo((i + 1).ToString());
                RequirementTypeRepository.EnsurePersistent(validEntity);
            }
            RequirementTypeRepository.DbContext.CommitTransaction();
        }

        /// <summary>
        /// Loads the priority types.
        /// </summary>
        /// <param name="entriesToAdd">The entries to add.</param>
        protected void LoadPriorityTypes(int entriesToAdd)
        {
            Repository.OfType<ProjectType>().DbContext.BeginTransaction();
            for (int i = 0; i < entriesToAdd; i++)
            {
                var validEntity = CreateValidEntities.PriorityType(i + 1);
                Repository.OfType<PriorityType>().EnsurePersistent(validEntity);
            }
            Repository.OfType<ProjectType>().DbContext.CommitTransaction();
        }

        /// <summary>
        /// Loads the projects.
        /// </summary>
        /// <param name="entriesToAdd">The entries to add.</param>
        protected void LoadProjects(int entriesToAdd)
        {
            Repository.OfType<Project>().DbContext.BeginTransaction();
            for (int i = 0; i < entriesToAdd; i++)
            {
                var validEntity = CreateValidEntities.Project(i + 1);
                Repository.OfType<Project>().EnsurePersistent(validEntity);
            }
            Repository.OfType<Project>().DbContext.CommitTransaction();
        }

        /// <summary>
        /// Loads the Use Cases.
        /// </summary>
        /// <param name="entriesToAdd">The entries to add.</param>
        protected void LoadUseCase(int entriesToAdd)
        {
            for (int i = 0; i < entriesToAdd; i++)
            {
                var validEntity = CreateValidEntities.UseCase(i + 1);
                Repository.OfType<UseCase>().EnsurePersistent(validEntity);
            }
        }

        /// <summary>
        /// Loads the categories.
        /// </summary>
        /// <param name="entriesToAdd">The entries to add.</param>
        protected void LoadCategories(int entriesToAdd)
        {
            Repository.OfType<RequirementCategory>().DbContext.BeginTransaction();
            for (int i = 0; i < entriesToAdd; i++)
            {
                var validEntity = CreateValidEntities.Category(i + 1);
                validEntity.Project = Repository.OfType<Project>().GetNullableByID(1);
                Repository.OfType<RequirementCategory>().EnsurePersistent(validEntity);
            }
            Repository.OfType<RequirementCategory>().DbContext.CommitTransaction();
        }

        /// <summary>
        /// Loads the person types.
        /// </summary>
        /// <param name="entriesToAdd">The entries to add.</param>
        protected void LoadPersonTypes(int entriesToAdd)
        {
            Repository.OfType<PersonType>().DbContext.BeginTransaction();
            for (int i = 0; i < entriesToAdd; i++)
            {
                var validEntity = CreateValidEntities.PersonType(i + 1);
                Repository.OfType<PersonType>().EnsurePersistent(validEntity);
            }
            Repository.OfType<PersonType>().DbContext.CommitTransaction();
        }

        protected void LoadUsers(int entriesToAdd)
        {
            Repository.OfType<User>().DbContext.BeginTransaction();
            for (int i = 0; i < entriesToAdd; i++)
            {
                var validEntity = CreateValidEntities.User(i + 1);
                Repository.OfType<User>().EnsurePersistent(validEntity);
            }
            Repository.OfType<User>().DbContext.CommitTransaction();
        }

        protected void LoadFileTypes(int entriesToAdd)
        {
            Repository.OfType<FileType>().DbContext.BeginTransaction();
            for (int i = 0; i < entriesToAdd; i++)
            {
                var validEntity = CreateValidEntities.FileType(i + 1);
                Repository.OfType<FileType>().EnsurePersistent(validEntity);
            }
            Repository.OfType<FileType>().DbContext.CommitTransaction();
        }

        protected void LoadTextTypes(int entriesToAdd)
        {
            TextTypeRepository.DbContext.BeginTransaction();
            for (int i = 0; i < entriesToAdd; i++)
            {
                var validEntity = CreateValidEntities.TextType(i + 1);
                //validEntity.SetIdTo((i + 1).ToString());
                validEntity.setID((i+1).ToString());
                Repository.OfType<TextType>().EnsurePersistent(validEntity, true);
            }
            TextTypeRepository.DbContext.CommitTransaction();
        }

        /// <summary>
        /// Loads the requirement categories.
        /// Requires Project
        /// </summary>
        /// <param name="entriesToAdd">The entries to add.</param>
        protected void LoadRequirementCategories(int entriesToAdd)
        {
            Repository.OfType<RequirementCategory>().DbContext.BeginTransaction();
            for (int i = 0; i < entriesToAdd; i++)
            {
                var validEntity = CreateValidEntities.RequirementCategory(i + 1);
                validEntity.Project = Repository.OfType<Project>().GetNullableByID(1);
                Repository.OfType<RequirementCategory>().EnsurePersistent(validEntity);
            }
            Repository.OfType<RequirementCategory>().DbContext.CommitTransaction();
        }

        

        /// <summary>
        /// Abstract Repository Tests Action
        /// </summary>
        public enum ARTAction
        {
            Compare = 1,
            Update,
            Restore,
            CompareNotUpdated
        }
        #endregion Utilities


    }
}
