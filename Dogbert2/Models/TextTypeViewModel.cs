using Dogbert2.Core.Domain;
using UCDArch.Core.PersistanceSupport;
using UCDArch.Core.Utils;

namespace Dogbert2.Models
{
    /// <summary>
    /// ViewModel for the TextType class
    /// </summary>
    public class TextTypeViewModel
    {
        public TextType TextType { get; set; }
 
        public static TextTypeViewModel Create(IRepository repository)
        {
            Check.Require(repository != null, "Repository must be supplied");
			
            var viewModel = new TextTypeViewModel {TextType = new TextType()};
 
            return viewModel;
        }
    }
}