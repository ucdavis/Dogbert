<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Dogbert.Controllers.ViewModels.ProjectViewModel>" %>
<%@ Import Namespace="Dogbert.Core.Resources"%>
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
            <li><a href="#<%= StaticValues.Tab_ProjectDetails %>"><span>Project Details</span></a></li>
            <li><a href="#<%= StaticValues.Tab_Requirements %>"><span>Requirements</span></a></li>
            <li><a href="#<%= StaticValues.Tab_UseCases %>"><span>Use Cases</span></a></li>
        </ul>
        
        <div id="<%= StaticValues.Tab_ProjectDetails %>">
        <% Html.RenderPartial("ProjectForm"); %>
        
        <% Html.RenderPartial("ProjectEditForm"); %>
        </div>
    
        <div id="<%= StaticValues.Tab_Requirements %>">
            
            <% Html.RenderPartial("RequirementListForm", Model.Project.Requirements); %>
            
            <p>
                <%= Html.ActionLink<RequirementController>(a => a.Create(Model.Project.Id), "Create Requirement") %>
            </p>
            
        </div>

        <div id="<%= StaticValues.Tab_UseCases %>">
            Use Case Tab Starts Here
            <% Html.RenderPartial("ProjectUseCaseForm"); %>
        </div>
    
    </div> <%--//end tabs--%> 
</asp:Content>


