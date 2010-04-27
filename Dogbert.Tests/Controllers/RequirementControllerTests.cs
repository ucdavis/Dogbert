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
        protected List<Category> Categories { get; set; }
        protected IRepository<Category> CategoryRepository { get; set; }
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

            Categories = new List<Category>();
            CategoryRepository = FakeRepository<Category>();
            Controller.Repository.Expect(a => a.OfType<Category>()).Return(CategoryRepository).Repeat.Any();

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
        public void TestIndexReturnsView()
        {
            Controller.Index()
                .AssertActionRedirect()
                .ToAction<ProjectController>(a => a.Index());
        }

        #endregion Index Tests

        #region Create Tests

        [TestMethod]
        public void TestCreateWhenIdNotFoundRedirectsToProjectIndex()
        {
            ProjectRepository.Expect(a => a.GetNullableByID(1)).Return(null).Repeat.Any();
            Controller.Create(1)
                .AssertActionRedirect()
                .ToAction<ProjectController>(a => a.Index());
            Assert.AreEqual("Project was not found.", Controller.Message);
        }


        [TestMethod]
        public void TestCreateWhenIdIsFound()
        {
            FakeProjects(Projects, 3);
            FakeRequirementTypes(RequirementTypes, 1);
            FakeCategories(Categories, 2);
            FakePriorityTypes(PriorityTypes, 3);
            ProjectRepository.Expect(a => a.GetNullableByID(2)).Return(Projects[1]).Repeat.Any();
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
        #endregion Create Tests

        #region Edit Tests

        [TestMethod]
        public void TestEditDataWithInvalidDataDoesNotSave()
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

            ProjectRepository.Expect(a => a.GetNullableByID(1)).Return(Projects[0]).Repeat.Any();
            RequirementRepository.Expect(a => a.GetNullableByID(1)).Return(Requirements[0]).Repeat.Any();

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
