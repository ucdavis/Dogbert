<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ProjectFileViewModel>" %>

<%@ Import Namespace="xVal.Html" %>
<%@ Import Namespace="Dogbert.Core.Domain" %>
<%@ Import Namespace="Dogbert.Controllers.ViewModels" %>
<%@ Import Namespace="Dogbert.Controllers.Helpers" %>
<%@ Import Namespace="Dogbert.Core.Resources" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Remove
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Remove</h2>
    <%= Html.ValidationSummary("Remove was unsuccessful. Please correct the errors and try again.") %>
    <% using (Html.BeginForm())
       {%>
    <%= Html.Hidden("projectId", Model.Project.Id) %>
    <%= Html.AntiForgeryToken() %>
    <fieldset>
        <legend>Fields</legend>
        <% Html.RenderPartial("ProjectFileForm"); %>
        <p>
            <label for="FileName">
                File Name:</label>
            <%= Model.ProjectFile.FileName ?? string.Empty%>
        </p>
    </fieldset>
    <p>
        <input type="submit" value="Remove" />
    </p>
    <% } %>
   
    <div>
       <%= string.Format("<a href='{0}#{1}'>Back to Project</a>", Url.RouteUrl(new { controller = "Project", action = "Edit", id = Model.Project.Id }), StaticValues.Tab_ProjectFiles)%>
   
      <%--  <%=Html.EditProjectUrl(Model.Project.Id, StaticValues.Tab_ProjectFiles)%>--%>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
