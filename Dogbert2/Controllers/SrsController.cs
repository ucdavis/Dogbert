using System.Web.Mvc;
using Dogbert2.Core.Domain;
using Dogbert2.Services;
using UCDArch.Core.PersistanceSupport;

namespace Dogbert2.Controllers
{
    /// <summary>
    /// Controller for the Srs class
    /// </summary>
    public class SrsController : ApplicationController
    {
        private readonly IRepository<Project> _projectRepository;
        private readonly ISrsGenerator _srsGenerator;

        public SrsController(IRepository<Project> projectRepository, ISrsGenerator srsGenerator)
        {
            _projectRepository = projectRepository;
            _srsGenerator = srsGenerator;
        }

        /// <summary>
        /// Get the SRS for a project
        /// </summary>
        /// <param name="id">Project Id</param>
        /// <returns></returns>
        public FileResult Index(int id)
        {
            var project = _projectRepository.GetNullableById(id);

            var pdf = _srsGenerator.GeneratePdf(project);
            return File(pdf, "application/pdf", "doc.pdf");
        }
    }
}
