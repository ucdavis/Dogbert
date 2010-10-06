using System.Collections.Generic;
using Dogbert.Core.Domain;
using Dogbert.Tests.Core.Helpers;
using MvcContrib.TestHelper;
using UCDArch.Testing;
using UCDArch.Web.Controller;

namespace Dogbert.Tests.Core
{
    public class ControllerTestBase<TCt> : UCDArch.Testing.ControllerTestBase<TCt> where TCt : SuperController
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
            // ReSharper disable RedundantBaseQualifier
            base.Controller = new TestControllerBuilder().CreateController<TCt>();
            // ReSharper restore RedundantBaseQualifier
        }

        #region Fakes

        protected virtual void FakeProjects(List<Project> entities, int entitiesToAdd)
        {
            var offSet = entities.Count;
            for (int i = 0; i < entitiesToAdd; i++)
            {
                entities.Add(CreateValidEntities.Project(offSet + i + 1));
                entities[offSet + i].SetIdTo(offSet + i + 1);
            }
        }

        protected virtual void FakeRequirementTypes(List<RequirementType> entities, int entitiesToAdd)
        {
            var offSet = entities.Count;
            for (int i = 0; i < entitiesToAdd; i++)
            {
                entities.Add(CreateValidEntities.RequirementType(offSet + i + 1));
                entities[offSet + i].SetIdTo((offSet + i + 1).ToString());
            }
        }

        protected virtual void FakeCategories(List<RequirementCategory> entities, int entitiesToAdd)
        {
            var offSet = entities.Count;
            for (int i = 0; i < entitiesToAdd; i++)
            {
                entities.Add(CreateValidEntities.Category(offSet + i + 1));
                entities[offSet + i].SetIdTo(offSet + i + 1);
            }
        }

        protected virtual void FakePriorityTypes(List<PriorityType> entities, int entitiesToAdd)
        {
            var offSet = entities.Count;
            for (int i = 0; i < entitiesToAdd; i++)
            {
                entities.Add(CreateValidEntities.PriorityType(offSet + i + 1));
                entities[offSet + i].SetIdTo(offSet + i + 1);
            }
        }

        protected virtual void FakeRequirements(List<Requirement> entities, int entitiesToAdd)
        {
            var offSet = entities.Count;
            for (int i = 0; i < entitiesToAdd; i++)
            {
                entities.Add(CreateValidEntities.Requirement(offSet + i + 1));
                entities[offSet + i].SetIdTo(offSet + i + 1);
            }
        }

        /// <summary>
        /// Fakes the file types.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <param name="entitiesToAdd">The entities to add.</param>
        protected virtual void FakeFileTypes(List<FileType> entities, int entitiesToAdd)
        {
            var offSet = entities.Count;
            for (int i = 0; i < entitiesToAdd; i++)
            {
                entities.Add(CreateValidEntities.FileType(offSet + i + 1));
                entities[offSet + i].SetIdTo(offSet + i + 1);
            }
        }

        /// <summary>
        /// Fakes the text types.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <param name="entitiesToAdd">The entities to add.</param>
        protected virtual void FakeTextTypes(List<TextType> entities, int entitiesToAdd)
        {
            var offSet = entities.Count;
            for (int i = 0; i < entitiesToAdd; i++)
            {
                entities.Add(CreateValidEntities.TextType(offSet + i + 1));
                entities[offSet + i].SetIdTo((offSet + i + 1).ToString());
            }
        }

        /// <summary>
        /// Fakes the project files.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <param name="entitiesToAdd">The entities to add.</param>
        protected virtual void FakeProjectFiles(List<ProjectFile> entities, int entitiesToAdd)
        {
            var offSet = entities.Count;
            for (int i = 0; i < entitiesToAdd; i++)
            {
                entities.Add(CreateValidEntities.ProjectFile(offSet + i + 1));
                entities[offSet + i].SetIdTo(offSet + i + 1);
            }
        }

        #endregion Fakes
    }
}
