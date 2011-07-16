using Dogbert2.Core.Domain;
using UCDArch.Core.PersistanceSupport;
using UCDArch.Core.Utils;

namespace Dogbert2.Models
{
    /// <summary>
    /// ViewModel for the TextType class
    /// </summary>
    public class SectionTypeViewModel
    {
        public SectionType SectionType { get; set; }
 
        public static SectionTypeViewModel Create(IRepository repository)
        {
            Check.Require(repository != null, "Repository must be supplied");
			
            var viewModel = new SectionTypeViewModel {SectionType = new SectionType()};
 
            return viewModel;
        }
    }
}