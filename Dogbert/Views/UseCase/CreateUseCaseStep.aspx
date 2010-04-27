<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<UseCaseViewModel>" %>
<%@ Import Namespace="Dogbert.Core.Resources"%>
<%@ Import Namespace="Dogbert.Controllers.Helpers"%>
<%@ Import Namespace="Dogbert.Controllers.ViewModels"%>
<%@ Import Namespace="Dogbert.Core.Domain"%>
<%@ Import Namespace="xVal.Html"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Create Use Case Step
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%= Html.ClientSideValidation<UseCaseStep>("UseCaseStep")%>
    <h2>Create Use Case Step</h2>
    <%--<h2>UseCase: <%= this.Label("UseCase.Name")%> </h2>
    <h4>Project: <%= this.Label("UseCase.Project.Name")%> </h4>--%>

    <%= Html.ValidationSummary("Create was unsuccessful. Please correct the errors and try again.") %>
    <% using (Html.BeginForm()) {%>
    <% Html.RenderPartial("UseCaseStepForm"); %>
    

    <% } %>
    
    <div>
        <%=Html.EditProjectUrl(Model.Project.Id, StaticValues.Tab_UseCases)%>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

