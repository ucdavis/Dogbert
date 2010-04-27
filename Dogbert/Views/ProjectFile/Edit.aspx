<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ProjectFileViewModel>" %>

<%@ Import Namespace="xVal.Html" %>
<%@ Import Namespace="Dogbert.Core.Domain" %>
<%@ Import Namespace="Dogbert.Controllers.ViewModels" %>
<%@ Import Namespace="Dogbert.Controllers.Helpers" %>
<%@ Import Namespace="Dogbert.Core.Resources" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Edit
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Edit</h2>
    <%= Html.ClientSideValidation<ProjectFile>("ProjectFile")%>
    <%= Html.ValidationSummary("Edit was unsuccessful. Please correct the errors and try again.") %>
    <% using (Html.BeginForm("Edit", "ProjectFile", FormMethod.Post, new { @enctype = "multipart/form-data" }))
       {%>
    <%= Html.Hidden("projectId", Model.Project.Id) %>
    <%= Html.AntiForgeryToken() %>
    <fieldset>
        <legend>Fields</legend>
        <% Html.RenderPartial("ProjectFileForm"); %>
        <p>
            <label for="FileName">
                Current File Name:</label>
            <%= Model.ProjectFile.FileName ?? string.Empty%>
        </p>
        <p>
            <label for="file">
                Replace with new upload:</label>
            <input type="file" name="file" id="file" />
        </p>
    </fieldset>
    <p>
        <input type="submit" value="Save" />
    </p>
    <% } %>
    <div>
        <%=Html.EditProjectUrl(Model.Project.Id, StaticValues.Tab_ProjectFiles)%>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
