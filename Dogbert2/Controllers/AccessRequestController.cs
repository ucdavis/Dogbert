using System;
using System.Linq;
using System.Web.Mvc;
using Dogbert2.Core.Domain;
using Dogbert2.Filters;
using Dogbert2.Models;
using Dogbert2.Services;
using UCDArch.Core.PersistanceSupport;
using MvcContrib;

namespace Dogbert2.Controllers
{
    /// <summary>
    /// Controller for the AccessRequest class
    /// </summary>
    public class AccessRequestController : ApplicationController
    {
	    private readonly IRepository<AccessRequest> _accessRequestRepository;
        private readonly IDirectorySearchService _directorySearchService;

        public AccessRequestController(IRepository<AccessRequest> accessRequestRepository, IDirectorySearchService directorySearchService )
        {
            _accessRequestRepository = accessRequestRepository;
            _directorySearchService = directorySearchService;
        }

        //
        // GET: /AccessRequest/
        [AdminOnly]
        public ActionResult Index()
        {
            var accessRequestList = _accessRequestRepository.Queryable.OrderBy(a=>a.Pending);

            return View(accessRequestList.ToList());
        }

        [AdminOnly]
        public ActionResult Details(int id)
        {
            var accessRequest = _accessRequestRepository.GetNullableById(id);

            if (accessRequest == null) return RedirectToAction("Index");

            return View(accessRequest);
        }

        /// <summary>
        /// People just asking for access to application
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult Create()
        {
			var viewModel = AccessRequestViewModel.Create(Repository, _directorySearchService, CurrentUser.Identity.Name);
            
            return View(viewModel);
        } 

        /// <summary>
        /// People just asking for access to application
        /// </summary>
        /// <param name="accessRequest"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public ActionResult Create(AccessRequest accessRequest)
        {
            if (ModelState.IsValid)
            {
                _accessRequestRepository.EnsurePersistent(accessRequest);

                Message = "Your request has been submitted, we will contact you shortly.";

                return this.RedirectToAction<HomeController>(a => a.Index());
            }

			var viewModel = AccessRequestViewModel.Create(Repository, _directorySearchService, CurrentUser.Identity.Name);
            viewModel.AccessRequest = accessRequest;

            return View(viewModel);
        }
    }
}
