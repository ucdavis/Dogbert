using System.Linq;
using System.Web.Mvc;
using Dogbert2.Core.Domain;
using Dogbert2.Models;
using UCDArch.Core.PersistanceSupport;

namespace Dogbert2.Controllers
{
    /// <summary>
    /// Controller for the TextType class
    /// </summary>
    public class TextTypeController : ApplicationController
    {
        private readonly IRepositoryWithTypedId<TextType, string> _textTypeRepository;

        public TextTypeController(IRepositoryWithTypedId<TextType, string> textTypeRepository)
        {
            _textTypeRepository = textTypeRepository;
        }

        //
        // GET: /TextType/
        public ActionResult Index()
        {
            var textTypeList = _textTypeRepository.Queryable;

            return View(textTypeList.ToList());
        }

        //
        // GET: /TextType/Create
        public ActionResult Create()
        {
			var viewModel = TextTypeViewModel.Create(Repository);
            
            return View(viewModel);
        } 

        //
        // POST: /TextType/Create
        [HttpPost]
        public ActionResult Create(TextType textType)
        {
            // check to make sure the id doesn't already exist

            if (ModelState.IsValid)
            {
                _textTypeRepository.EnsurePersistent(textType);

                Message = "TextType Created Successfully";

                return RedirectToAction("Index");
            }
			
            var viewModel = TextTypeViewModel.Create(Repository);
            viewModel.TextType = textType;

            return View(viewModel);
        }

        //
        // GET: /TextType/Edit/5
        public ActionResult Edit(string id)
        {
            var textType = _textTypeRepository.GetNullableById(id);

            if (textType == null) return RedirectToAction("Index");

			var viewModel = TextTypeViewModel.Create(Repository);
			viewModel.TextType = textType;

			return View(viewModel);
        }
        
        //
        // POST: /TextType/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, TextType textType)
        {
            var textTypeToEdit = _textTypeRepository.GetNullableById(id);

            if (textTypeToEdit == null) return RedirectToAction("Index");

            AutoMapper.Mapper.Map(textType, textTypeToEdit);

            if (ModelState.IsValid)
            {
                _textTypeRepository.EnsurePersistent(textTypeToEdit);

                Message = "TextType Edited Successfully";

                return RedirectToAction("Index");
            }
			
            var viewModel = TextTypeViewModel.Create(Repository);
            viewModel.TextType = textType;

            return View(viewModel);
        }
                
    }
}
