using System;
using System.Collections.Generic;
using System.Linq;
using Dogbert.Core.Domain;
using UCDArch.Core.PersistanceSupport;
using UCDArch.Core.Utils;

namespace Dogbert.Controllers.ViewModels
{
    public class ProjectIndexViewModel
    {
        public static ProjectIndexViewModel Create()
        {
            return new ProjectIndexViewModel();
        }

        public IList<ProjectType> ProjectTypes { get; set; }
        public IList<Project> Projects { get; set; }
    }

    public class ProjectViewModel
    {
        // used for creating and editing
        public Project Project { get; set; }
        public IList<ProjectType> ProjectTypes { get; set; }
        public IList<User> Users { get; set; }
        public IList<StatusCode> StatusCode { get; set; }

        // used for editing
        public ProjectText ProjectText { get; set; }
        public IList<TextType> TextTypes { get; set; }
        public IList<RequirementCategory> RequirementCategories { get; set; }
        
        //used for requirements
        public Requirement Requirement { get; set; }
        public IList<RequirementType> RequirementTypes { get; set; }
        public IList<PriorityType> PriorityTypes { get; set; }

        ////used for use cases
        //public UseCase UseCase { get; set; }
        //public IList<UseCase> UseCases { get; set; }
        //public IList<Actor> Actors { get; set; }
        //public UseCaseStep UseCaseStep { get; set; }
        
        public static ProjectViewModel Create(IRepository repository)
        {
            return CreateBasic(repository);
        }

        public static ProjectViewModel CreateEdit(IRepository repository)
        {
            var viewModel = CreateBasic(repository);

            //populate the stuff needed for editing
            viewModel.TextTypes = repository.OfType<TextType>().Queryable.ToList();

            //populate the stuff needed for requirements
            viewModel.RequirementTypes= repository.OfType<RequirementType>().Queryable.Where(r => r.IsActive).ToList();
            viewModel.PriorityTypes = repository.OfType<PriorityType>().Queryable.Where(r => r.IsActive).ToList();
            //populate the stuff needed for use cases
           // viewModel.Actors = repository.OfType<Actor>().Queryable.ToList();
            viewModel.RequirementCategories = repository.OfType<RequirementCategory>().Queryable.Where(r =>r.IsActive).ToList();

            return viewModel;
        }

        public static ProjectViewModel CreateEditText(IRepository repository)
        {
            var viewModel = new ProjectViewModel
            {
                //populate the stuff needed for editing
                TextTypes = repository.OfType<TextType>().Queryable.ToList(),
     
            };
            return viewModel;
        }

        public static ProjectViewModel CreateEditRequirements(IRepository repository)
        {
            var viewModel =new ProjectViewModel
            {
                //populate the stuff needed for requirements
                RequirementTypes = repository.OfType<RequirementType>().Queryable.ToList(),
                PriorityTypes = repository.OfType<PriorityType>().Queryable .ToList()
            };
            return viewModel;
        }

        //public static ProjectViewModel CreateEditUseCase(IRepository repository)
        //{
        //    var viewModel = new ProjectViewModel
        //    {
        //        //populate the stuff needed for use cases
        //        Actors = repository.OfType<Actor>().Queryable.ToList(),
        //        RequirementCategories = repository.OfType<RequirementCategory>().Queryable.ToList()
              
        //    };
        //    return viewModel;
        //}

  
        
        private static ProjectViewModel CreateBasic(IRepository repository)
        {
            Check.Require(repository != null, "Repository is required");
            var viewModel = new ProjectViewModel();
                
                viewModel.ProjectTypes = repository.OfType<ProjectType>().Queryable.ToList();
                viewModel.Users = repository.OfType<User>().Queryable.Where(u => u.Inactive == false).ToList();
                viewModel.StatusCode = repository.OfType<StatusCode>().Queryable.Where(s => s.IsActive).ToList();
               //Where(p => p.ProjectType.Name == "Web Application")
            return viewModel;
        }

        
     
    }

  }





