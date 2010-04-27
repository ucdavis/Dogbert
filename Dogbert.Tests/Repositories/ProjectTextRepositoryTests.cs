using System.Linq;
using Dogbert.Core.Domain;
using Dogbert.Tests.Core;
using Dogbert.Tests.Core.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UCDArch.Core.PersistanceSupport;
using UCDArch.Data.NHibernate;

namespace Dogbert.Tests.Repositories
{
    /// <summary>
    /// Entity Name:		ProjectText
    /// LookupFieldName:	Text
    /// </summary>
    [TestClass]
    public class ProjectTextRepositoryTests : AbstractRepositoryTests<ProjectText, int>
    {
        /// <summary>
        /// Gets or sets the ProjectText repository.
        /// </summary>
        /// <value>The ProjectText repository.</value>
        public IRepository<ProjectText> ProjectTextRepository { get; set; }
		
        #region Init and Overrides

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectTextRepositoryTests"/> class.
        /// </summary>
        public ProjectTextRepositoryTests()
        {
            ProjectTextRepository = new Repository<ProjectText>();
        }

        /// <summary>
        /// Gets the valid entity of type T
        /// </summary>
        /// <param name="counter">The counter.</param>
        /// <returns>A valid entity of type T</returns>
        protected override ProjectText GetValid(int? counter)
        {
            var rtValue =  CreateValidEntities.ProjectText(counter);
            rtValue.Project = Repository.OfType<Project>().GetNullableByID(1);
            rtValue.TextType = TextTypeRepository.GetNullableByID("1");

            return rtValue;
        }

        /// <summary>
        /// A Query which will return a single record
        /// </summary>
        /// <param name="numberAtEnd"></param>
        /// <returns></returns>
        protected override IQueryable<ProjectText> GetQuery(int numberAtEnd)
        {
            return ProjectTextRepository.Queryable.Where(a => a.Text.EndsWith(numberAtEnd.ToString()));
        }

        /// <summary>
        /// A way to compare the entities that were read.
        /// For example, this would have the assert.AreEqual("Comment" + counter, entity.Comment);
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="counter"></param>
        protected override void FoundEntityComparison(ProjectText entity, int counter)
        {
            Assert.AreEqual("Text" + counter, entity.Text);
        }

        /// <summary>
        /// Updates , compares, restores.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="action">The action.</param>
        protected override void UpdateUtility(ProjectText entity, ARTAction action)
        {
            const string updateValue = "Updated";
            switch (action)
            {
                case ARTAction.Compare:
                    Assert.AreEqual(updateValue, entity.Text);
                    break;
                case ARTAction.Restore:
                    entity.Text = RestoreValue;
                    break;
                case ARTAction.Update:
                    RestoreValue = entity.Text;
                    entity.Text = updateValue;
                    break;
            }
        }

        /// <summary>
        /// Loads the data.
        /// </summary>
        protected override void LoadData()
        {
            LoadProjects(1);
            LoadTextTypes(1);
            ProjectTextRepository.DbContext.BeginTransaction();
            LoadRecords(5);
            ProjectTextRepository.DbContext.CommitTransaction();
        }

        #endregion Init and Overrides		
		
        
    }
}