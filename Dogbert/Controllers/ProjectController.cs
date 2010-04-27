using System.Linq;
using System.Web.Mvc;
using Dogbert.Controllers.ViewModels;
using Dogbert.Core.Domain;
using MvcContrib.Attributes;
using UCDArch.Core.PersistanceSupport;
using UCDArch.Core.Utils;
using UCDArch.Web.ActionResults;
using UCDArch.Web.Attributes;
using UCDArch.Web.Controller;
using MvcContrib;
using UCDArch.Web.Helpers;
using UCDArch.Web.Validator;
using System;

namespace Dogbert.Controllers
{
    public class ProjectController : SuperController
    {


        private readonly IRepository<Project> _projectRepository;
        
        public ProjectController(IRepository<Project> projectRepository)
        {
            Check.Require(projectRepository != null);
            _projectRepository = projectRepository;
        }
        
        
        
        // GET: /Project/
        [HandleTransactionManually]
        public ActionResult Index()
        {
            var projects = _projectRepository.GetAll();
            return View(projects);
        }

        [AcceptPost]
        public ActionResult UpdateProjectPriority(int[] projects)
        {
            Project p;
            for (int i = 0; i < projects.Length; i++)
            {
                p = Repository.OfType<Project>().GetById(projects[i]);
                p.Priority = i + 1;
                Repository.OfType<Project>().EnsurePersistent(p);
            }
            return new JsonNetResult(true);
        }

        //GET: /Project/Create
        public ActionResult Create()
        {
            return View(ProjectViewModel.Create(Repository));
        }
        
        //POST: /Project/Create
        [AcceptPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Project project)
        {           
           //set project Status
            project.StatusCode = Repository.OfType<StatusCode>().Queryable.Where(p => p.Name == "Pending").FirstOrDefault();
            project.Deadline = project.ProjectedEnd;
            project.DateAdded = DateTime.Now;
            project.LastModified = DateTime.Now;
   

            project.TransferValidationMessagesTo(ModelState);
            if (ModelState.IsValid)
            {
                _projectRepository.EnsurePersistent(project);
                Message = "New Project Created Successfully";
                return RedirectToAction("Index");
            }
            else
            {
                _projectRepository.DbContext.RollbackTransaction();

                var viewModel = ProjectViewModel.Create(Repository);
                viewModel.Project = project;

                return View(viewModel);
            }
        }

    
        // GET: /Project/Edit/
        public ActionResult Edit(int id)
        {
            var existingProject = _projectRepository.GetNullableByID(id);

            if (existingProject == null) return RedirectToAction("Create");

            var viewModel = ProjectViewModel.CreateEdit(Repository);

            viewModel.Project = existingProject;

            return View(viewModel);
        }

        private static void TransferValuesTo(Project projectToUpdate, Project project)
        {
            projectToUpdate.Name = project.Name;
            projectToUpdate.ProjectType = project.ProjectType;
            projectToUpdate.Contact = project.Contact;
            projectToUpdate.ContactEmail = project.ContactEmail;
            projectToUpdate.Unit = project.Unit;
            projectToUpdate.Complexity = project.Complexity;
            projectToUpdate.ProjectedStart = project.ProjectedStart;
            //Deadline
            //ProjectManager
            //LeadProgrammer
            //Description
            projectToUpdate.LastModified = DateTime.Now;
   
        }

        // POST: /Project/Edit/5
        [AcceptPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Project project)
        {
            var projectToUpdate = _projectRepository.GetById(project.Id);
            TransferValuesTo(projectToUpdate, project);

            MvcValidationAdapter.TransferValidationMessagesTo(ModelState, projectToUpdate.ValidationResults());

            if (ModelState.IsValid)
            {
                _projectRepository.EnsurePersistent(projectToUpdate);
                Message = "Project edited successfully";
                return RedirectToAction("Index");
            }
            else
            {
                var viewModel = ProjectViewModel.Create(Repository);
                viewModel.Project = project;
                return View(viewModel);
            }
        }



    //---------------------------------------------------------------------------------------------------
    //Project Texts -------------------------------------------------------------------------------------
    //---------------------------------------------------------------------------------------------------
        // GET: /Project/EditText/
        public ActionResult EditText(int Id)
        {
            var existingProjectText = Repository.OfType<ProjectText>().GetNullableByID(Id);
         
            
            if (existingProjectText == null) return RedirectToAction("Create");//?Need to redirect to edit screen, but don't have projId.

            var viewModel = ProjectViewModel.CreateEditText(Repository);
            viewModel.ProjectText = existingProjectText;
            return View(viewModel);
        }

        // POST: /Project/EditText/
        [AcceptPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditText(int id, ProjectText projectText)
        {
            var pt = Repository.OfType<ProjectText>().GetNullableByID(id);

            TransferValuesTo(pt, projectText);

            MvcValidationAdapter.TransferValidationMessagesTo(ModelState, pt.ValidationResults());
            if (ModelState.IsValid)
            {
                Repository.OfType<ProjectText>().EnsurePersistent(pt);
                Message = "Project text edited successfully";

                var project = Repository.OfType<Project>().GetNullableByID(pt.Project.Id);
                return RedirectToAction("Edit", project);
               // return RedirectToAction("Edit", pt.Project.Id);  //?Need to redirect to edit screen, but don't have projId.
            }
            else
            {
                //Else what?
                return RedirectToAction("Index");  
            }
        }



        
        private static void TransferValuesTo(ProjectText projectTextToUpdate, ProjectText projectText)
        {
            projectTextToUpdate.TextType = projectText.TextType;
            projectTextToUpdate.Text = projectText.Text;
        }


        // POST: /Project/CreateText/
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Project Id</param>
        /// <param name="projectText"></param>
        [AcceptPost]
        public ActionResult CreateText(int projectId, [Bind(Exclude="Id")]ProjectText projectText)
        {
            var project = Repository.OfType<Project>().GetNullableByID(projectId);

            if (project == null)
            {
                return RedirectToAction("Index");
            }

            project.AddProjectTexts(projectText);

            MvcValidationAdapter.TransferValidationMessagesTo(ModelState, project.ValidationResults());

            if (project.ProjectTexts.Any(a => a.TextType == projectText.TextType))
            {
                ModelState.AddModelError("Text Type", "Text type already exists in this project");
            }

            if (ModelState.IsValid)
            {
                Repository.OfType<Project>().EnsurePersistent(project);
                //_projectRepository.EnsurePersistent(projectText);//which repository
                Message = "New Text Created Successfully";
                //return RedirectToAction("Index");
            }

            var viewModel = ProjectViewModel.CreateEdit(Repository);
            viewModel.Project = project;

            return this.RedirectToAction(a => a.Edit(projectId));
        }
   
    }
   
}
