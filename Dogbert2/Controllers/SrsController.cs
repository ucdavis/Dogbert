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
    [AllRoles]
    public class SrsController : ApplicationController
    {
        private readonly IRepository<Project> _projectRepository;
        private readonly ISrsGenerator _srsGenerator;
        private readonly IAccessValidatorService _accessValidator;

        public SrsController(IRepository<Project> projectRepository, ISrsGenerator srsGenerator, IAccessValidatorService accessValidator)
        {
            _projectRepository = projectRepository;
            _srsGenerator = srsGenerator;
            _accessValidator = accessValidator;
        }

        /// <summary>
        /// Get the SRS for a project
        /// </summary>
        /// <param name="id">Project Id</param>
        /// <returns></returns>
        public FileResult Index(int id)
        {
            var project = _projectRepository.GetNullableById(id);

            // validate access
            var redirect = _accessValidator.CheckReadAccess(CurrentUser.Identity.Name, project);
            if (redirect != null) throw new SecurityException("Not authorized to view file");

            var pdf = _srsGenerator.GeneratePdf(project);
            return File(pdf, "application/pdf", "doc.pdf");
        }
    }
}
