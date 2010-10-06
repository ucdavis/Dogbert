using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Dogbert.Controllers;
using Dogbert.Controllers.ViewModels;
using Dogbert.Core.Abstractions;
using Dogbert.Core.Domain;
using Dogbert.Tests.Core.Extensions;
using Dogbert.Tests.Core.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcContrib.TestHelper;
using Rhino.Mocks;
using UCDArch.Core.PersistanceSupport;
using UCDArch.Testing.Fakes;


namespace Dogbert.Tests.Controllers
{
    [TestClass]
    public class ProjectFileControllerTests : Core.ControllerTestBase<ProjectFileController>
    {
        protected List<Project> Projects { get; set; }
        protected IRepository<Project> ProjectRepository { get; set; }
        protected List<FileType> FileTypes { get; set; }
        protected IRepository<FileType> FileTypeRepository { get; set; }
        protected List<TextType> TextTypes { get; set; } //LD added 10/06
        protected IRepository<TextType> TextTypeRepository { get; set; } //LD added 10/06
        protected List<ProjectFile> ProjectFiles { get; set; }
        protected IRepository<ProjectFile> ProjectFileRepository { get; set; }

        public ProjectFileControllerTests()
        {
            ProjectFiles = new List<ProjectFile>();
            ProjectFileRepository = FakeRepository<ProjectFile>();
            Controller.Repository.Expect(a => a.OfType<ProjectFile>()).Return(ProjectFileRepository).Repeat.Any();

            FileTypes = new List<FileType>();
            FileTypeRepository = FakeRepository<FileType>();
            Controller.Repository.Expect(a => a.OfType<FileType>()).Return(FileTypeRepository).Repeat.Any();

            TextTypes = new List<TextType>();
            TextTypeRepository = FakeRepository<TextType>();
            Controller.Repository.Expect(a => a.OfType<TextType>()).Return(TextTypeRepository).Repeat.Any();

            Projects = new List<Project>();
            ProjectRepository = FakeRepository<Project>();
            Controller.Repository.Expect(a => a.OfType<Project>()).Return(ProjectRepository).Repeat.Any();
        }

        #region Route Tests

        /// <summary>
        /// Tests mapping index.
        /// </summary>
        [TestMethod]
        public void TestMappingIndex()
        {
            "~/ProjectFile/Index".ShouldMapTo<ProjectFileController>(a => a.Index());
        }

        /// <summary>
        /// Tests the mapping create.
        /// </summary>
        [TestMethod]
        public void TestMappingCreateGet()
        {
            "~/ProjectFile/Create/3".ShouldMapTo<ProjectFileController>(a => a.Create(3), true);
        }

        /// <summary>
        /// Tests the mapping create.
        /// </summary>
        [TestMethod]
        public void TestMappingCreatePost()
        {
            "~/ProjectFile/Create/3".ShouldMapTo<ProjectFileController>(a => a.Create(new ProjectFile(), 3, null), true);
        }

        /// <summary>
        /// Tests the mapping edit get.
        /// </summary>
        [TestMethod]
        public void TestMappingEditGet()
        {
            "~/ProjectFile/Edit/3".ShouldMapTo<ProjectFileController>(a => a.Edit(3));   
        }

        /// <summary>
        /// Tests the mapping edit post.
        /// </summary>
        [TestMethod]
        public void TestMappingEditPost()
        {
            "~/ProjectFile/Edit/3".ShouldMapTo<ProjectFileController>(a => a.Edit(3, new ProjectFile(),null ),true);
        }

        /// <summary>
        /// Tests the mapping remove get.
        /// </summary>
        [TestMethod]
        public void TestMappingRemoveGet()
        {
            "~/ProjectFile/Remove/3".ShouldMapTo<ProjectFileController>(a => a.Remove(3)); 
        }

        [TestMethod]
        public void TestMappingRemovePost()
        {
            "~/ProjectFile/Remove/3".ShouldMapTo<ProjectFileController>(a => a.Remove(3, new ProjectFile()), true);
        }

        [TestMethod]
        public void TestMappingViewFile()
        {
            "~/ProjectFile/ViewFile/3".ShouldMapTo<ProjectFileController>(a => a.ViewFile(3));
        }
        #endregion Route Tests

        #region Index Tests

        /// <summary>
        /// Tests the index of the index redirects to project controller.
        /// </summary>
        [TestMethod]
        public void TestIndexRedirectsToProjectControllerIndex()
        {
            Controller.Index()
                .AssertActionRedirect()
                .ToAction<ProjectController>(a => a.Index());
        }

        #endregion Index Tests

        #region Create Tests

        /// <summary>
        /// Tests the create get redirects to project controller index when project id not found.
        /// </summary>
        [TestMethod]
        public void TestCreateGetRedirectsToProjectControllerIndexWhenProjectIdNotFound()
        {
            ProjectRepository.Expect(a => a.GetNullableByID(1)).Return(null).Repeat.Any();
            Controller.Create(1)
                .AssertActionRedirect()
                .ToAction<ProjectController>(a => a.Index());
            Assert.AreEqual("Project was not found.", Controller.Message);
        }

        /// <summary>
        /// Tests the create get returns view when project id is found.
        /// </summary>
        [TestMethod]
        public void TestCreateGetReturnsViewWhenProjectIdIsFound()
        {
            FakeProjects(Projects, 1);
            FakeFileTypes(FileTypes, 3);
            FakeTextTypes(TextTypes, 3);
            FileTypes[1].IsActive = false;
            TextTypes[1].IsActive = true;
            TextTypes[1].HasImage = true;  //else text type comes back null and causes error
            ProjectRepository.Expect(a => a.GetNullableByID(1)).Return(Projects[0]).Repeat.Any();
            FileTypeRepository.Expect(a => a.Queryable).Return(FileTypes.AsQueryable()).Repeat.Any();
            TextTypeRepository.Expect(a => a.Queryable).Return(TextTypes.AsQueryable()).Repeat.Any();

            var result = Controller.Create(1)
                .AssertViewRendered()
                .WithViewData<ProjectFileViewModel>();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.FileTypes.Count());
            Assert.IsNotNull(result.ProjectFile);
            Assert.AreSame(Projects[0], result.Project);
            foreach (var fileType in result.FileTypes.ToList())
            {
                Assert.IsTrue(fileType.IsActive); 
            }
        }

        /// <summary>
        /// Tests the create post redirects to project controller index when project id is not found.
        /// </summary>
        [TestMethod]
        public void TestCreatePostRedirectsToProjectControllerIndexWhenProjectIdIsNotFound()
        {
            ProjectRepository.Expect(a => a.GetNullableByID(1)).Return(null).Repeat.Any();
            Controller.Create(new ProjectFile(), 1, null)
                .AssertActionRedirect()
                .ToAction<ProjectController>(a => a.Index());
            Assert.AreEqual("Project was not found.", Controller.Message);
        }

        /// <summary>
        /// Tests the create post With valid data saves.
        /// </summary>
        [TestMethod]
        public void TestCreatePostWithValidDataSaves()
        {
            Controller.ControllerContext.HttpContext.Response.Expect(a => a.ApplyAppPathModifier(null))
                .IgnoreArguments().Return("http://Test.com/").Repeat.Any();
            Controller.Url = MockRepository.GenerateStub<UrlHelper>(Controller.ControllerContext
                .RequestContext);

            FakeProjects(Projects, 1);
            FakeFileTypes(FileTypes, 1);
            ProjectRepository.Expect(a => a.GetNullableByID(1)).Return(Projects[0]).Repeat.Any();

            var fakeUploadFile = new FakeHttpPostedFileBase("TestFile.jpg", "image/jpg", new byte[] { 9, 8, 7, 6, 5 });

            var projectFileToCreate = CreateValidEntities.ProjectFile(null);
            projectFileToCreate.FileName = null;
            projectFileToCreate.FileContents = null;
            projectFileToCreate.FileContentType = null;
            projectFileToCreate.Type = FileTypes[0];

            var result = Controller.Create(projectFileToCreate, 1, fakeUploadFile)
                .AssertHttpRedirect();

            Assert.AreEqual("http://Test.com/#tab-4", result.Url);
            ProjectFileRepository.AssertWasCalled(a => a.EnsurePersistent(projectFileToCreate));
            Assert.AreEqual("ProjectFile has been created successfully.", Controller.Message);
            Assert.IsTrue(Controller.ModelState.IsValid);
            Assert.AreSame(Projects[0], projectFileToCreate.Project);
            Assert.AreEqual("TestFile.jpg", projectFileToCreate.FileName);
            Assert.AreEqual("image/jpg", projectFileToCreate.FileContentType);
            Assert.AreEqual("98765", projectFileToCreate.FileContents.ByteArrayToString());
        }


        /// <summary>
        /// Tests the create post with invalid data does not save.
        /// </summary>
        [TestMethod]
        public void TestCreatePostWithInvalidDataDoesNotSave()
        {
    
            FakeProjects(Projects, 1);
            FakeFileTypes(FileTypes, 1);
            ProjectRepository.Expect(a => a.GetNullableByID(1)).Return(Projects[0]).Repeat.Any();
            FileTypeRepository.Expect(a => a.Queryable).Return(FileTypes.AsQueryable()).Repeat.Any();
            
            FakeTextTypes(TextTypes, 3);
            TextTypeRepository.Expect(a => a.Queryable).Return(TextTypes.AsQueryable()).Repeat.Any();

            //var fakeUploadFile = new FakeHttpPostedFileBase("TestFile.jpg", "image/jpg", new byte[] { 9, 8, 7, 6, 5 });

            var projectFileToCreate = CreateValidEntities.ProjectFile(null);
            projectFileToCreate.FileName = null;
            projectFileToCreate.FileContents = null;
            projectFileToCreate.FileContentType = null;
            projectFileToCreate.Type = FileTypes[0];
            projectFileToCreate.TextType = TextTypes[0];


            Controller.Create(projectFileToCreate, 1, null)
                .AssertViewRendered()
                .WithViewData<ProjectFileViewModel>();

            ProjectFileRepository.AssertWasNotCalled(a => a.EnsurePersistent(Arg<ProjectFile>.Is.Anything));
            Assert.AreNotEqual("ProjectFile has been created successfully.", Controller.Message);
            Assert.IsFalse(Controller.ModelState.IsValid);
            Assert.AreSame(Projects[0], projectFileToCreate.Project);
            Assert.AreNotEqual("TestFile.jpg", projectFileToCreate.FileName);
            Assert.AreNotEqual("image/jpg", projectFileToCreate.FileContentType);
            Assert.AreNotEqual("98765", projectFileToCreate.FileContents.ByteArrayToString());
            Controller.ModelState.AssertErrorsAre(
                "FileName: may not be null or empty",
                "FileContents: may not be empty",
                "FileContentType: may not be null or empty");
        }

        #endregion Create Tests

        #region Edit Tests

        /// <summary>
        /// Tests the edit get redirects to project controller index when project file id not found.
        /// </summary>
        [TestMethod]
        public void TestEditGetRedirectsToProjectControllerIndexWhenProjectFileIdNotFound()
        {
            ProjectFileRepository.Expect(a => a.GetNullableByID(1)).Return(null).Repeat.Any();
            Controller.Edit(1)
                .AssertActionRedirect()
                .ToAction<ProjectController>(a => a.Index());
            Assert.AreEqual("ProjectFile was not found.", Controller.Message);
        }

        /// <summary>
        /// Tests the edit get returns view when project file id is found.
        /// </summary>
        [TestMethod]
        public void TestEditGetReturnsViewWhenProjectFileIdIsFound()
        {
            FakeProjectFiles(ProjectFiles, 1);
            FakeProjects(Projects, 1);
            FakeFileTypes(FileTypes, 3);
            FileTypes[1].IsActive = false;
            ProjectFileRepository.Expect(a => a.GetNullableByID(1)).Return(ProjectFiles[0]).Repeat.Any();
            FileTypeRepository.Expect(a => a.Queryable).Return(FileTypes.AsQueryable()).Repeat.Any();

            ProjectFiles[0].Project = Projects[0];
            ProjectFiles[0].Type = FileTypes[0];

            var result = Controller.Edit(1)
                .AssertViewRendered()
                .WithViewData<ProjectFileViewModel>();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.FileTypes.Count());
            Assert.IsNotNull(result.ProjectFile);
            Assert.AreSame(Projects[0], result.Project);
            foreach (var fileType in result.FileTypes.ToList())
            {
                Assert.IsTrue(fileType.IsActive);
            }
        }

        /// <summary>
        /// Tests the edit post redirects to project controller when project file id is not found.
        /// </summary>
        [TestMethod]
        public void TestEditPostRedirectsToProjectControllerWhenProjectFileIdIsNotFound()
        {
            //Arrange
            ProjectFileRepository.Expect(a => a.GetNullableByID(1)).Return(null).Repeat.Any();

            //Act
            Controller.Edit(1, new ProjectFile(), null)
                .AssertActionRedirect()
                .ToAction<ProjectController>(a => a.Index());

            //Assert
            Assert.AreEqual("ProjectFile was not found.", Controller.Message);
        }


        /// <summary>
        /// Tests the edit post with valid data saves.
        /// </summary>
        [TestMethod]
        public void TestEditPostWithValidDataSaves()
        {
            #region Arrange
            Controller.ControllerContext.HttpContext.Response.Expect(a => a.ApplyAppPathModifier(null))
                .IgnoreArguments().Return("http://Test.com/").Repeat.Any();
            Controller.Url = MockRepository.GenerateStub<UrlHelper>(Controller.ControllerContext
                .RequestContext);

            FakeProjectFiles(ProjectFiles, 3);
            FakeProjects(Projects, 2);
            FakeFileTypes(FileTypes, 2);
            var fakeDate = new DateTime(2010, 02, 14, 08, 30, 01);
            SystemTime.Now = () => fakeDate;
            var dateAdded = new DateTime(2009, 01, 01);

            ProjectFiles[1].Project = Projects[0]; //Should not change
            ProjectFiles[1].Type = FileTypes[0];
            ProjectFiles[1].DateAdded = dateAdded; //Should not change

            var projectFileUpdate = CreateValidEntities.ProjectFile(99);
            projectFileUpdate.Project = Projects[1];
            projectFileUpdate.Type = FileTypes[1];
            projectFileUpdate.FileName = "Ugg.txt";
            projectFileUpdate.FileContentType = "unknown";
            projectFileUpdate.FileContents = new byte[]{1,2};

            var fakeUploadFile = new FakeHttpPostedFileBase("TestFile.jpg", "image/jpg", new byte[] { 9, 8, 7, 6, 5 });

            ProjectFileRepository.Expect(a => a.GetNullableByID(2)).Return(ProjectFiles[1]).Repeat.Any();
            #endregion Arrange

            #region Act

            var result = Controller.Edit(2, projectFileUpdate, fakeUploadFile)
                .AssertHttpRedirect();

            #endregion Act

            #region Assert
            Assert.AreEqual("http://Test.com/#tab-4", result.Url);
            ProjectFileRepository.AssertWasCalled(a => a.EnsurePersistent(ProjectFiles[1]));
            Assert.AreEqual("ProjectFile has been updated successfully.", Controller.Message);
            Assert.IsTrue(Controller.ModelState.IsValid);
            Assert.AreSame(Projects[0], ProjectFiles[1].Project);//Was not changed
            Assert.AreEqual("TestFile.jpg", ProjectFiles[1].FileName);//Was changed
            Assert.AreEqual("image/jpg", ProjectFiles[1].FileContentType);//Was changed
            Assert.AreEqual("98765", ProjectFiles[1].FileContents.ByteArrayToString());//Was changed
            Assert.AreEqual(fakeDate, ProjectFiles[1].DateChanged); //Was changed
            Assert.AreEqual(dateAdded, ProjectFiles[1].DateAdded); //Was not changed
            Assert.AreSame(FileTypes[1], ProjectFiles[1].Type); //Was Changed
            #endregion Assert
        }


        [TestMethod]
        public void TestEditPostWithInvalidDataDoesNotSave()
        {
            #region Arrange
            Controller.ControllerContext.HttpContext.Response.Expect(a => a.ApplyAppPathModifier(null))
                .IgnoreArguments().Return("http://Test.com/").Repeat.Any();
            Controller.Url = MockRepository.GenerateStub<UrlHelper>(Controller.ControllerContext
                .RequestContext);

            FakeProjectFiles(ProjectFiles, 3);
            FakeProjects(Projects, 2);
            FakeFileTypes(FileTypes, 2);
            var fakeDate = new DateTime(2010, 02, 14, 08, 30, 01);
            SystemTime.Now = () => fakeDate;
            var dateAdded = new DateTime(2009, 01, 01);

            ProjectFiles[1].Project = Projects[0]; //Should not change
            ProjectFiles[1].Type = FileTypes[0];
            ProjectFiles[1].DateAdded = dateAdded; //Should not change

            var projectFileUpdate = CreateValidEntities.ProjectFile(99);
            projectFileUpdate.Project = Projects[1];
            projectFileUpdate.Type = null;
            projectFileUpdate.FileName = "Ugg.txt";
            projectFileUpdate.FileContentType = "unknown";
            projectFileUpdate.FileContents = new byte[] { 1, 2 };

            var fakeUploadFile = new FakeHttpPostedFileBase("TestFile.jpg", "image/jpg", new byte[] { 9, 8, 7, 6, 5 });

            ProjectFileRepository.Expect(a => a.GetNullableByID(2)).Return(ProjectFiles[1]).Repeat.Any();
            FileTypeRepository.Expect(a => a.Queryable).Return(FileTypes.AsQueryable()).Repeat.Any();
            #endregion Arrange

            #region Act

            Controller.Edit(2, projectFileUpdate, fakeUploadFile)
                .AssertViewRendered()
                .WithViewData<ProjectFileViewModel>();

            #endregion Act

            #region Assert
            ProjectFileRepository.AssertWasNotCalled(a => a.EnsurePersistent(Arg<ProjectFile>.Is.Anything));
            Assert.AreNotEqual("ProjectFile has been updated successfully.", Controller.Message);
            Assert.IsFalse(Controller.ModelState.IsValid);
            Assert.AreSame(Projects[0], ProjectFiles[1].Project);//Was not changed
            Assert.AreEqual("TestFile.jpg", ProjectFiles[1].FileName);//Was changed
            Assert.AreEqual("image/jpg", ProjectFiles[1].FileContentType);//Was changed
            Assert.AreEqual("98765", ProjectFiles[1].FileContents.ByteArrayToString());//Was changed
            Assert.AreEqual(fakeDate, ProjectFiles[1].DateChanged); //Was changed
            Assert.AreEqual(dateAdded, ProjectFiles[1].DateAdded); //Was not changed
            Assert.AreNotSame(FileTypes[1], ProjectFiles[1].Type); //Was Changed
            Controller.ModelState.AssertErrorsAre("Type: may not be empty");
            #endregion Assert
        }

        #endregion Edit Tests


        #region Remove Tests

        [TestMethod]
        public void TestRemoveGetWhenIdIsNotFoundRedirectsToProjectControllerIndex()
        {
            #region Arrange

            ProjectFileRepository.Expect(a => a.GetNullableByID(1)).Return(null).Repeat.Any();

            #endregion Arrange

            #region Act

            Controller.Remove(1)
                .AssertActionRedirect()
                .ToAction<ProjectController>(a => a.Index());
            #endregion Act

            #region Assert
            Assert.AreEqual("ProjectFile was not found.", Controller.Message);
            #endregion Assert		
        }

        [TestMethod]
        public void TestRemoveGetReturnsViewWhenIdFound()
        {
            #region Arrange
            FakeProjectFiles(ProjectFiles, 1);
            FakeFileTypes(FileTypes, 1);
            ProjectFileRepository.Expect(a => a.GetNullableByID(1)).Return(ProjectFiles[0]).Repeat.Any();
            FileTypeRepository.Expect(a => a.Queryable).Return(FileTypes.AsQueryable()).Repeat.Any();

            #endregion Arrange

            #region Act
            var result = Controller.Remove(1)
                .AssertViewRendered()
                .WithViewData<ProjectFileViewModel>();

            #endregion Act

            #region Assert
            Assert.AreEqual(1, result.FileTypes.Count());
            #endregion Assert		
        }
        
        [TestMethod]
        public void TestRemovePostRedirectsToProjectControllerIndexWhenIdNotFound()
        {
            #region Arrange
            FakeProjectFiles(ProjectFiles, 1);
            ProjectFileRepository.Expect(a => a.GetNullableByID(1)).Return(null).Repeat.Any();
            
            #endregion Arrange

            #region Act
            Controller.Remove(1, ProjectFiles[0])
                .AssertActionRedirect()
                .ToAction<ProjectController>(a => a.Index());

            #endregion Act

            #region Assert
            Assert.AreEqual("ProjectFile was not found.", Controller.Message);

            #endregion Assert		
        }

        [TestMethod]
        public void TestRemovePostWhenIdFoundRedirectsToProjectControllerFilesTab()
        {
            #region Arrange
            Controller.ControllerContext.HttpContext.Response.Expect(a => a.ApplyAppPathModifier(null))
                .IgnoreArguments().Return("http://Test.com/").Repeat.Any();
            Controller.Url = MockRepository.GenerateStub<UrlHelper>(Controller.ControllerContext
                .RequestContext);

            FakeProjects(Projects, 3);
            FakeProjectFiles(ProjectFiles, 1);
            ProjectFiles[0].Project = Projects[1];
            ProjectFileRepository.Expect(a => a.GetNullableByID(1)).Return(ProjectFiles[0]).Repeat.Any();

            #endregion Arrange

            #region Act
            var result = Controller.Remove(1, ProjectFiles[0])
                .AssertHttpRedirect();

            #endregion Act

            #region Assert
            ProjectFileRepository.AssertWasCalled(a => a.Remove(ProjectFiles[0]));
            Assert.AreEqual("http://Test.com/#tab-4", result.Url);
            Assert.AreEqual("ProjectFile has been removed successfully.", Controller.Message);
            Assert.IsTrue(Controller.ModelState.IsValid);
            #endregion Assert		
        }

        [TestMethod]
        public void TestRemovePostWithInvalidDataWillStillRemove()
        {
            #region Arrange
            Controller.ControllerContext.HttpContext.Response.Expect(a => a.ApplyAppPathModifier(null))
                .IgnoreArguments().Return("http://Test.com/").Repeat.Any();
            Controller.Url = MockRepository.GenerateStub<UrlHelper>(Controller.ControllerContext
                .RequestContext);

            FakeProjects(Projects, 3);
            FakeProjectFiles(ProjectFiles, 1);
            ProjectFiles[0].Project = Projects[1];
            ProjectFiles[0].FileContents = null; //Invalid, but we don't care
            ProjectFileRepository.Expect(a => a.GetNullableByID(1)).Return(ProjectFiles[0]).Repeat.Any();

            #endregion Arrange

            #region Act
            var result = Controller.Remove(1, ProjectFiles[0])
                .AssertHttpRedirect();

            #endregion Act

            #region Assert
            ProjectFileRepository.AssertWasCalled(a => a.Remove(ProjectFiles[0]));
            Assert.AreEqual("http://Test.com/#tab-4", result.Url);
            Assert.AreEqual("ProjectFile has been removed successfully.", Controller.Message);
            Assert.IsTrue(Controller.ModelState.IsValid);
            #endregion Assert				
        }

        #endregion Remove Tests

        #region ViewFile Tests

        [TestMethod]
        [ExpectedException(typeof(UCDArch.Core.Utils.PreconditionException))]
        public void TestViewFileThrowsExceptionIfIdNotFound()
        {
            #region Arrange
            ProjectFileRepository.Expect(a => a.GetNullableByID(1)).Return(null).Repeat.Any();
            
            #endregion Arrange

            try
            {
                #region Act
                Controller.ViewFile(1);

                #endregion Act
            }
            catch (Exception ex)
            {
                #region Assert
                Assert.IsNotNull(ex);
                Assert.AreEqual("Invalid ProjectFile identifier", ex.Message);
                #endregion Assert
                throw;
            }  		
        }


        [TestMethod]
        public void TestViewFileReturnsFile()
        {
            #region Arrange

            FakeProjectFiles(ProjectFiles, 1);
            ProjectFiles[0].FileContents = new byte[]{1,2,3,4,5,6};
            ProjectFileRepository.Expect(a => a.GetNullableByID(1)).Return(ProjectFiles[0]).Repeat.Any();

            #endregion Arrange

            #region Act
            var result = Controller.ViewFile(1).AssertResultIs<FileResult>();

            #endregion Act

            #region Assert
            Assert.AreEqual("image/jpg", result.ContentType);
            #endregion Assert		
        }
        

        #endregion ViewFile Tests

    }


}
