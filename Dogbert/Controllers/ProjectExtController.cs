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
    public class ProjectExtController : SuperController
    {


        private readonly IRepository<ProjectText> _projectTextRepository;
        
        public ProjectExtController(IRepository<ProjectText> projectTextRepository)
        {
            Check.Require(projectTextRepository != null);
            _projectTextRepository = projectTextRepository;
        }
        
        
        
        //GET: /Project/Create
        public ActionResult Create()
        {
            return View(ProjectExtViewModel.Create(Repository));
        }
        
   
        // GET: /Project/Edit/
        public ActionResult Edit(int pid)
        {
            var existingProjectText = _projectTextRepository.GetNullableByID(pid);  //this is wrong!!
                //need to get by project Id, not by projectTextID
                //.Queryable.Where(p => p.Project.Id == pid);

            if (existingProjectText == null) return RedirectToAction("Create");

            var viewModel = ProjectExtViewModel.Create(Repository);

            viewModel.ProjectText = existingProjectText;

            return View(viewModel);
        }


        


   
    }
   
}
