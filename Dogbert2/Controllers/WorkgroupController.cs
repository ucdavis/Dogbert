

using System;
using System.Linq;
using System.Web.Mvc;
using UCDArch.Core.PersistanceSupport;
using UCDArch.Core.Utils;

namespace Dogbert2.Controllers
{
    /// <summary>
    /// Controller for the Workgroup class
    /// </summary>
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
            var workgroupToCreate = new Workgroup();

            TransferValues(workgroup, workgroupToCreate);

            if (ModelState.IsValid)
            {
                _workgroupRepository.EnsurePersistent(workgroupToCreate);

                Message = "Workgroup Created Successfully";

                return RedirectToAction("Index");
            }
            else
            {
				var viewModel = WorkgroupViewModel.Create(Repository);
                viewModel.Workgroup = workgroup;

                return View(viewModel);
            }
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

            TransferValues(workgroup, workgroupToEdit);

            if (ModelState.IsValid)
            {
                _workgroupRepository.EnsurePersistent(workgroupToEdit);

                Message = "Workgroup Edited Successfully";

                return RedirectToAction("Index");
            }
            else
            {
				var viewModel = WorkgroupViewModel.Create(Repository);
                viewModel.Workgroup = workgroup;

                return View(viewModel);
            }
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
        /// Transfer editable values from source to destination
        /// </summary>
        private static void TransferValues(Workgroup source, Workgroup destination)
        {
			//Recommendation: Use AutoMapper
			//Mapper.Map(source, destination)
            throw new NotImplementedException();
        }


    }

	/// <summary>
    /// ViewModel for the Workgroup class
    /// </summary>
    public class WorkgroupViewModel
	{
		public Workgroup Workgroup { get; set; }
 
		public static WorkgroupViewModel Create(IRepository repository)
		{
			Check.Require(repository != null, "Repository must be supplied");
			
			var viewModel = new WorkgroupViewModel {Workgroup = new Workgroup()};
 
			return viewModel;
		}
	}
}
