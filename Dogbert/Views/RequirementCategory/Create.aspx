<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Dogbert.Controllers.ViewModels.RequirementCategoryViewModel>" %>
<%@ Import Namespace="Dogbert.Controllers"%>
<%@ Import Namespace="Dogbert.Core.Resources" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Create
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Create</h2>

    <%= Html.ValidationSummary("Create was unsuccessful. Please correct the errors and try again.") %>

    <% using (Html.BeginForm()) {%>
        <%= Html.AntiForgeryToken() %>
        <fieldset>
            <legend>Fields</legend>
            <p>
                <label for="Name">Name:</label>
                <%= Html.TextBox("RequirementCategory.Name") %>
                <%= Html.ValidationMessage("RequirementCategory.Name", "*")%>
            </p>
            <p>
                <input type="submit" value="Create" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%= string.Format("<a href='{0}#{1}'>Back to Project</a>", Url.RouteUrl(new { controller = "Project", action = "Edit", id = Model.Project.Id }), StaticValues.Tab_RequirementCategories)%>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

