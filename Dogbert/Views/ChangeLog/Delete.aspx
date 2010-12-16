<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Dogbert.Controllers.ViewModels.ChangeLogViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Delete
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Delete Requirement</h2>
    <h4>Project: <%= this.Label("Project.Name")%> </h4>
    
    <div>
        <%= string.Format("<a href='{0}#{1}'>Back to Project</a>", Url.RouteUrl(new {controller="Project", action="Edit", id=Model.Project.Id}), Dogbert.Core.Resources.StaticValues.Tab_ChangeLog) %>
    </div>
 
    <% using (Html.BeginForm()) {%>
     <p>
        <input type="submit" value="Delete" />
    </p>
    <% Html.RenderPartial("ChangeLogDetailForm"); %>
    <% } %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="PageHeader" runat="server">
</asp:Content>

