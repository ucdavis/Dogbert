using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Dogbert.Core.Domain;
using UCDArch.Web.Controller;
using MvcContrib;
using System.Linq;
using UCDArch.Core.PersistanceSupport;
using UCDArch.Core.Utils;
using System.Data.OleDb;
using System.Data;


namespace Dogbert.Controllers
{
    [Authorize(Roles = "User")]
    public class ReportController : SuperController
    {
        //
        // GET: /Report/
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Project Id</param>
        /// <returns></returns>
        public ActionResult SRS(int id)
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

        public ActionResult Requirements(int id)
        {
            var project = Repository.OfType<Project>().GetNullableByID(id);

            if (project == null) return this.RedirectToAction<ProjectController>(a => a.DynamicIndex());

            // load the report
            var rview = new Microsoft.Reporting.WebForms.ReportViewer();
            rview.ServerReport.ReportServerUrl = new Uri(System.Web.Configuration.WebConfigurationManager.AppSettings["ReportServer"]);

            rview.ServerReport.ReportPath = @"/Dogbert/Requirements";

            var paramList = new List<Microsoft.Reporting.WebForms.ReportParameter>();

            paramList.Add(new Microsoft.Reporting.WebForms.ReportParameter("projectid", project.Id.ToString()));

            rview.ServerReport.SetParameters(paramList);

            string mimeType, encoding, extension, deviceInfo;
            string[] streamids;
            Microsoft.Reporting.WebForms.Warning[] warnings;

            string format;

            format = "excel";

            deviceInfo =
            "<DeviceInfo>" +
            "<SimplePageHeaders>True</SimplePageHeaders>" +
            "<HumanReadablePDF>True</HumanReadablePDF>" +   // this line disables the compression done by SSRS 2008 so that it can be merged.
            "</DeviceInfo>";

            byte[] bytes = rview.ServerReport.Render(format, deviceInfo, out mimeType, out encoding, out extension, out streamids, out warnings);

            return File(bytes, "application/excel");
        }

        public ActionResult Projects()
        {
            var project = Repository.OfType<Project>().Queryable.Where(a => !a.StatusCode.IsComplete);

            if (project == null) return this.RedirectToAction<ProjectController>(a => a.DynamicIndex());

            // load the report
            var rview = new Microsoft.Reporting.WebForms.ReportViewer();
            rview.ServerReport.ReportServerUrl = new Uri(System.Web.Configuration.WebConfigurationManager.AppSettings["ReportServer"]);

            rview.ServerReport.ReportPath = @"/Dogbert/Projects";

            //var paramList = new List<Microsoft.Reporting.WebForms.ReportParameter>();

            //paramList.Add(new Microsoft.Reporting.WebForms.ReportParameter("projectid", project.Id.ToString()));

            //rview.ServerReport.SetParameters(paramList);

            string mimeType, encoding, extension, deviceInfo;
            string[] streamids;
            Microsoft.Reporting.WebForms.Warning[] warnings;
            
            string format;

            format = "excel";

            deviceInfo =
            "<DeviceInfo>" +
            "<SimplePageHeaders>True</SimplePageHeaders>" +
            "<RemoveSpace>0.125in</RemoveSpace>" +   //to remove extra rows or columns that do not contain report items
            "<HumanReadablePDF>True</HumanReadablePDF>" +   // this line disables the compression done by SSRS 2008 so that it can be merged.
            "</DeviceInfo>";

            byte[] bytes = rview.ServerReport.Render(format, deviceInfo, out mimeType, out encoding, out extension, out streamids, out warnings);

            return File(bytes, "application/excel");
        }

   
    }

   
}
