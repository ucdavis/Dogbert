using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Dogbert.Controllers;
using Dogbert.Core.Domain;
using Dogbert.Tests.Core.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcContrib.TestHelper;
using Rhino.Mocks;
using UCDArch.Core.PersistanceSupport;


namespace Dogbert.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest : Core.ControllerTestBase<HomeController>
    {
        protected List<Project> Projects { get; set; }
        protected IRepository<Project> ProjectRepository { get; set; }

        public HomeControllerTest()
        {
            Projects = new List<Project>();
            ProjectRepository = FakeRepository<Project>();
            Controller.Repository.Expect(a => a.OfType<Project>()).Return(ProjectRepository).Repeat.Any();
        }
        [TestMethod]
        public void Index()
        {
            // Arrange
            Projects = new List<Project>();
            for (int i = 0; i < 3; i++)
            {
                Projects.Add(CreateValidEntities.Project(i + 1));
            }
            ProjectRepository.Expect(a => a.Queryable).Return(Projects.AsQueryable()).Repeat.Any();
           
            // Act
            var result = Controller.Index()
                .AssertViewRendered()
                .WithViewData<IEnumerable<Project>>();

            // Assert
            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void About()
        {
            // Arrange


            // Act
            ViewResult result = Controller.About() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
