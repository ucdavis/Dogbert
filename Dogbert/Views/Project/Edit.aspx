<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Dogbert.Controllers.ViewModels.ProjectViewModel>" %>
<%@ Import Namespace="Dogbert.Controllers"%>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
<script type="text/javascript">
    $(function() {
        var tabs = $("#tabs");
        $("#tabs").tabs();
    });

    $(function() {
    $("#" + "Project_ProjectedStart").datepicker(); //TextBox("Project.ProjectedStart")
    $("#" + "Project_ProjectedEnd").datepicker(); //TextBox("Project.ProjectedStart")
    });

</script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Edit</h2>
    
    <div id="tabs">
        <ul>
            <li><a href="#tab-1"><span>Project Details</span></a></li>
            <li><a href="#tab-2"><span>Requirements</span></a></li>
            <li><a href="#tab-3"><span>Use Cases</span></a></li>
        </ul>
        
        <div id="tab-1">
        <% Html.RenderPartial("ProjectForm"); %>
        
        <% Html.RenderPartial("ProjectEditForm"); %>
        </div>
    
        <div id="tab-2">
            
            <% Html.RenderPartial("RequirementListForm", Model.Project.Requirements); %>
            
            <p>
                <%= Html.ActionLink<RequirementController>(a => a.Create(Model.Project.Id), "Create Requirement") %>
            </p>
            
        </div>

        <div id="tab-3">
            Use Case Tab Starts Here
            <% Html.RenderPartial("ProjectUseCaseForm"); %>
        </div>
    
    </div> <%--//end tabs--%> 
</asp:Content>


