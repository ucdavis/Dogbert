using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Web.Mvc;
using Dogbert2.Core.Domain;
using Dogbert2.Filters;
using Dogbert2.Services;
using UCDArch.Core.PersistanceSupport;

namespace Dogbert2.Controllers
{
    /// <summary>
    /// Controller for the Srs class
    /// </summary>
    [Authorize]
    public class SrsController : ApplicationController
    {
        private readonly IRepository<Project> _projectRepository;
        private readonly IRepository<File> _fileRepository;
        private readonly IAccessValidatorService _accessValidator;

        public SrsController(IRepository<Project> projectRepository, IRepository<File> fileRepository, IAccessValidatorService accessValidator)
        {
            _projectRepository = projectRepository;
            _fileRepository = fileRepository;
            _accessValidator = accessValidator;
        }

        /// <summary>
        /// Get the SRS for a project
        /// </summary>
        /// <param name="id">Project Id</param>
        /// <returns></returns>
        public FileResult Index(int id, bool draft = false)
        {
            var project = _projectRepository.GetNullableById(id);

            // validate access
            var redirect = _accessValidator.CheckReadAccess(CurrentUser.Identity.Name, project);
            if (redirect != null) throw new SecurityException("Not authorized to view file");

            var generator = new SrsGenerator(_fileRepository, Repository.OfType<SectionType>());

            // get the srs
            var srs = generator.GeneratePdf(project, draft);

            // append the files if any
            var files = new List<byte[]>();
            files.Add(srs);
            files.AddRange(project.Files.Where(a => a.Append).Select(a=>a.Contents).ToList());

            var pdf = new byte[0];
            if (files.Count() > 1)
            {
                pdf = PdfMerger.MergePdfs(files);
            }
            else
            {
                pdf = srs;
            }
            
            return File(pdf, "application/pdf", string.Format("srs-{0}.pdf", id));
        }
    }
}
