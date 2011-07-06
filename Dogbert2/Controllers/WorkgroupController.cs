using System;
using System.Linq;
using System.Web.Mvc;
using Dogbert2.Clients;
using Dogbert2.Core.Domain;
using Dogbert2.Filters;
using Dogbert2.Models;
using UCDArch.Core.PersistanceSupport;

namespace Dogbert2.Controllers
{
    /// <summary>
    /// Controller for the Workgroup class
    /// </summary>
    [AdminOnly]
    public class WorkgroupController : ApplicationController
    {
	    private readonly IRepository<Workgroup> _workgroupRepository;

        public WorkgroupController(IRepository<Workgroup> workgroupRepository)
        {
            _workgroupRepository = workgroupRepository;
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


        public ActionResult AddWorker(int id)
        {
            return View();
        }

        public ActionResult RemoveWorker(int id)
        {
            return View();
        }

    }
}
