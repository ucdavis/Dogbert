using System;
using System.Linq;
using System.Web.Mvc;
using Dogbert2.App_GlobalResources;
using Dogbert2.Clients;
using Dogbert2.Core.Domain;
using Dogbert2.Filters;
using Dogbert2.Models;
using UCDArch.Core.PersistanceSupport;
using MvcContrib;
using UCDArch.Web.Helpers;

namespace Dogbert2.Controllers
{
    /// <summary>
    /// Controller for the Workgroup class
    /// </summary>
    [AdminOnly]
    public class WorkgroupController : ApplicationController
    {
	    private readonly IRepository<Workgroup> _workgroupRepository;
        private readonly IRepository<WorkgroupWorker> _workgroupWorkerRepository;

        public WorkgroupController(IRepository<Workgroup> workgroupRepository, IRepository<WorkgroupWorker> workgroupWorkerRepository)
        {
            _workgroupRepository = workgroupRepository;
            _workgroupWorkerRepository = workgroupWorkerRepository;
        }

        //
        // GET: /Workgroup/
        public ActionResult Index()
        {
            var workgroupList = _workgroupRepository.Queryable;

            return View(workgroupList.ToList());
        }


        //
        // GET: /Workgroup/Details/5
        public ActionResult Details(int id)
        {
            var workgroup = _workgroupRepository.GetNullableById(id);

            if (workgroup == null) return RedirectToAction("Index");

            return View(workgroup);
        }

        //
        // GET: /Workgroup/Create
        public ActionResult Create()
        {
			var viewModel = WorkgroupViewModel.Create(Repository);
            
            return View(viewModel);
        } 

        //
        // POST: /Workgroup/Create
        [HttpPost]
        public ActionResult Create(Workgroup workgroup)
        {
            if (ModelState.IsValid)
            {
                _workgroupRepository.EnsurePersistent(workgroup);

                Message = "Workgroup Created Successfully";

                return RedirectToAction("Index");
            }

            var viewModel = WorkgroupViewModel.Create(Repository);
            viewModel.Workgroup = workgroup;

            return View(viewModel);
        }

        //
        // GET: /Workgroup/Edit/5
        public ActionResult Edit(int id)
        {
            var workgroup = _workgroupRepository.GetNullableById(id);

            if (workgroup == null) return RedirectToAction("Index");

            var viewModel = WorkgroupViewModel.Create(Repository);
			viewModel.Workgroup = workgroup;

			return View(viewModel);
        }
        
        //
        // POST: /Workgroup/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Workgroup workgroup)
        {
            var workgroupToEdit = _workgroupRepository.GetNullableById(id);

            if (workgroupToEdit == null) return RedirectToAction("Index");

            AutoMapper.Mapper.Map(workgroup, workgroupToEdit);

            if (ModelState.IsValid)
            {
                _workgroupRepository.EnsurePersistent(workgroupToEdit);

                Message = "Workgroup Edited Successfully";

                return RedirectToAction("Index");
            }
            
            var viewModel = WorkgroupViewModel.Create(Repository);
            viewModel.Workgroup = workgroup;

            return View(viewModel);
        }
        
        //
        // GET: /Workgroup/Delete/5 
        public ActionResult Delete(int id)
        {
			var workgroup = _workgroupRepository.GetNullableById(id);

            if (workgroup == null) return RedirectToAction("Index");

            return View(workgroup);
        }

        //
        // POST: /Workgroup/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Workgroup workgroup)
        {
			var workgroupToDelete = _workgroupRepository.GetNullableById(id);

            if (workgroupToDelete == null) return RedirectToAction("Index");

            _workgroupRepository.Remove(workgroupToDelete);

            Message = "Workgroup Removed Successfully";

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Add a worker to a workgroup
        /// </summary>
        /// <param name="id">Workgroup Id</param>
        /// <returns></returns>
        public ActionResult AddWorker(int id)
        {
            var workgroup = _workgroupRepository.GetNullableById(id);
            if (workgroup == null) return this.RedirectToAction(a => a.Index());

            var viewModel = WorkgroupWorkerViewModel.Create(Repository, workgroup, CurrentUser.Identity.Name);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AddWorker(int id, WorkgroupWorker workgroupWorker)
        {
            var workgroup = _workgroupRepository.GetNullableById(id);
            if (workgroup == null) return this.RedirectToAction(a => a.Index());

            workgroupWorker.Workgroup = workgroup;

            ModelState.Clear();
            workgroupWorker.TransferValidationMessagesTo(ModelState);

            // check to make sure user isn't already part of workgroup
            if (_workgroupWorkerRepository.Queryable.Where(a => a.Workgroup == workgroup && a.Worker == workgroupWorker.Worker).Any())
            {
                ModelState.AddModelError("", "Worker is already part of workgroup.");
            }

            if (ModelState.IsValid)
            {
                _workgroupWorkerRepository.EnsurePersistent(workgroupWorker);

                Message = "Worker has been added to workgroup.";

                return this.RedirectToAction(a => a.Details(id));
            }

            var viewModel = WorkgroupWorkerViewModel.Create(Repository, workgroup, CurrentUser.Identity.Name);
            viewModel.WorkgroupWorker = workgroupWorker;
            return View(viewModel);
        }

        /// <summary>
        /// Removes a user from a workgroup
        /// </summary>
        /// <param name="id">Workgroup Worker Id</param>
        /// <returns></returns>
        public ActionResult RemoveWorker(int id)
        {
            var workgroupWorker = _workgroupWorkerRepository.GetNullableById(id);

            if (workgroupWorker == null)
            {
                Message = string.Format(Messages.NotFound, "Workgroup Worker", id);
                return this.RedirectToAction(a => a.Index());
            }

            return View(workgroupWorker);
        }

        [HttpPost]
        public ActionResult RemoveWorker(int id, WorkgroupWorker workgroupWorker)
        {
            workgroupWorker = _workgroupWorkerRepository.GetNullableById(id);

            if (workgroupWorker == null)
            {
                Message = string.Format(Messages.NotFound, "Workgroup Worker", id);
                return this.RedirectToAction(a => a.Index());
            }

            var workgroupId = workgroupWorker.Workgroup.Id;

            _workgroupWorkerRepository.Remove(workgroupWorker);

            Message = string.Format(Messages.Deleted, "Workgroup Worker");

            return this.RedirectToAction(a => a.Details(workgroupId));
        }
    }
}
