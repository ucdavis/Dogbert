using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Dogbert.Controllers.Helpers
{
    /// <summary>
    /// Helper class to go to a particular tab from the page
    /// </summary>
    public static class ReturnUrlGenerator
    {
        private static UrlHelper Url = new UrlHelper(new RequestContext(new HttpContextWrapper(HttpContext.Current), new RouteData()));

        public static string EditProjectUrl(this HtmlHelper html, int projectId, string tabName)
        {
            var url = Url.RouteUrl(new { controller = "Project", action = "Edit", id = projectId });// +"#" + tabName;
            var link = string.Format("<a href='{0}'>Back to Project</a>", url ?? "blank");

            return link;
        }
    }

    /// <summary>
    /// Helper class to go to a particular tab from the controller
    /// </summary>
    public static class ControllerReturnUrlGenerator
    {
        public static string EditProjectUrl(this UrlHelper url, int projectId, string tabName)
        {
            //var returnUrl = url.RouteUrl(new { controller = "Project", action = "Edit", id = projectId }) + "#" + tabName;
            var returnUrl = url.RouteUrl(ControllerGenerator.EditProject(projectId)) + "#" + tabName;

            return returnUrl;
        }
    }

    internal static class ControllerGenerator
    {
        internal static object EditProject(int projectId)
        {
            return new {controller = "Project", action = "Edit", id = projectId};
        }
    }
}
