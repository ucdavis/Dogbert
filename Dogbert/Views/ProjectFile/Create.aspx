<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ProjectFileViewModel>" %>

<%@ Import Namespace="Dogbert.Core.Domain" %>
<%@ Import Namespace="xVal.Html" %>
<%@ Import Namespace="Dogbert.Controllers.ViewModels" %>
<%@ Import Namespace="Dogbert.Controllers.Helpers" %>
<%@ Import Namespace="Dogbert.Core.Resources" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Create
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Create</h2>
      <div>
      <%-- causing error so switched formatt --%>
      <%--  <%=Html.EditProjectUrl(Model.Project.Id, StaticValues.Tab_ProjectFiles)%>--%>
      <%= string.Format("<a href='{0}#{1}'>Back to Project</a>", Url.RouteUrl(new { controller = "Project", action = "Edit", id = Model.Project.Id }), StaticValues.Tab_ProjectFiles)%>
    </div>
    <%= Html.ClientSideValidation<ProjectFile>("ProjectFile")%>
    <%= Html.ValidationSummary("Create was unsuccessful. Please correct the errors and try again.") %>
    <% using (Html.BeginForm("Create", "ProjectFile", FormMethod.Post, new { @enctype = "multipart/form-data" }))
       {%>
    <%= Html.Hidden("projectId", Model.Project.Id) %>
    <%= Html.AntiForgeryToken() %>
    <fieldset>
        <legend>Fields</legend>
        <% Html.RenderPartial("ProjectFileForm"); %>
        <p>
            <%=this.FileUpload("fileUpload").Label("Upload File:")%>
        </p>
    </fieldset>
    <p>
        <input type="submit" value="Create" />
    </p>
    <% } %>
    
  
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
