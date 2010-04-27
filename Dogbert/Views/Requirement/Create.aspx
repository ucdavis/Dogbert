<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Dogbert.Controllers.ViewModels.RequirementViewModel>" %>

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
                <label for="Description">Description:</label>
                <%= Html.TextBox("Description") %>
                <%= Html.ValidationMessage("Description", "*") %>
            </p>
            <p>
                <label for="TechnicalDifficulty">TechnicalDifficulty:</label>
                <%= Html.TextBox("TechnicalDifficulty") %>
                <%= Html.ValidationMessage("TechnicalDifficulty", "*") %>
            </p>
            <p>
                <%= this.Select("RequirementType").Options(Model.RequirementTypes, x=>x.Id, x=>x.Name)
                        .FirstOption("--Select a Requirement Type--")
                        .HideFirstOptionWhen(Model.Requirement != null && Model.Requirement.RequirementType != null)                
                        .Label("Requirement Type:")
                        .Selected( Model.Requirement != null && Model.Requirement.RequirementType != null ? Model.Requirement.RequirementType.Id : string.Empty )
                                        %>
            </p>
            <p>
                <label for="IsComplete">IsComplete:</label>
                <%= Html.CheckBox("IsComplete") %>
                <%= Html.ValidationMessage("IsComplete", "*") %>
            </p>
            <p>
                <input type="submit" value="Create" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%=Html.ActionLink("Back to List", "Index") %>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

