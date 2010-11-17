<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<UseCaseViewModel>" %>
<%@ Import Namespace="Dogbert.Core.Resources"%>
<%@ Import Namespace="Dogbert.Controllers.Helpers"%>
<%@ Import Namespace="Dogbert.Controllers.ViewModels"%>
<%@ Import Namespace="Dogbert.Core.Domain"%>
<%@ Import Namespace="xVal.Html"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Create
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Create Use Case</h2>
   <%= Html.ClientSideValidation<UseCase>("UseCase")%>
    <h4>Project: <%= this.Label("Project.Name")%> </h4>
    <div>
        <%= string.Format("<a href='{0}#{1}'>Back to Project</a>", Url.RouteUrl(new {controller="Project", action="Edit", id=Model.Project.Id}), StaticValues.Tab_UseCases) %>
    </div>
    <%= Html.ValidationSummary("Create was unsuccessful. Please correct the errors and try again.") %>
    <% using (Html.BeginForm()) {%>
    <% Html.RenderPartial("UseCaseForm"); %>
    
    <p>
        <input type="submit" value="Create" />
    </p>

    <% } %>
    


</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script src="<%= Url.Content("~/tiny_mce/jquery.tinymce.js") %>" type="text/javascript"></script>
</asp:Content>

