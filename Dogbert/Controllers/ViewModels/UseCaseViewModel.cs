using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Dogbert.Core.Domain;
using Elmah;
using MvcContrib.FluentHtml.Elements;
using UCDArch.Core.PersistanceSupport;
using UCDArch.Core.Utils;

namespace Dogbert.Controllers.ViewModels
{
    public class UseCaseViewModel
    {
        //used for use cases
        public Project Project { get; set; }
        public UseCase UseCase { get; set; }
        public MultiSelectList Actors { get; set; }
        public UseCaseStep UseCaseStep { get; set; }
        public IList<RequirementCategory> RequirementCategories { get; set; }

        //for editRelateRequirements
        public MultiSelectList RelatedRequirements{ get; set; }

        public static UseCaseViewModel Create(IRepository repository, Project project)
        {
            var actor = repository.OfType<Actor>().Queryable.Where(a => a.IsActive);
       
            var viewModel = new UseCaseViewModel
            {
                //populate the stuff needed for use cases
                
                Actors = new MultiSelectList(actor, "Id", "Name"),
                RequirementCategories = repository.OfType<RequirementCategory>().Queryable.ToList(),
            //    UseCases = repository.OfType<UseCase>().Queryable.ToList(),
            //    Childeren = repository.OfType<UseCase>().Queryable.ToList(),
                Project = project

            };
            return viewModel;
        }

        public static UseCaseViewModel Edit(IRepository repository, Project project, int ucid)
        {
            var actor = repository.OfType<Actor>().Queryable.Where(a => a.IsActive);
            var usecase = repository.OfType<UseCase>().GetById(ucid);
          
            var viewModel = new UseCaseViewModel
            {
                //populate the stuff needed for use cases
                Actors = new MultiSelectList(actor, "Id", "Name", usecase.Actors.Select(ua => ua.Id)),
                RequirementCategories = repository.OfType<RequirementCategory>().Queryable.Where(a => a.Project.Id == project.Id).ToList(),
                //    UseCases = repository.OfType<UseCase>().Queryable.ToList(),
                //    Childeren = repository.OfType<UseCase>().Queryable.ToList(),
                Project = project

            };
            return viewModel;
        }

        public static UseCaseViewModel CreateRelatedRequirements(IRepository repository, int ucid)
        {
            var usecase = repository.OfType<UseCase>().GetById(ucid);
            var req = repository.OfType<Requirement>().Queryable.Where(a => a.UseCases.Contains(usecase));
            //var req = repository.OfType<Requirement>().Queryable.ToList();
            var project = repository.OfType<Project>().GetById(usecase.Project.Id);
           

            var viewModel = new UseCaseViewModel
            {
                //populate the stuff needed for relating a requirement
                RelatedRequirements = new MultiSelectList(req, "Id", "ShortDescription"),
                Project = project
            };
            return viewModel;
        }

    }
}
