using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using Dogbert.Controllers;
using Dogbert.Controllers.ViewModels;
using Dogbert.Core.Domain;
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
    public class RequirementControllerTests : ControllerTestBase<RequirementController>
    {
        #region Init

        /// <summary>
        /// Registers the routes.
        /// </summary>
        protected override void RegisterRoutes()
        {
            new RouteConfigurator().RegisterRoutes();
        }

        /// <summary>
        /// Setups the controller.
        /// </summary>
        protected override void SetupController()
        {
            Controller = new TestControllerBuilder().CreateController<RequirementController>();
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
            //"~/Requirement/Edit/5".ShouldMapTo<RequirementController>(a => a.Edit(5));
            Assert.Inconclusive("Need to create edit method");
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


        #endregion Index Tests

        #region Create Tests


        #endregion Create Tests

        #region Edit Tests


        #endregion Edit Tests

        #region Details Tests


        #endregion Details Tests
    }
}
