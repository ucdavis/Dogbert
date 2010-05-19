using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Dogbert.Core.Domain;
using UCDArch.Web.Controller;
using MvcContrib;

namespace Dogbert.Controllers
{
    public class ReportController : SuperController
    {
        //
        // GET: /Report/
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Project Id</param>
        /// <returns></returns>
        public ActionResult Index(int id)
        {
            var project = Repository.OfType<Project>().GetNullableByID(id);

            if (project == null) return this.RedirectToAction<ProjectController>(a => a.DynamicIndex());

            // load the report
            var rview = new Microsoft.Reporting.WebForms.ReportViewer();
            rview.ServerReport.ReportServerUrl = new Uri(System.Web.Configuration.WebConfigurationManager.AppSettings["ReportServer"]);

            rview.ServerReport.ReportPath = @"/Dogbert/SRS";

            var paramList = new List<Microsoft.Reporting.WebForms.ReportParameter>();

            paramList.Add(new Microsoft.Reporting.WebForms.ReportParameter("projectid", project.Id.ToString()));

            rview.ServerReport.SetParameters(paramList);

            string mimeType, encoding, extension, deviceInfo;
            string[] streamids;
            Microsoft.Reporting.WebForms.Warning[] warnings;

            string format;

            format = "PDF";

            deviceInfo =
            "<DeviceInfo>" +
            "<SimplePageHeaders>True</SimplePageHeaders>" +
            "<HumanReadablePDF>True</HumanReadablePDF>" +   // this line disables the compression done by SSRS 2008 so that it can be merged.
            "</DeviceInfo>";

            byte[] bytes = rview.ServerReport.Render(format, deviceInfo, out mimeType, out encoding, out extension, out streamids, out warnings);

            return File(bytes, "application/pdf");
        }

        //public ActionResult View(int id)
        //{
        //    return View();
        //}
    }
}
