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

        /// <summary>
        /// Tests the mapping details.
        /// </summary>
        [TestMethod]
        public void TestMappingDetails()
        {
            //"~/Requirement/Details/5".ShouldMapTo<RequirementController>(a => a.Details(5));
            Assert.Inconclusive("Need to create details method");
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


        #endregion Edit Tests

        #region Details Tests


        #endregion Details Tests
    }
}
