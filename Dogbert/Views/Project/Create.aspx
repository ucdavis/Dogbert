<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Dogbert.Controllers.ViewModels.ProjectViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Create
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Create</h2>
    
    <% Html.RenderPartial("ProjectForm"); %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
<script type="text/javascript">
    $(function() {
        $("#tabs").tabs();

        $("#" + "Project_ProjectedStart").datepicker(); //TextBox("Project.ProjectedStart")
        $("#" + "Project_ProjectedEnd").datepicker(); //TextBox("Project.ProjectedStart")
    });

</script>

</asp:Content>

