using System;
using System.Linq;
using System.Web.Mvc;
using Dogbert.Controllers.Helpers;
using Dogbert.Controllers.ViewModels;
using Dogbert.Core.Domain;
using Dogbert.Core.Resources;
using MvcContrib.Attributes;
using UCDArch.Core.PersistanceSupport;
using UCDArch.Core.Utils;
using UCDArch.Web.Controller;
using MvcContrib;
using UCDArch.Web.Helpers;
using UCDArch.Web.Validator;


namespace Dogbert.Controllers
{
    [Authorize(Roles = "User")]
    public class ActorController : SuperController
    {
        private readonly IRepository<Actor> _actorRepository;

         public ActorController(IRepository<Actor> actorRepository)
        {
            Check.Require(actorRepository != null);
            _actorRepository = actorRepository;
        }
        
        
        // GET: /Actor/Create
        public ActionResult Create(int projectId)
        {
            return View(ActorViewModel.Create(Repository, projectId));
        } 

        //
        // POST: /Actor/Create
        [AcceptPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int projectId, [Bind(Exclude = "Id, IsActive")] Actor actor)
        {
            actor.IsActive = true;
            actor.TransferValidationMessagesTo(ModelState);

            var actors = _actorRepository.GetAll().ToList();
            if (actors.Any(a => a.Name == actor.Name))
            {
                ModelState.AddModelError("Actor", "Actor already exists");
            }

            if (ModelState.IsValid)
            {

                _actorRepository.EnsurePersistent(actor);
                Message = "New Actor Created Successfully";
                return Redirect(Url.EditProjectUrl(projectId, StaticValues.Tab_Actors));
                
            }
            else
            {
                _actorRepository.DbContext.RollbackTransaction();
                var viewModel = ActorViewModel.Create(Repository, projectId);
                viewModel.Actor = actor;
                return View(viewModel);
            }

        }

        //
        // GET: /Actor/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Actor/Edit/5
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
