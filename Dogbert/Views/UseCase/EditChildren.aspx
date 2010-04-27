<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<UseCaseViewModel>" %>
<%@ Import Namespace="Dogbert.Core.Resources"%>
<%@ Import Namespace="Dogbert.Controllers.Helpers"%>
<%@ Import Namespace="Dogbert.Controllers.ViewModels"%>
<%@ Import Namespace="Dogbert.Core.Domain"%>
<%@ Import Namespace="xVal.Html"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Related Use Cases (Children)
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%= Html.ClientSideValidation<UseCase>("UseCase")%>
    <h2>Edit Related Use Cases</h2>

    <%= Html.ValidationSummary("Create was unsuccessful. Please correct the errors and try again.") %>
    <% using (Html.BeginForm()) {%>
    <% Html.RenderPartial("UseCaseChildForm"); %>
   
    <% } %>
    
    <div>
        <%=Html.EditProjectUrl(Model.Project.Id, StaticValues.Tab_UseCases)%>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

