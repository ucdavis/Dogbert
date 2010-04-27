<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ProjectFileViewModel>" %>
<%@ Import Namespace="xVal.Html"%>
<%@ Import Namespace="Dogbert.Core.Domain"%>
<%@ Import Namespace="Dogbert.Controllers.ViewModels"%>
<%@ Import Namespace="Dogbert.Controllers.Helpers"%>
<%@ Import Namespace="Dogbert.Core.Resources"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Remove
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Remove</h2>

    <%= Html.ValidationSummary("Remove was unsuccessful. Please correct the errors and try again.") %>

    <% using (Html.BeginForm()) {%>
    <% Html.RenderPartial("ProjectFileForm"); %>
    
    <p>
        <input type="submit" value="Remove" />
    </p>

    <% } %>
    
    

    <div>
       <%=Html.EditProjectUrl(Model.Project.Id, StaticValues.Tab_ProjectFiles)%>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
