using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using Dogbert.Controllers;
using Dogbert.Controllers.ViewModels;
using Dogbert.Core.Domain;
using Dogbert.Tests;
using Dogbert.Tests.Core.Extensions;
using Dogbert.Tests.Core.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcContrib.TestHelper;
using Rhino.Mocks;
using UCDArch.Core.PersistanceSupport;
using UCDArch.Testing;

namespace Dogbert.Tests.Controllers
{
    [TestClass]
    public class RequirementControllerTests : Core.ControllerTestBase<RequirementController>
    {
        protected List<Project> Projects { get; set; }
        protected IRepository<Project> ProjectRepository { get; set; }
        protected List<RequirementType> RequirementTypes { get; set; }
        protected IRepository<RequirementType> RequirementTypeRepository { get; set; }
        protected List<RequirementCategory> Categories { get; set; }
        protected IRepository<RequirementCategory> CategoryRepository { get; set; }
        protected List<PriorityType> PriorityTypes { get; set; }
        protected IRepository<PriorityType> PriorityTypeRepository { get; set; }
        protected List<Requirement> Requirements { get; set; }
        protected IRepository<Requirement> RequirementRepository { get; set; }


        #region Init
        public RequirementControllerTests()
        {
            Projects = new List<Project>();
            ProjectRepository = FakeRepository<Project>();
            Controller.Repository.Expect(a => a.OfType<Project>()).Return(ProjectRepository).Repeat.Any();

            RequirementTypes = new List<RequirementType>();
            RequirementTypeRepository = FakeRepository<RequirementType>();
            Controller.Repository.Expect(a => a.OfType<RequirementType>()).Return(RequirementTypeRepository).Repeat.Any();

            Categories = new List<RequirementCategory>();
            CategoryRepository = FakeRepository<RequirementCategory>();
            Controller.Repository.Expect(a => a.OfType<RequirementCategory>()).Return(CategoryRepository).Repeat.Any();

            PriorityTypes = new List<PriorityType>();
            PriorityTypeRepository = FakeRepository<PriorityType>();
            Controller.Repository.Expect(a => a.OfType<PriorityType>()).Return(PriorityTypeRepository).Repeat.Any();

            Requirements = new List<Requirement>();
            RequirementRepository = FakeRepository<Requirement>();
            Controller.Repository.Expect(a => a.OfType<Requirement>()).Return(RequirementRepository).Repeat.Any();
        
        }


        #endregion Init

        #region Route Tests

        /// <summary>
        /// Tests the mapping of the index.
        /// </summary>
        [TestMethod]
        public void TestMappingIndex()
        {
            "~/Requirement/Index".ShouldMapTo<RequirementController>(a => a.Index());
        }

        /// <summary>
        /// Tests the mapping create with one parameter.
        /// </summary>
        [TestMethod]
        public void TestMappingCreateWithOneParameter()
        {
            "~/Requirement/Create/?projectId=2".ShouldMapTo<RequirementController>(a => a.Create(2),true);
        }

        /// <summary>
        /// Tests the mapping create with two parameters.
        /// </summary>
        [TestMethod]
        public void TestMappingCreateWithTwoParameters()
        {
            "~/Requirement/Create".ShouldMapTo<RequirementController>(a => a.Create(new Requirement(), 2), true);
        }

        /// <summary>
        /// Tests the mapping edit.
        /// </summary>
        [TestMethod]
        public void TestMappingEdit()
        {
           "~/Requirement/Edit/5".ShouldMapTo<RequirementController>(a => a.Edit(5));
        }

        /// <summary>
        /// Tests the mapping edit.
        /// </summary>
        [TestMethod]
        public void TestMappingEditWithTwoParameters()
        {
           "~/Requirement/Edit/5".ShouldMapTo<RequirementController>(a => a.Edit(5, new Requirement()), true);
        }

      
        #endregion Route Tests

        #region Index Tests

        [TestMethod]
        public void TestIndexRedirectsToAction()
        {
            Controller.Index()
                .AssertActionRedirect()
                .ToAction<ProjectController>(a => a.Index());
        }

        #endregion Index Tests

        #region Create Tests

        /// <summary>
        /// Tests the index of the create when id not found redirects to project.
        /// </summary>
        [TestMethod]
        public void TestCreateWhenIdNotFoundRedirectsToProjectIndex()
        {
            ProjectRepository.Expect(a => a.GetNullableById(1)).Return(null).Repeat.Any();
            Controller.Create(1)
                .AssertActionRedirect()
                .ToAction<ProjectController>(a => a.Index());
            Assert.AreEqual("Project was not found.", Controller.Message);
        }


        /// <summary>
        /// Tests the create when id is found.
        /// </summary>
        [TestMethod]
        public void TestCreateWhenIdIsFound()
        {
            FakeProjects(Projects, 3);
            FakeRequirementTypes(RequirementTypes, 1);
            FakeCategories(Categories, 2);
            FakePriorityTypes(PriorityTypes, 3);
            ProjectRepository.Expect(a => a.GetNullableById(2)).Return(Projects[1]).Repeat.Any();
            RequirementTypeRepository.Expect(a => a.Queryable).Return(RequirementTypes.AsQueryable()).Repeat.Any();
            CategoryRepository.Expect(a => a.Queryable).Return(Categories.AsQueryable()).Repeat.Any();
            PriorityTypeRepository.Expect(a => a.Queryable).Return(PriorityTypes.AsQueryable()).Repeat.Any();

            var result = Controller.Create(2)
                .AssertViewRendered()
                .WithViewData<RequirementViewModel>();
            Assert.AreEqual(1, result.RequirementTypes.Count());
            Assert.AreEqual(2, result.Categories.Count());
            Assert.AreEqual(3, result.PriorityTypes.Count());
        }

        /// <summary>
        /// Tests the create when project id is not found does not save.
        /// </summary>
        [TestMethod]
        public void TestCreateWhenProjectIdIsNotFoundDoesNotSave()
        {
            ProjectRepository.Expect(a => a.GetNullableById(1)).Return(null).Repeat.Any();
            Controller.Create(new Requirement(), 1)
                .AssertActionRedirect()
                .ToAction<ProjectController>(a => a.Index());
            Assert.AreEqual("Project was not found.", Controller.Message);
            RequirementRepository.AssertWasNotCalled(a => a.EnsurePersistent(Arg<Requirement>.Is.Anything));
        }


        /// <summary>
        /// Tests the create assigns found project id and saves.
        /// </summary>
        [TestMethod]
        public void TestCreateAssignsFoundProjectIdAndSaves()
        {
            Controller.ControllerContext.HttpContext.Response.Expect(a => a.ApplyAppPathModifier(null)).IgnoreArguments()
                .Return("http://Test.com/").Repeat.Any();
            Controller.Url = MockRepository.GenerateStub<UrlHelper>(Controller.ControllerContext.RequestContext);

            FakeProjects(Projects, 3);
            ProjectRepository.Expect(a => a.GetNullableById(2)).Return(Projects[1]).Repeat.Any();
            var requirement = CreateValidEntities.Requirement(1);
            requirement.Project = null;

            var result = Controller.Create(requirement, 2)
                .AssertHttpRedirect();
            Assert.AreEqual("http://Test.com/#tab-2", result.Url);
            RequirementRepository.AssertWasCalled(a => a.EnsurePersistent(requirement));
            Assert.AreSame(requirement.Project, Projects[1]);
            Assert.AreEqual("Requirement has been created successfully.", Controller.Message);
        }


        /// <summary>
        /// Tests the create with invalid data does not save.
        /// </summary>
        [TestMethod]
        public void TestCreateWithInvalidDataDoesNotSave()
        {
            Controller.ControllerContext.HttpContext.Response.Expect(a => a.ApplyAppPathModifier(null)).IgnoreArguments()
                .Return("http://Test.com/").Repeat.Any();
            Controller.Url = MockRepository.GenerateStub<UrlHelper>(Controller.ControllerContext.RequestContext);

            FakeProjects(Projects, 3);
            FakeRequirementTypes(RequirementTypes, 1);
            FakeCategories(Categories, 2);
            FakePriorityTypes(PriorityTypes, 3);

            RequirementTypeRepository.Expect(a => a.Queryable).Return(RequirementTypes.AsQueryable()).Repeat.Any();
            CategoryRepository.Expect(a => a.Queryable).Return(Categories.AsQueryable()).Repeat.Any();
            PriorityTypeRepository.Expect(a => a.Queryable).Return(PriorityTypes.AsQueryable()).Repeat.Any();
            ProjectRepository.Expect(a => a.GetNullableById(2)).Return(Projects[1]).Repeat.Any();

            var requirement = CreateValidEntities.Requirement(1);
            requirement.Project = null;
            requirement.Description = " "; //Invalid

            Controller.Create(requirement, 2)
                .AssertViewRendered()
                .WithViewData<RequirementViewModel>();
  
            RequirementRepository.AssertWasNotCalled(a => a.EnsurePersistent(Arg<Requirement>.Is.Anything));
            Assert.AreSame(requirement.Project, Projects[1]);
            Assert.AreNotEqual("Requirement has been created successfully.", Controller.Message);
            Controller.ModelState.AssertErrorsAre("Description: may not be null or empty");
        }

        #endregion Create Tests

        #region Edit Tests

        /// <summary>
        /// Tests the edit with one parameter redirects to project index when id not found.
        /// </summary>
        [TestMethod]
        public void TestEditWithOneParameterRedirectsToProjectIndexWhenIdNotFound()
        {
            RequirementRepository.Expect(a => a.GetNullableById(1)).Return(null).Repeat.Any();
            Controller.Edit(1)
                .AssertActionRedirect()
                .ToAction<ProjectController>(a => a.Index());
            Assert.AreEqual("Requirement was not found.", Controller.Message); 
        }

        /// <summary>
        /// Tests the edit with one parameter returns view when id is found.
        /// </summary>
        [TestMethod]
        public void TestEditWithOneParameterReturnsViewWhenIdIsFound()
        {
            FakeProjects(Projects, 3);
            FakeRequirementTypes(RequirementTypes, 1);
            FakeCategories(Categories, 2);
            FakePriorityTypes(PriorityTypes, 3);
            FakeRequirements(Requirements, 1);

            RequirementTypeRepository.Expect(a => a.Queryable).Return(RequirementTypes.AsQueryable()).Repeat.Any();
            CategoryRepository.Expect(a => a.Queryable).Return(Categories.AsQueryable()).Repeat.Any();
            PriorityTypeRepository.Expect(a => a.Queryable).Return(PriorityTypes.AsQueryable()).Repeat.Any();
            ProjectRepository.Expect(a => a.GetNullableById(2)).Return(Projects[1]).Repeat.Any();
            RequirementRepository.Expect(a => a.GetNullableById(1)).Return(Requirements[0]).Repeat.Any();

            var result = Controller.Edit(1)
                .AssertViewRendered()
                .WithViewData<RequirementViewModel>();
            Assert.AreSame(Requirements[0], result.Requirement);
        }

        [TestMethod]
        public void TestEditWithValidDataSavesAndOnlyCopiesExpectedFields()
        {
            Controller.ControllerContext.HttpContext.Response.Expect(a => a.ApplyAppPathModifier(null)).IgnoreArguments()
                .Return("http://Test.com/").Repeat.Any();
            Controller.Url = MockRepository.GenerateStub<UrlHelper>(Controller.ControllerContext.RequestContext);

            FakeRequirements(Requirements, 3);
            RequirementRepository.Expect(a => a.GetNullableById(2)).Return(Requirements[1]).Repeat.Any();
            var requirementUpdateValues = CreateValidEntities.Requirement(99);
            requirementUpdateValues.Description = "DescriptionUpdated";
            requirementUpdateValues.RequirementType = CreateValidEntities.RequirementType(99);
            requirementUpdateValues.IsComplete = !Requirements[1].IsComplete;
            requirementUpdateValues.Category = CreateValidEntities.Category(99);
            requirementUpdateValues.PriorityType = CreateValidEntities.PriorityType(99);
            requirementUpdateValues.Project = CreateValidEntities.Project(99); //This one will not be updated
            requirementUpdateValues.TechnicalDifficulty = 99;

            Assert.AreNotEqual(Requirements[1].Description, requirementUpdateValues.Description);
            Assert.AreNotSame(Requirements[1].RequirementType, requirementUpdateValues.RequirementType);
            Assert.AreNotEqual(Requirements[1].IsComplete, requirementUpdateValues.IsComplete);
            Assert.AreNotSame(Requirements[1].Category, requirementUpdateValues.Category);
            Assert.AreNotSame(Requirements[1].PriorityType, requirementUpdateValues.PriorityType);
            Assert.AreNotSame(Requirements[1].Project, requirementUpdateValues.Project);
            Assert.AreNotEqual(Requirements[1].TechnicalDifficulty, requirementUpdateValues.TechnicalDifficulty);

            var result = Controller.Edit(2, requirementUpdateValues)
                .AssertHttpRedirect();

            Assert.AreEqual("http://Test.com/#tab-2", result.Url);
            RequirementRepository.AssertWasCalled(a => a.EnsurePersistent(Requirements[1]));
            Assert.AreEqual("Requirement has been updated successfully.", Controller.Message);

            Assert.AreEqual(Requirements[1].Description, requirementUpdateValues.Description);
            Assert.AreSame(Requirements[1].RequirementType, requirementUpdateValues.RequirementType);
            Assert.AreEqual(Requirements[1].IsComplete, requirementUpdateValues.IsComplete);
            Assert.AreSame(Requirements[1].Category, requirementUpdateValues.Category);
            Assert.AreSame(Requirements[1].PriorityType, requirementUpdateValues.PriorityType);
            Assert.AreNotSame(Requirements[1].Project, requirementUpdateValues.Project);
            Assert.AreEqual(Requirements[1].TechnicalDifficulty, requirementUpdateValues.TechnicalDifficulty);
        }

        [TestMethod]
        public void TestEditWithInvalidDataDoesNotSave1()
        {
            FakeProjects(Projects, 3);
            FakeRequirementTypes(RequirementTypes, 1);
            FakeCategories(Categories, 2);
            FakePriorityTypes(PriorityTypes, 3);

            RequirementTypeRepository.Expect(a => a.Queryable).Return(RequirementTypes.AsQueryable()).Repeat.Any();
            CategoryRepository.Expect(a => a.Queryable).Return(Categories.AsQueryable()).Repeat.Any();
            PriorityTypeRepository.Expect(a => a.Queryable).Return(PriorityTypes.AsQueryable()).Repeat.Any();
            ProjectRepository.Expect(a => a.GetNullableById(2)).Return(Projects[1]).Repeat.Any();

            FakeRequirements(Requirements, 3);
            RequirementRepository.Expect(a => a.GetNullableById(2)).Return(Requirements[1]).Repeat.Any();
            var requirementUpdateValues = CreateValidEntities.Requirement(99);
            requirementUpdateValues.Description = " "; //Invalid
            requirementUpdateValues.RequirementType = CreateValidEntities.RequirementType(99);
            requirementUpdateValues.IsComplete = !Requirements[1].IsComplete;
            requirementUpdateValues.Category = CreateValidEntities.Category(99);
            requirementUpdateValues.PriorityType = CreateValidEntities.PriorityType(99);
            requirementUpdateValues.Project = CreateValidEntities.Project(99); //This one will not be updated
            requirementUpdateValues.TechnicalDifficulty = 99;

            Assert.AreNotEqual(Requirements[1].Description, requirementUpdateValues.Description);
            Assert.AreNotSame(Requirements[1].RequirementType, requirementUpdateValues.RequirementType);
            Assert.AreNotEqual(Requirements[1].IsComplete, requirementUpdateValues.IsComplete);
            Assert.AreNotSame(Requirements[1].Category, requirementUpdateValues.Category);
            Assert.AreNotSame(Requirements[1].PriorityType, requirementUpdateValues.PriorityType);
            Assert.AreNotSame(Requirements[1].Project, requirementUpdateValues.Project);
            Assert.AreNotEqual(Requirements[1].TechnicalDifficulty, requirementUpdateValues.TechnicalDifficulty);

            Controller.Edit(2, requirementUpdateValues)
                .AssertViewRendered()
                .WithViewData<RequirementViewModel>();

            RequirementRepository.AssertWasNotCalled(a => a.EnsurePersistent(Arg<Requirement>.Is.Anything));
            Assert.AreNotEqual("Requirement has been updated successfully.", Controller.Message);

            Assert.AreEqual(Requirements[1].Description, requirementUpdateValues.Description);
            Assert.AreSame(Requirements[1].RequirementType, requirementUpdateValues.RequirementType);
            Assert.AreEqual(Requirements[1].IsComplete, requirementUpdateValues.IsComplete);
            Assert.AreSame(Requirements[1].Category, requirementUpdateValues.Category);
            Assert.AreSame(Requirements[1].PriorityType, requirementUpdateValues.PriorityType);
            Assert.AreNotSame(Requirements[1].Project, requirementUpdateValues.Project);
            Assert.AreEqual(Requirements[1].TechnicalDifficulty, requirementUpdateValues.TechnicalDifficulty);
        }

        [TestMethod]
        public void TestEditWithInvalidDataDoesNotSave2()
        {
            FakeProjects(Projects, 1);
            FakeRequirementTypes(RequirementTypes, 1);
            FakePriorityTypes(PriorityTypes, 1);
            FakeCategories(Categories, 1);

            FakeRequirements(Requirements, 2);
            Requirements[0].Project = Projects[0];
            Requirements[0].RequirementType = RequirementTypes[0];
            Requirements[0].PriorityType = PriorityTypes[0];
            Requirements[0].Category = Categories[0];
            Requirements[0].SetIdTo(1);

            Requirements[1].Project = Projects[0];
            Requirements[1].RequirementType = RequirementTypes[0];
            Requirements[1].PriorityType = PriorityTypes[0];
            Requirements[1].Category = Categories[0];
            Requirements[1].SetIdTo(1);

            Requirements[1].Description = "";

            ProjectRepository.Expect(a => a.GetNullableById(1)).Return(Projects[0]).Repeat.Any();
            RequirementRepository.Expect(a => a.GetNullableById(1)).Return(Requirements[0]).Repeat.Any();

            CategoryRepository.Expect(a => a.Queryable).Return(Categories.AsQueryable()).Repeat.Any();
            PriorityTypeRepository.Expect(a => a.Queryable).Return(PriorityTypes.AsQueryable()).Repeat.Any();
            RequirementTypeRepository.Expect(a => a.Queryable).Return(RequirementTypes.AsQueryable()).Repeat.Any();


            var result = Controller.Edit(1, Requirements[1]);

            RequirementRepository.AssertWasNotCalled(a => a.EnsurePersistent(Arg<Requirement>.Is.Anything));
            ProjectRepository.AssertWasNotCalled(a => a.EnsurePersistent(Arg<Project>.Is.Anything));
            
        }

        #endregion Edit Tests

    }
}
