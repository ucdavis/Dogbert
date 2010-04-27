using System;
using System.Collections.Generic;
using System.Linq;
using Dogbert.Core.Domain;
using UCDArch.Core.PersistanceSupport;
using UCDArch.Core.Utils;

namespace Dogbert.Controllers.ViewModels
{
    public class ProjectViewModel
    {
        // used for creating and editing
        public Project Project { get; set; }
        public IList<ProjectType> ProjectTypes { get; set; }
        public IList<User> Users { get; set; }

        // used for editing
        public ProjectText ProjectText { get; set; }
        public IList<TextType> TextTypes { get; set; }

        public static ProjectViewModel Create(IRepository repository)
        {
            return CreateBasic(repository);
        }

        public static ProjectViewModel CreateEdit(IRepository repository)
        {
            var viewModel = CreateBasic(repository);

            //populate the stuff needed for editing
            viewModel.TextTypes = repository.OfType<TextType>().Queryable.ToList();
            return viewModel;
        }


        public static ProjectViewModel CreateEditText(IRepository repository)
        {
            var viewModel = new ProjectViewModel
            {

                //populate the stuff needed for editing
                TextTypes = repository.OfType<TextType>().Queryable.ToList()
            };
            return viewModel;
        }



        private static ProjectViewModel CreateBasic(IRepository repository)
        {
            Check.Require(repository != null, "Repository is required");
            var viewModel = new ProjectViewModel
            {
                ProjectTypes = repository.OfType<ProjectType>().Queryable.ToList(),
                Users = repository.OfType<User>().Queryable.Where(u => u.Inactive == false).ToList()

                //Where(p => p.ProjectType.Name == "Web Application")
            };
            return viewModel;
        }

        
    }

  }





