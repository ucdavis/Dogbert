<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Dogbert.Core.Domain.Project>" %>
<%@ Import Namespace="Dogbert.Controllers" %>
<%@ Import Namespace="Dogbert.Core.Resources" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	 All Text Types Used
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Edit Project Text</h2>
    <h4>Project: <%=Html.Encode(Model.Name)%> </h4>
   
    <div>
        <%= string.Format("<a href='{0}#{1}'>Back to Project</a>", Url.RouteUrl(new { controller = "Project", action = "Edit", id = Model.Id }), StaticValues.Tab_ProjectText)%>
    </div>
    <h2>There are no more "Text Types" to add. </h2>


</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="PageHeader" runat="server">
</asp:Content>
