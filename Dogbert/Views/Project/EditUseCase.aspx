<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Dogbert.Controllers.ViewModels.ProjectViewModel>" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	View/Edit Use Case
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h2>View/Edit Use Case</h2>
    
    <% Html.RenderPartial("ProjectEditUseCaseForm"); %>
     <h3>Use Case Steps</h3>
    <% Html.RenderPartial("ProjectUseCaseStepsForm"); %>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
