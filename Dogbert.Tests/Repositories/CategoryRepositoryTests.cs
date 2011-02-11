using System.Linq;
using Dogbert.Core.Domain;
using Dogbert.Tests.Core;
using Dogbert.Tests.Core.Helpers;
using Dogbert.Tests.Core.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UCDArch.Core.PersistanceSupport;
using UCDArch.Data.NHibernate;
using UCDArch.Testing.Extensions;
using System;

namespace Dogbert.Tests.Repositories
{
    /// <summary>
    /// Entity Name:		Category
    /// LookupFieldName:	Name
    /// </summary>
    [TestClass]
    public class CategoryRepositoryTests : AbstractRepositoryTests<RequirementCategory, int>
    {
        /// <summary>
        /// Gets or sets the Category repository.
        /// </summary>
        /// <value>The Category repository.</value>
        public IRepository<RequirementCategory> CategoryRepository { get; set; }
        public IRepository<Project> ProjectRepository { get; set; }
		
        #region Init and Overrides

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryRepositoryTests"/> class.
        /// </summary>
        public CategoryRepositoryTests()
        {
            CategoryRepository = new Repository<RequirementCategory>();
            ProjectRepository = new Repository<Project>();
        }

        /// <summary>
        /// Gets the valid entity of type T
        /// </summary>
        /// <param name="counter">The counter.</param>
        /// <returns>A valid entity of type T</returns>
        protected override RequirementCategory GetValid(int? counter)
        {
            var rtValue = CreateValidEntities.Category(counter);
            rtValue.Project = Repository.OfType<Project>().GetNullableById(1);
            return rtValue;
        }

        /// <summary>
        /// A Query which will return a single record
        /// </summary>
        /// <param name="numberAtEnd"></param>
        /// <returns></returns>
        protected override IQueryable<RequirementCategory> GetQuery(int numberAtEnd)
        {
            return CategoryRepository.Queryable.Where(a => a.Name.EndsWith(numberAtEnd.ToString()));
        }

        /// <summary>
        /// A way to compare the entities that were read.
        /// For example, this would have the assert.AreEqual("Comment" + counter, entity.Comment);
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="counter"></param>
        protected override void FoundEntityComparison(RequirementCategory entity, int counter)
        {
            Assert.AreEqual("Name" + counter, entity.Name);
        }

        /// <summary>
        /// Updates , compares, restores.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="action">The action.</param>
        protected override void UpdateUtility(RequirementCategory entity, ARTAction action)
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
            CategoryRepository.DbContext.BeginTransaction();
            LoadRecords(5);
            CategoryRepository.DbContext.CommitTransaction();
        }

        #endregion Init and Overrides		
		
        #region Name test

        #region invalid tests
        /// <summary>
        /// Tests the code with null does not save.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(System.ApplicationException))]
        public void TestNameWithNullDoesNotSave()
        {
            RequirementCategory requirementcategory = null;
            try
            {
                #region Arrange
                requirementcategory = GetValid(9);
                requirementcategory.Name = null;
                #endregion Arrange

                #region Act
                CategoryRepository.DbContext.BeginTransaction();
                CategoryRepository.EnsurePersistent(requirementcategory);
                CategoryRepository.DbContext.CommitTransaction();
                #endregion Act
            }
            catch (System.Exception)
            {
                #region Assert
                Assert.IsNotNull(requirementcategory);
                var results = requirementcategory.ValidationResults().AsMessageList();
                results.AssertErrorsAre(
                    "Name: may not be null or empty");
                Assert.IsTrue(requirementcategory.IsTransient());
                Assert.IsFalse(requirementcategory.IsValid());
                #endregion Assert

                throw;
            }
        }

        /// <summary>
        /// Tests the code with empty string does not save.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestNameWithEmptyStringDoesNotSave()
        {
            RequirementCategory requirementcategory = null;
            try
            {
                #region Arrange
                requirementcategory = GetValid(9);
                requirementcategory.Name = string.Empty;
                #endregion Arrange

                #region Act
                CategoryRepository.DbContext.BeginTransaction();
                CategoryRepository.EnsurePersistent(requirementcategory);
                CategoryRepository.DbContext.CommitTransaction();
                #endregion Act
            }
            catch (Exception)
            {
                #region Assert
                Assert.IsNotNull(requirementcategory);
                var results = requirementcategory.ValidationResults().AsMessageList();
                results.AssertErrorsAre(
                    "Name: may not be null or empty");

                Assert.IsTrue(requirementcategory.IsTransient());
                Assert.IsFalse(requirementcategory.IsValid());
                #endregion Assert

                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestNameTooLongDoesNotSave()
        {
            RequirementCategory requirementcategory = null;
            try
            {
                #region Arrange
                requirementcategory = GetValid(9);
                requirementcategory.Name = "x".RepeatTimes(51);
                #endregion Arrange

                #region Act
                CategoryRepository.DbContext.BeginTransaction();
                CategoryRepository.EnsurePersistent(requirementcategory);
                CategoryRepository.DbContext.CommitTransaction();
                #endregion Act
            }//end try
            catch
            {
                #region Assert
                Assert.IsNotNull(requirementcategory);
                var results = requirementcategory.ValidationResults().AsMessageList();
                results.AssertErrorsAre("Name: length must be between 0 and 50");
                Assert.IsTrue(requirementcategory.IsTransient());
                Assert.IsFalse(requirementcategory.IsValid());
                #endregion Assert

                throw;
            }//end  catch
        }//end TestStringTooLongDoesNotSave

        #endregion invalid tests

        #region valid tests
        [TestMethod]
        public void TestNameWithOneCharSaves()
        {
            RequirementCategory requirementcategory = null;

            #region Arrange
            requirementcategory = GetValid(9);
            requirementcategory.Name = "x";
            #endregion Arrange

            #region act
            CategoryRepository.DbContext.BeginTransaction();
            CategoryRepository.EnsurePersistent(requirementcategory);
            CategoryRepository.DbContext.CommitTransaction();
            #endregion act

            #region Assert
            Assert.IsNotNull(requirementcategory);
            Assert.AreEqual(requirementcategory.Name.Length, 1);
            Assert.IsFalse(requirementcategory.IsTransient());
            Assert.IsTrue(requirementcategory.IsValid());
            #endregion Assert

        }//end TestNameWithOneCharSaves

        [TestMethod]
        public void TestNameWithMaxCharSaves()
        {
            RequirementCategory requirementcategory = null;

            #region Arrange
            requirementcategory = GetValid(9);
            requirementcategory.Name = "x".RepeatTimes(50);
            #endregion Arrange

            #region act
            CategoryRepository.DbContext.BeginTransaction();
            CategoryRepository.EnsurePersistent(requirementcategory);
            CategoryRepository.DbContext.CommitTransaction();
            #endregion act

            #region Assert
            Assert.IsNotNull(requirementcategory);
            Assert.AreEqual(requirementcategory.Name.Length, 50);
            Assert.IsFalse(requirementcategory.IsTransient());
            Assert.IsTrue(requirementcategory.IsValid());
            #endregion Assert

        }//end TestNameWithOneCharSaves

        #endregion valid tests

        #endregion Name test

        #region IsActive

        #region valid tests
        [TestMethod]
        public void IsActiveFalseSaves()
        {
            RequirementCategory requirementcategory = null;

            #region Arrange
            requirementcategory = GetValid(9);
            requirementcategory.IsActive = false;
            #endregion Arrange

            #region Act
            CategoryRepository.DbContext.BeginTransaction();
            CategoryRepository.EnsurePersistent(requirementcategory);
            CategoryRepository.DbContext.CommitTransaction();
            #endregion Act

            #region Assert
            Assert.IsNotNull(requirementcategory);
            Assert.AreEqual(requirementcategory.IsActive, false);
            Assert.IsFalse(requirementcategory.IsTransient());
            Assert.IsTrue(requirementcategory.IsValid());
            #endregion Assert
        }//end IsActiveFalseSaves

        [TestMethod]
        public void IsActiveTrueSaves()
        {
            RequirementCategory requirementcategory = null;

            #region Arrange
            requirementcategory = GetValid(9);
            requirementcategory.IsActive = true;
            #endregion Arrange

            #region Act
            CategoryRepository.DbContext.BeginTransaction();
            CategoryRepository.EnsurePersistent(requirementcategory);
            CategoryRepository.DbContext.CommitTransaction();
            #endregion Act

            #region Assert
            Assert.IsNotNull(requirementcategory);
            Assert.AreEqual(requirementcategory.IsActive, true);
            Assert.IsFalse(requirementcategory.IsTransient());
            Assert.IsTrue(requirementcategory.IsValid());
            #endregion Assert
        }//end IsActiveFalseSaves

        #endregion valid tests
        #endregion IsActive

        #region Project
        #region invalid tests
        /// <summary>
        /// Tests that a null project does not save.
        /// </summary>
        /// 
        [ExpectedException(typeof(ApplicationException))]
        [TestMethod]
        public void TestNullProjectDoesNotSave()
        {
            var category = GetValid(9);
            try
            {
                #region Arrange
               
                category.Project = null;
                #endregion Arrange

                #region act
                CategoryRepository.DbContext.BeginTransaction();
                CategoryRepository.EnsurePersistent(category);
                CategoryRepository.DbContext.CommitTransaction();
                #endregion act
            }
            catch{

                #region assert
                Assert.IsNotNull(category);
                var result = category.ValidationResults().AsMessageList();
                result.AssertErrorsAre("Project: may not be empty");
                Assert.IsTrue(category.IsTransient());
                Assert.IsFalse(category.IsValid());
                #endregion assert
               
                throw;
            }
        }

        #endregion invalid tests

        #region validTests

        /// <summary>
        /// Tests that valid project saves in category.
        /// </summary>
        [TestMethod]
        public void TestProjectSaveValid()
        {
            LoadProjects(3);
            #region Arrange
            var category = GetValid(9);
            category.Project = ProjectRepository.GetNullableById(2);
            #endregion Arrange

            #region act
            CategoryRepository.DbContext.BeginTransaction();
            CategoryRepository.EnsurePersistent(category);
            CategoryRepository.DbContext.CommitTransaction();
            #endregion act

            #region assert
            Assert.IsFalse(category.IsTransient());
            Assert.IsTrue(category.IsValid());

            #endregion assert
           

        }//end TestProjectSaveValid
        #endregion validTests
        #endregion Project

    }
}