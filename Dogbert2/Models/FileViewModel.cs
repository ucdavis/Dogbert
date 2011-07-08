using System.Collections.Generic;
using System.Web;
using Dogbert2.Core.Domain;
using UCDArch.Core.PersistanceSupport;
using UCDArch.Core.Utils;

namespace Dogbert2.Models
{
    /// <summary>
    /// ViewModel for the File class
    /// </summary>
    public class FileViewModel
    {
        public IList<File> Files { get; set; }
        public Project Project { get; set; }

        public static FileViewModel Create(IRepository repository, Project project)
        {
            Check.Require(repository != null, "Repository must be supplied");
            Check.Require(project != null, "project is required.");

            var viewModel = new FileViewModel {Project = project, Files = new List<File>()};
 
            return viewModel;
        }
    }

    public class FilePostModel
    {
        public string Title { get; set; }
        public string Caption { get; set; }
        public HttpPostedFileBase File { get; set; }
        public bool Append { get; set; }

        public byte[] FileContents { 
            get {

                if (this.File != null && this.File.ContentLength > 0)
                {
                    var buffer = new byte[this.File.ContentLength];
                    this.File.InputStream.Read(buffer, 0, this.File.ContentLength);
                    return buffer;    
                }

                return null;

            } 
        }

        public bool IsImage
        {
            get
            {
                if (this.File != null && this.File.ContentLength > 0)
                {
                    if (this.File.ContentType.Contains("image"))
                    {
                        return true;
                    }
                }

                return false;
            }
        }
    }
}