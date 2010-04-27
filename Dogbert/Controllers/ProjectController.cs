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

            var viewModel = ProjectViewModel.Create(Repository);

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
       
   
    }
   
}
