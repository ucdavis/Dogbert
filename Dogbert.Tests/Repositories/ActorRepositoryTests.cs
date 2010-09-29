using System.Linq;
using Dogbert.Core.Domain;
using Dogbert.Tests.Core;
using Dogbert.Tests.Core.Helpers;
using Dogbert.Tests.Core.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UCDArch.Data.NHibernate;
using UCDArch.Core.PersistanceSupport;
using UCDArch.Testing.Extensions;

using System;


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
		
        #region Name test

        #region invalid tests
        /// <summary>
        /// Tests the code with null does not save.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(System.ApplicationException))]
        public void TestNameWithNullDoesNotSave()
        {
            Actor actor = null;
            try
            {
                #region Arrange
                actor = GetValid(9);
                actor.Name = null;
                #endregion Arrange

                #region Act
                ActorRepository.DbContext.BeginTransaction();
                ActorRepository.EnsurePersistent(actor);
                ActorRepository.DbContext.CommitTransaction();
                #endregion Act
            }
            catch (System.Exception)
            {
                #region Assert
                Assert.IsNotNull(actor);
                var results = actor.ValidationResults().AsMessageList();
                results.AssertErrorsAre(
                    "Name: may not be null or empty");
                Assert.IsTrue(actor.IsTransient());
                Assert.IsFalse(actor.IsValid());
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
            Actor actor= null;
            try
            {
                #region Arrange
                actor = GetValid(9);
                actor.Name = string.Empty;
                #endregion Arrange

                #region Act
                ActorRepository.DbContext.BeginTransaction();
                ActorRepository.EnsurePersistent(actor);
                ActorRepository.DbContext.CommitTransaction();
                #endregion Act
            }
            catch (Exception)
            {
                #region Assert
                Assert.IsNotNull(actor);
                var results = actor.ValidationResults().AsMessageList();
                results.AssertErrorsAre(
                    "Name: may not be null or empty");

                Assert.IsTrue(actor.IsTransient());
                Assert.IsFalse(actor.IsValid());
                #endregion Assert

                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestNameTooLongDoesNotSave()
        {
            Actor actor = null;
            try
            {
                #region Arrange
                actor = GetValid(9);
                actor.Name = "x".RepeatTimes(51);
                #endregion Arrange

                #region Act
                ActorRepository.DbContext.BeginTransaction();
                ActorRepository.EnsurePersistent(actor);
                ActorRepository.DbContext.CommitTransaction();
                #endregion Act
            }//end try
            catch
            {
                #region Assert
                Assert.IsNotNull(actor);
                var results = actor.ValidationResults().AsMessageList();
                results.AssertErrorsAre("Name: length must be between 0 and 50");
                Assert.IsTrue(actor.IsTransient());
                Assert.IsFalse(actor.IsValid());
                #endregion Assert
                
                throw;
            }//end  catch



        }//end TestStringTooLongDoesNotSave

        #endregion invalid tests
    
        #region valid tests
        [TestMethod]
        public void TestNameWithOneCharSaves()
        {
            Actor actor = null;

            #region Arrange
            actor = GetValid(9);
            actor.Name = "x";
            #endregion Arrange

            #region act
            ActorRepository.DbContext.BeginTransaction();
            ActorRepository.EnsurePersistent(actor);
            ActorRepository.DbContext.CommitTransaction();
            #endregion act

            #region Assert
            Assert.IsNotNull(actor);
            Assert.AreEqual(actor.Name.Length, 1);
            Assert.IsFalse(actor.IsTransient());
            Assert.IsTrue(actor.IsValid());
            #endregion Assert

        }//end TestNameWithOneCharSaves

        [TestMethod]
        public void TestNameWithMaxCharSaves()
        {
            Actor actor = null;

            #region Arrange
            actor = GetValid(9);
            actor.Name = "x".RepeatTimes(50);
            #endregion Arrange

            #region act
            ActorRepository.DbContext.BeginTransaction();
            ActorRepository.EnsurePersistent(actor);
            ActorRepository.DbContext.CommitTransaction();
            #endregion act

            #region Assert
            Assert.IsNotNull(actor);
            Assert.AreEqual(actor.Name.Length, 50);
            Assert.IsFalse(actor.IsTransient());
            Assert.IsTrue(actor.IsValid());
            #endregion Assert

        }//end TestNameWithOneCharSaves

        #endregion valid tests

        #endregion Name test

        #region IsActive

        #region valid tests
        [TestMethod]
        public void IsActiveFalseSaves()
        {
            Actor actor = null;

            #region Arrange
            actor = GetValid(9);
            actor.IsActive = false;
            #endregion Arrange

            #region Act
            ActorRepository.DbContext.BeginTransaction();
            ActorRepository.EnsurePersistent(actor);
            ActorRepository.DbContext.CommitTransaction();
            #endregion Act

            #region Assert
            Assert.IsNotNull(actor);
            Assert.AreEqual(actor.IsActive, false);
            Assert.IsFalse(actor.IsTransient());
            Assert.IsTrue(actor.IsValid());
            #endregion Assert
        }//end IsActiveFalseSaves

        [TestMethod]
        public void IsActiveTrueSaves()
        {
            Actor actor = null;

            #region Arrange
            actor = GetValid(9);
            actor.IsActive = true;
            #endregion Arrange

            #region Act
            ActorRepository.DbContext.BeginTransaction();
            ActorRepository.EnsurePersistent(actor);
            ActorRepository.DbContext.CommitTransaction();
            #endregion Act

            #region Assert
            Assert.IsNotNull(actor);
            Assert.AreEqual(actor.IsActive, true);
            Assert.IsFalse(actor.IsTransient());
            Assert.IsTrue(actor.IsValid());
            #endregion Assert
        }//end IsActiveFalseSaves

        #endregion valid tests
        #endregion IsActive

        #region UseCases
        #region valid tests
        [TestMethod]
        public void TestAddActorFromUseCasetoActor()
        {
            ////5 actors in repository to start with
            //Assert.AreEqual(5, ActorRepository.GetAll().Count);

            //Actor actor = null;
            //var UseCaseRepository = new Repository<UseCase>();
            
            //#region Arrange
            //UseCase usecase = CreateValidEntities.UseCase(9); //get usecase entity
            //actor = GetValid(9); //get actor entity
            //actor.UseCases = null;
            //usecase.AddActors(actor);
            //#endregion Arrange

            //#region Act
            //UseCaseRepository.DbContext.BeginTransaction();
            //UseCaseRepository.EnsurePersistent(usecase);
            //UseCaseRepository.DbContext.CommitTransaction();
            //#endregion Act

            //#region Asserts
            //Assert.AreEqual(6, ActorRepository.GetAll().Count);
            //#endregion Asserts


        }//end UseCaseSave

        #endregion valid tests
        #endregion UseCases

    }
}