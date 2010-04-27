using System.Collections.Generic;
using Dogbert.Core.Domain;
using Dogbert.Tests.Core.Helpers;
using MvcContrib.TestHelper;
using Rhino.Mocks;
using UCDArch.Core.PersistanceSupport;
using UCDArch.Testing;
using UCDArch.Web.Controller;

namespace Dogbert.Tests.Core
{
    public class ControllerTestBase<CT> : UCDArch.Testing.ControllerTestBase<CT> where CT : SuperController
    {

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
            base.Controller = new TestControllerBuilder().CreateController<CT>();
        }

        #region Fakes

        protected void FakeProjects(List<Project> entities, int entitiesToAdd)
        {
            var offSet = entities.Count;
            for (int i = 0; i < entitiesToAdd; i++)
            {
                entities.Add(CreateValidEntities.Project(offSet + i + 1));
                entities[offSet + i].SetIdTo(offSet + i + 1);
            }
        }

        protected void FakeRequirementTypes(List<RequirementType> entities, int entitiesToAdd)
        {
            var offSet = entities.Count;
            for (int i = 0; i < entitiesToAdd; i++)
            {
                entities.Add(CreateValidEntities.RequirementType(offSet + i + 1));
                entities[offSet + i].SetIdTo((offSet + i + 1).ToString());
            }
        }

        protected void FakeCategories(List<Category> entities, int entitiesToAdd)
        {
            var offSet = entities.Count;
            for (int i = 0; i < entitiesToAdd; i++)
            {
                entities.Add(CreateValidEntities.Category(offSet + i + 1));
                entities[offSet + i].SetIdTo(offSet + i + 1);
            }
        }

        protected void FakePriorityTypes(List<PriorityType> entities, int entitiesToAdd)
        {
            var offSet = entities.Count;
            for (int i = 0; i < entitiesToAdd; i++)
            {
                entities.Add(CreateValidEntities.PriorityType(offSet + i + 1));
                entities[offSet + i].SetIdTo(offSet + i + 1);
            }
        }

        protected void FakeRequirements(List<Requirement> entities, int entitiesToAdd)
        {
            var offSet = entities.Count;
            for (int i = 0; i < entitiesToAdd; i++)
            {
                entities.Add(CreateValidEntities.Requirement(offSet + i + 1));
                entities[offSet + i].SetIdTo(offSet + i + 1);
            }
        }


        #endregion Fakes
    }
}
