using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Dogbert2.Core.Domain;
using Dogbert2.Filters;
using Dogbert2.Models;
using UCDArch.Core.PersistanceSupport;
using UCDArch.Web.ActionResults;

namespace Dogbert2.Controllers
{
    /// <summary>
    /// Controller for the TextType class
    /// </summary>
    [AdminOnly]
    public class SectionTypeController : ApplicationController
    {
        private readonly IRepositoryWithTypedId<SectionType, string> _textTypeRepository;

        public SectionTypeController(IRepositoryWithTypedId<SectionType, string> textTypeRepository)
        {
            _textTypeRepository = textTypeRepository;
        }

        //
        // GET: /TextType/
        public ActionResult Index()
        {
            var textTypeList = _textTypeRepository.Queryable.OrderBy(a=>a.Order);

            return View(textTypeList.ToList());
        }

        //
        // GET: /TextType/Create
        public ActionResult Create()
        {
			var viewModel = SectionTypeViewModel.Create(Repository);
            
            return View(viewModel);
        } 

        //
        // POST: /TextType/Create
        [HttpPost]
        public ActionResult Create(SectionType sectionType)
        {
            // check to make sure the id doesn't already exist

            if (ModelState.IsValid)
            {
                _textTypeRepository.EnsurePersistent(sectionType);

                Message = "TextType Created Successfully";

                return RedirectToAction("Index");
            }
			
            var viewModel = SectionTypeViewModel.Create(Repository);
            viewModel.SectionType = sectionType;

            return View(viewModel);
        }

        //
        // GET: /TextType/Edit/5
        public ActionResult Edit(string id)
        {
            var textType = _textTypeRepository.GetNullableById(id);

            if (textType == null) return RedirectToAction("Index");

			var viewModel = SectionTypeViewModel.Create(Repository);
			viewModel.SectionType = textType;

			return View(viewModel);
        }
        
        //
        // POST: /TextType/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, SectionType sectionType)
        {
            var textTypeToEdit = _textTypeRepository.GetNullableById(id);

            if (textTypeToEdit == null) return RedirectToAction("Index");

            AutoMapper.Mapper.Map(sectionType, textTypeToEdit);

            if (ModelState.IsValid)
            {
                _textTypeRepository.EnsurePersistent(textTypeToEdit);

                Message = "TextType Edited Successfully";

                return RedirectToAction("Index");
            }
			
            var viewModel = SectionTypeViewModel.Create(Repository);
            viewModel.SectionType = sectionType;

            return View(viewModel);
        }

        /// <summary>
        /// Updates the order of the text types (to be used for report generation)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonNetResult UpdateOrder(List<string> textTypes)
        {
            try
            {
                for (int i = 0; i < textTypes.Count; i++)
                {
                    var tt = _textTypeRepository.GetNullableById(textTypes[i]);

                    if (tt != null)
                    {
                        tt.Order = i;
                        _textTypeRepository.EnsurePersistent(tt);
                    }
                }

                return new JsonNetResult(true);
            }
            catch (Exception)
            {
                return new JsonNetResult(false);
            }
        }
    }
}
