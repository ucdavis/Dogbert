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
using System.Text.RegularExpressions;

namespace Dogbert.Controllers
{

    [Authorize(Roles = "User")]  
    public class ProjectController : SuperController
    {
        private readonly IRepository<Project> _projectRepository;
       // DateTime mindate = new DateTime(1999, 1, 1);
        //DateTime maxdate = new DateTime(9999, 12, 31);
   
        
        public ProjectController(IRepository<Project> projectRepository)
        {
            Check.Require(projectRepository != null);
            _projectRepository = projectRepository;
        }
        
        
        // GET: /Project/
        [HandleTransactionsManually]
        public ActionResult Index()
        {
            var projects = _projectRepository.GetAll();
            return View(projects);
        }

        public ActionResult DynamicIndex()
        {
            var viewModel = ProjectIndexViewModel.Create();

            viewModel.Projects = _projectRepository.GetAll();
            viewModel.ProjectTypes = Repository.OfType<ProjectType>().Queryable.Where(x => x.IsActive).ToList();

            return View(viewModel);
        }

        [AcceptPost]
        [BypassAntiForgeryToken]
        public ActionResult UpdateProjectPriority(int[] projects) 
        {
            Project p;
            for (int i = 0; i < projects.Length; i++)
            {
                p = Repository.OfType<Project>().GetByID(projects[i]);
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

            //Validate input:  Create and Edit
            if (project.ProjectedStart.HasValue &&
                !Regex.IsMatch(project.ProjectedStart.Value.ToShortDateString(), @"((0?[1-9])|1[012])[- /.](0?[1-9]|[12][0-9]|3[01])[- /.](19|20)?\d\d"))
            {    //make sure date is valid format: (mm or m) /(dd or d) /(yy or YYYY)
                  ModelState.AddModelError("Project.ProjectedStart", "Invalid Date (format: mm/dd/yy)");
            }
            if (project.ProjectedEnd.HasValue &&
                !Regex.IsMatch(project.ProjectedEnd.Value.ToShortDateString(), @"((0?[1-9])|1[012])[- /.](0?[1-9]|[12][0-9]|3[01])[- /.](19|20)?\d\d"))
            {    //make sure date is valid format: (mm or m) /(dd or d) /(yy or YYYY)
                ModelState.AddModelError("Project.ProjectedEnd", "Invalid Date (format: mm/dd/yy)");
            }
            if (project.ProjectedEnd.HasValue && project.ProjectedStart.HasValue && project.ProjectedEnd < project.ProjectedStart)
            {
                ModelState.AddModelError("Project.ProjectedEnd", "End Date must be > Project Start Date");
            }
            if (!Regex.IsMatch(project.ContactEmail, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"))
            {
                ModelState.AddModelError("Project.ContactEmail", "Email format is invalid.");
            }

            
            if (ModelState.IsValid)
            {
                _projectRepository.EnsurePersistent(project);
                Message = "New Project Created Successfully";
                return RedirectToAction("DynamicIndex");
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
            viewModel.UseCaseSteps = Repository.OfType<UseCaseStep>().Queryable.Where(a => a.UseCase.Project == existingProject).ToList();
            return View(viewModel);
        }

        //Transfer Values: For Project/Edit 
        private static void TransferValuesTo(Project projectToUpdate, Project project)
        {
            projectToUpdate.Name = project.Name;
            projectToUpdate.ProjectType = project.ProjectType;
            projectToUpdate.Contact = project.Contact;
            projectToUpdate.ContactEmail = project.ContactEmail;
            projectToUpdate.Unit = project.Unit;
            projectToUpdate.Complexity = project.Complexity;
            projectToUpdate.ProjectedStart = project.ProjectedStart;
            projectToUpdate.StatusCode = project.StatusCode;
            projectToUpdate.LeadProgrammer = project.LeadProgrammer;
            projectToUpdate.ProjectManager = project.ProjectManager;
            projectToUpdate.ProjectedStart = project.ProjectedStart;
            projectToUpdate.ProjectedEnd= project.ProjectedEnd;
            //Deadline
            //Description
            projectToUpdate.LastModified = DateTime.Now;
   
        }

        // POST: /Project/Edit/5
        [ValidateAntiForgeryToken]
        [AcceptPost]
        public ActionResult Edit(Project project)
        {
            var projectToUpdate = _projectRepository.GetByID(project.Id);
            //TransferValuesTo(projectToUpdate, project);
            
            //Validate input:  Create and Edit
            if (project.ProjectedStart.HasValue &&
                !Regex.IsMatch(project.ProjectedStart.Value.ToShortDateString(), @"((0?[1-9])|1[012])[- /.](0?[1-9]|[12][0-9]|3[01])[- /.](19|20)?\d\d"))
            {    //make sure date is valid format: (mm or m) /(dd or d) /(yy or YYYY)
                ModelState.AddModelError("Project.ProjectedStart", "Invalid Date (format: mm/dd/yy)");
            }
            if (project.ProjectedEnd.HasValue &&
                !Regex.IsMatch(project.ProjectedEnd.Value.ToShortDateString(), @"((0?[1-9])|1[012])[- /.](0?[1-9]|[12][0-9]|3[01])[- /.](19|20)?\d\d"))
            {    //make sure date is valid format: (mm or m) /(dd or d) /(yy or YYYY)
                ModelState.AddModelError("Project.ProjectedEnd", "Invalid Date (format: mm/dd/yy)");
            }
            if (project.ProjectedEnd.HasValue && project.ProjectedStart.HasValue && project.ProjectedEnd < project.ProjectedStart)
            {
                ModelState.AddModelError("Project.ProjectedEnd", "End Date must be > Project Start Date");
            }
            if (!Regex.IsMatch(project.ContactEmail, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"))
            {
                ModelState.AddModelError("Project.ContactEmail", "Email format is invalid.");
            }
            //----------------------------

            if (ModelState.IsValid)
            {//if values are valid, transfer to project
                TransferValuesTo(projectToUpdate, project);

            }

            MvcValidationAdapter.TransferValidationMessagesTo(ModelState, projectToUpdate.ValidationResults()); 
        
            if (ModelState.IsValid)
            {
                _projectRepository.EnsurePersistent(projectToUpdate);
                Message = "Project edited successfully";
                return RedirectToAction("DynamicIndex");
            }
            else
            {
               // _projectRepository.DbContext.RollbackTransaction();
                var viewModel = ProjectViewModel.CreateEdit(Repository);
                viewModel.Project = projectToUpdate;
                viewModel.UseCaseSteps = Repository.OfType<UseCaseStep>().Queryable.Where(a => a.UseCase.Project == projectToUpdate).ToList();
                return View(viewModel);
            }
        }



    //--------------------------------------------------------------------------------------------------------
    //Project Texts
    //--------------------------------------------------------------------------------------------------------
        // GET: /Project/EditText/


        


  
    }
   
}
