using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Dogbert.Controllers.Helpers;
using Dogbert.Controllers.ViewModels;
using Dogbert.Core.Domain;
using Dogbert.Core.Resources;
using MvcContrib.Attributes;
using UCDArch.Web.Controller;
using MvcContrib;
using UCDArch.Web.Helpers;
using UCDArch.Web.Validator;

namespace Dogbert.Controllers
{
    [Authorize(Roles = "User")]
    public class UseCaseController : SuperController
    {
        public ActionResult Index()
        {
            return this.RedirectToAction<ProjectController>(a => a.Index());
        }

        //
        // GET: /UseCase/
        /// </summary>
        /// <param name="id">Project Id to link to</param>
        /// <returns></returns>
        public ActionResult Create(int projectId)
        {
            var project = Repository.OfType<Project>().GetNullableByID(projectId);

            if (project == null)
            {
                Message = string.Format(NotificationMessages.STR_ObjectNotFound, "Project");
                return this.RedirectToAction<ProjectController>(a => a.DynamicIndex());
            }

            var viewModel = UseCaseViewModel.Create(Repository, project);
            return View(viewModel);
        }

      
        // POST: /CreateUseCase/
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Project Id</param>
        /// <param name="projectUseCase"></param>
        [AcceptPost]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Exclude = "Id")]UseCase useCase, int projectId)
        {
            var project = Repository.OfType<Project>().GetNullableByID(projectId);

            //set values
            useCase.DateAdded = DateTime.Now;
            useCase.LastModified = DateTime.Now;

            if (project == null)
            {
                return RedirectToAction("DynamicIndex");
            }

            //add actors
            //foreach (Actor i in useCase.Actors)
            //{
            //    useCase.AddActors(i);
            //}
        
            project.AddUseCase(useCase);
           
            
            MvcValidationAdapter.TransferValidationMessagesTo(ModelState, useCase.ValidationResults());

            if (ModelState.IsValid)
            {
                Repository.OfType<UseCase>().EnsurePersistent(useCase);
                //_projectRepository.EnsurePersistent(projectText);//which repository
                Message = "Use Case Created Successfully";
                return Redirect(Url.EditProjectUrl(projectId, StaticValues.Tab_UseCases));
            }
   
            var viewModel = UseCaseViewModel.Create(Repository, useCase.Project);
            viewModel.Project = useCase.Project;
         
            return View(viewModel);
        }

        // GET: /EditUseCase/
        public ActionResult Edit(int Id)
        {
            var existingUseCase = Repository.OfType<UseCase>().GetNullableByID(Id);
            var existingUCSteps = Repository.OfType<UseCaseStep>().GetNullableByID(existingUseCase.Id);

            if (existingUseCase == null) return RedirectToAction("Create");


            var viewModel = UseCaseViewModel.Create(Repository, existingUseCase.Project);
            viewModel.UseCase = existingUseCase;
            viewModel.UseCaseStep = existingUCSteps;
            return View(viewModel);
        }

        // POST: /Project/EditUseCase/
        [AcceptPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(int id, UseCase UseCase)
        {
            var uc = Repository.OfType<UseCase>().GetNullableByID(id);

            TransferValuesTo(uc, UseCase);

            MvcValidationAdapter.TransferValidationMessagesTo(ModelState, uc.ValidationResults());

            var proj = Repository.OfType<Project>().GetNullableByID(uc.Project.Id);

            if (UseCase.Actors != null && UseCase.Actors.Count > 0)
            {
                uc.Actors.Clear();    //clear list of actors
                foreach (Actor i in UseCase.Actors) //update list of actors
                {
                    uc.AddActors(i);
                }
            }

   

            if (ModelState.IsValid)
            {
                Repository.OfType<UseCase>().EnsurePersistent(uc);
                Message = "Use Case edited successfully";
                return Redirect(Url.EditProjectUrl(uc.Project.Id, StaticValues.Tab_UseCases));

            }
            var project = Repository.OfType<Project>().GetNullableByID(uc.Project.Id);
            return RedirectToAction("Edit", project);
            // return RedirectToAction("Edit", pt.Project.Id);  //Redirect to edit page

        }

        //Transfer Values: For  /Project/EditUseCase/
        private static void TransferValuesTo(UseCase UseCaseToUpdate, UseCase UseCase)
        {
            UseCaseToUpdate.Name = UseCase.Name;
            UseCaseToUpdate.Description = UseCase.Description;
            UseCaseToUpdate.Precondition = UseCase.Precondition;
            UseCaseToUpdate.Postcondition = UseCase.Postcondition;
            UseCaseToUpdate.RequirementCategory = UseCase.RequirementCategory;

            UseCaseToUpdate.LastModified = DateTime.Now;
        }


        // GET: /EditUseCase/CreateUseCaseStep
        /// </summary>
        /// <param name="id">Use Case ID to link to</param>
        /// <returns></returns>
        public ActionResult CreateUseCaseStep(int useCaseId)
        {
            var useCase = Repository.OfType<UseCase>().GetNullableByID(useCaseId);
            if (useCase == null)
            {
                Message = string.Format(NotificationMessages.STR_ObjectNotFound, "UseCase");
                return this.RedirectToAction<ProjectController>(a => a.DynamicIndex());
            }

            var viewModel = UseCaseViewModel.Create(Repository, useCase.Project);
            return View(viewModel);
        }

        // POST: /EditUseCase/CreateUseCaseStep
        [AcceptPost]
        [ValidateInput(false)]
        public ActionResult CreateUseCaseStep(int useCaseId, [Bind(Exclude = "Id")]UseCaseStep useCaseStep)
        {
            var useCase = Repository.OfType<UseCase>().GetNullableByID(useCaseId);

            //set values
            useCaseStep.DateAdded = DateTime.Now;
            useCaseStep.LastModified = DateTime.Now;

            if (useCase == null)
            {
                return RedirectToAction("DynamicIndex");
            }

            useCase.AddSteps(useCaseStep);

            MvcValidationAdapter.TransferValidationMessagesTo(ModelState, useCaseStep.ValidationResults());

            if (ModelState.IsValid)
            {
                Repository.OfType<UseCaseStep>().EnsurePersistent(useCaseStep);
                //_projectRepository.EnsurePersistent(projectText);//which repository
                Message = "Use Case Step Created Successfully";
                //return RedirectToAction("DynamicIndex");
            }

            var viewModel = UseCaseViewModel.Create(Repository, useCase.Project);
            viewModel.UseCase = useCase;
            return this.RedirectToAction(a => a.Edit(useCaseId));
        }

        // GET: /EditUseCase/EditUseCaseSteps/
        public ActionResult EditUseCaseSteps(int Id)
        {
            var existingUCSteps = Repository.OfType<UseCaseStep>().GetNullableByID(Id);

            if (existingUCSteps == null) return RedirectToAction("Create");//?Need to redirect to edit screen, but don't have projId.

            var viewModel = UseCaseViewModel.Create(Repository, existingUCSteps.UseCase.Project);
            viewModel.UseCaseStep = existingUCSteps;
            return View(viewModel);
        }

        // POST: /EditUseCase/EditUseCaseSteps/
        [AcceptPost]
        [ValidateInput (false)]
        public ActionResult EditUseCaseSteps(int Id, UseCaseStep useCaseStep)
        {
            var existingUCS = Repository.OfType<UseCaseStep>().GetNullableByID(Id);

            TransferValuesTo(existingUCS, useCaseStep);

            MvcValidationAdapter.TransferValidationMessagesTo(ModelState, existingUCS.ValidationResults());

            var uc = Repository.OfType<UseCase>().GetNullableByID(existingUCS.UseCase.Id);


            if (ModelState.IsValid)
            {
                Repository.OfType<UseCaseStep>().EnsurePersistent(existingUCS);
                Message = "Use Case edited successfully";
                //return Redirect(Url.EditProjectUrl(existingUCS.UseCase.Project.Id, StaticValues.Tab_UseCases));
                return this.RedirectToAction(a => a.Edit(uc.Id));
                
            }
            var project = Repository.OfType<Project>().GetNullableByID(existingUCS.UseCase.Project.Id);
            return Redirect(Url.EditProjectUrl(existingUCS.UseCase.Project.Id, StaticValues.Tab_UseCases));
            
            // return RedirectToAction("Edit", pt.Project.Id);  //Redirect to edit page
        }

        //Transfer Values: For  /Project/EditUseCase/
        private static void TransferValuesTo(UseCaseStep StepToUpdate, UseCaseStep Step)
        {

            StepToUpdate.Order = Step.Order;
            StepToUpdate.Description = Step.Description;
            StepToUpdate.Optional = Step.Optional;
            StepToUpdate.LastModified =  DateTime.Now;

        }

        // GET: /EditUseCase/EditChildren
        public ActionResult EditChildren(int ucid)
        {
            var uc = Repository.OfType<UseCase>().GetNullableByID(ucid);
            if (uc == null)
            {
                Message = string.Format(NotificationMessages.STR_ObjectNotFound, "UseCase");
                return this.RedirectToAction<ProjectController>(a => a.DynamicIndex());
            }

            var viewModel = UseCaseViewModel.Create(Repository, uc.Project);
            viewModel.UseCase = uc;
            return View(viewModel);
        }

        // POST: /EditUseCase/EditChildren
        [AcceptPost]
        public ActionResult EditChildren(int ucid, UseCase UseCase)
        {
            var uc = Repository.OfType<UseCase>().GetNullableByID(ucid);

            if (UseCase.Children != null && UseCase.Children.Count > 0)
            {
                uc.Children.Clear();    //clear list of children
                foreach (UseCase i in UseCase.Children) //update list 
                {
                    uc.addChild (i);
                }
            }

            MvcValidationAdapter.TransferValidationMessagesTo(ModelState, uc.ValidationResults());
            if (ModelState.IsValid)
            {
                Repository.OfType<UseCase>().EnsurePersistent(uc);
                Message = "Use Case edited successfully";
                return this.RedirectToAction(a => a.Edit(uc.Id));
            }
           // var project = Repository.OfType<Project>().GetNullableByID(uc.Project.Id);
            return RedirectToAction("Edit", uc);
        
        }

        // GET: /EditUseCase/EditRelatedRequirements
        public ActionResult EditRelatedRequirements(int ucid)
        {
            var uc = Repository.OfType<UseCase>().GetNullableByID(ucid);
            if (uc == null)
            {
                Message = string.Format(NotificationMessages.STR_ObjectNotFound, "UseCase");
                return this.RedirectToAction<ProjectController>(a => a.DynamicIndex());
            }

            var viewModel = UseCaseViewModel.Create(Repository, uc.Project);
            viewModel.UseCase = uc;
            return View(viewModel);
        }


        // POST: /EditUseCase/EditRelatedRequirements
        [AcceptPost]
        public ActionResult EditRelatedRequirements(int ucid, UseCase UseCase)
        {
            var uc = Repository.OfType<UseCase>().GetNullableByID(ucid);

            if (UseCase.Requirements != null && UseCase.Requirements.Count > 0)
            {
                uc.Requirements.Clear();    //clear list of children
                foreach (Requirement i in UseCase.Requirements) //update list 
                {
                    uc.AddRequirement (i);
                }
            }

            MvcValidationAdapter.TransferValidationMessagesTo(ModelState, uc.ValidationResults());
            if (ModelState.IsValid)
            {
                Repository.OfType<UseCase>().EnsurePersistent(uc);
                Message = "Use Case edited successfully";
                return this.RedirectToAction(a => a.Edit(uc.Id));
            }
            // var project = Repository.OfType<Project>().GetNullableByID(uc.Project.Id);
            return RedirectToAction("Edit", uc);

        }
       
    }
}
