<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Dogbert.Controllers.ViewModels.ProjectViewModel>" %>
<%@ Import Namespace="Dogbert.Core.Resources"%>
<%@ Import Namespace="Dogbert.Controllers"%>

<%--PROJECT/EDIT--%>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">

<script type="text/javascript">
    $(function() {
        $("#tabs").tabs();

        $("#" + "Project_ProjectedStart").datepicker(); //TextBox("Project.ProjectedStart")
        $("#" + "Project_ProjectedEnd").datepicker(); //TextBox("Project.ProjectedStart")
    });

</script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>View/Edit Project: <%= this.Label("Project.Name")%> </h2>
       <div>
        <%=Html.ActionLink("Back to List", "DynamicIndex")%>
       </div>
        
    <div id="tabs">
        <ul>
            <li><a href="#<%= StaticValues.Tab_ProjectDetails %>"><span>Project Details</span></a></li>
            <li><a href="#<%= StaticValues.Tab_ProjectText %>"><span>Project Text</span></a></li>
            <li><a href="#<%= StaticValues.Tab_Requirements %>"><span>Requirements</span></a></li>
            <li><a href="#<%= StaticValues.Tab_UseCases %>"><span>Use Cases</span></a></li>
            <li><a href="#<%= StaticValues.Tab_ProjectFiles %>"><span>Files</span></a></li>
            <li><a href="#<%= StaticValues.Tab_RequirementCategories %>"><span>Requirement Categories</span></a></li>
            <li><a href="#<%= StaticValues.Tab_Actors %>"><span>Actors</span></a></li>
        </ul>
        
        <div id="<%= StaticValues.Tab_ProjectDetails %>">
        <% Html.RenderPartial("ProjectForm"); %>
         </div>
         
        <div id="<%= StaticValues.Tab_ProjectText%>">
        <% Html.RenderPartial("ProjectTextListForm"); %>
       </div>
    
        <div id="<%= StaticValues.Tab_Requirements %>">
            
            <%= Html.ActionLink<ReportController>(a=>a.Requirements(Model.Project.Id), "Export to Excel") %>
            
            <% Html.RenderPartial("RequirementListForm", Model.Project.Requirements); %>
            <p>
                <%= Html.ActionLink<RequirementController>(a => a.Create(Model.Project.Id), "Create Requirement") %>
            </p>
            
        </div>
        
        <div id="<%= StaticValues.Tab_UseCases %>">
              <% Html.RenderPartial("UseCaseListForm", Model.Project.UseCases); %>
             <p>
                <%= Html.ActionLink<UseCaseController>(a => a.Create(Model.Project.Id), "Create UseCase") %>
            </p>
        </div>
  
        <div id="<%= StaticValues.Tab_ProjectFiles %>">
            <% Html.RenderPartial("ProjectFileListForm", Model.Project.ProjectFiles); %>
            <p>
                <%= Html.ActionLink<ProjectFileController>(a => a.Create(Model.Project.Id), "Create ProjectFile") %>
            </p>
            <p>Images (*.bmp, *.jpg, *.png, *.gif) will be included in reporting</p>
        </div>
    
        <div id="<%= StaticValues.Tab_RequirementCategories %>">
            <% Html.RenderPartial("RequirementCategoryForm", Model.Project); %>
        </div>
    
        <div id="<%= StaticValues.Tab_Actors %>">
            <p>
                <%= Html.ActionLink<ActorController>(a => a.Create(Model.Project.Id), "Create Actor") %>
            </p>
            <% Html.RenderPartial("ActorListForm", Model.Actors); %>
        </div>


    </div> <%--//end tabs--%> 
</asp:Content>


