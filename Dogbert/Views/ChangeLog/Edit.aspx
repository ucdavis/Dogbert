<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Dogbert.Controllers.ViewModels.ChangeLogViewModel>" %>
<%@ Import Namespace="Dogbert.Core.Domain"%>
<%@ Import Namespace="xVal.Html"%>
<%@ Import Namespace="Dogbert.Controllers.ViewModels"%>
<%@ Import Namespace="Dogbert.Controllers.Helpers"%>
<%@ Import Namespace="Dogbert.Core.Resources"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%= Html.ClientSideValidation<ChangeLog>("ChangeLog")%>
    <h2>Edit Change Log Entry</h2>
    <h4>Project: <%= this.Label("Project.Name")%> </h4>
    <div>
       <%= string.Format("<a href='{0}#{1}'>Back to Project</a>", Url.RouteUrl(new {controller="Project", action="Edit", id=Model.Project.Id}), StaticValues.Tab_ChangeLog) %>
    </div>
    <%= Html.ValidationSummary("Edit was unsuccessful. Please correct the errors and try again.") %>

    <% using (Html.BeginForm()) {%>
    <% Html.RenderPartial("ChangeLogForm"); %>
    
    <p>
        <input type="submit" value="Save" />
    </p>

    <% } %>
    
    



</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

