using System;
using System.Linq;
using System.Web.Mvc;
using Dogbert2.Core.Domain;
using UCDArch.Core.PersistanceSupport;
using UCDArch.Core.Utils;

namespace Dogbert2.Controllers
{
    /// <summary>
    /// Controller for the Worker class
    /// </summary>
    public class WorkerController : ApplicationController
    {
	    private readonly IRepository<Worker> _workerRepository;

        public WorkerController(IRepository<Worker> workerRepository)
        {
            _workerRepository = workerRepository;
        }
    
        //
        // GET: /Worker/
        public ActionResult Index()
        {
            var workerList = _workerRepository.Queryable.Where(a=>a.IsActive);

            return View(workerList.ToList());
        }


        //
        // GET: /Worker/Details/5
        public ActionResult Details(int id)
        {
            var worker = _workerRepository.GetNullableById(id);

            if (worker == null) return RedirectToAction("Index");

            return View(worker);
        }

        //
        // GET: /Worker/Create
        public ActionResult Create()
        {
			var viewModel = WorkerViewModel.Create(Repository);
            
            return View(viewModel);
        } 

        //
        // POST: /Worker/Create
        [HttpPost]
        public ActionResult Create(Worker worker)
        {
            // check if the user already exists
            var w = _workerRepository.Queryable.Where(a => a.LoginId == worker.LoginId).FirstOrDefault();
            if (w != null && w.IsActive)
            {
                ModelState.AddModelError("", string.Format("Worker with {0} login id already exists.", worker.LoginId));
            }
            else if (w != null)
            {
                w.IsActive = true;
                _workerRepository.EnsurePersistent(w);
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                _workerRepository.EnsurePersistent(worker);

                Message = "Worker Created Successfully";

                return RedirectToAction("Index");
            }

			var viewModel = WorkerViewModel.Create(Repository);
            viewModel.Worker = worker;

            return View(viewModel);
        }

        //
        // GET: /Worker/Edit/5
        public ActionResult Edit(int id)
        {
            var worker = _workerRepository.GetNullableById(id);

            if (worker == null) return RedirectToAction("Index");

			var viewModel = WorkerViewModel.Create(Repository);
			viewModel.Worker = worker;

			return View(viewModel);
        }
        
        //
        // POST: /Worker/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Worker worker)
        {
            var workerToEdit = _workerRepository.GetNullableById(id);

            if (workerToEdit == null) return RedirectToAction("Index");

            if (ModelState.IsValid)
            {
                _workerRepository.EnsurePersistent(workerToEdit);

                Message = "Worker Edited Successfully";

                return RedirectToAction("Index");
            }
            else
            {
				var viewModel = WorkerViewModel.Create(Repository);
                viewModel.Worker = worker;

                return View(viewModel);
            }
        }
        
        //
        // GET: /Worker/Delete/5 
        public ActionResult Delete(int id)
        {
			var worker = _workerRepository.GetNullableById(id);

            if (worker == null) return RedirectToAction("Index");

            return View(worker);
        }

        //
        // POST: /Worker/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Worker worker)
        {
			var workerToDelete = _workerRepository.GetNullableById(id);

            if (workerToDelete == null) return RedirectToAction("Index");

            workerToDelete.IsActive = false;
            _workerRepository.EnsurePersistent(workerToDelete);

            Message = "Worker Removed Successfully";

            return RedirectToAction("Index");
        }
        
    }

	/// <summary>
    /// ViewModel for the Worker class
    /// </summary>
    public class WorkerViewModel
	{
		public Worker Worker { get; set; }
 
		public static WorkerViewModel Create(IRepository repository)
		{
			Check.Require(repository != null, "Repository must be supplied");
			
			var viewModel = new WorkerViewModel {Worker = new Worker()};
 
			return viewModel;
		}
	}
}
