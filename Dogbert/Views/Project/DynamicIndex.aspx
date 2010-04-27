<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ProjectIndexViewModel>" %>
<%@ Import Namespace="Dogbert.Controllers.ViewModels"%>
<%@ Import Namespace="Dogbert.Controllers"%>
<%@ Import Namespace="Dogbert.Helpers"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">

 <%--    Drag formats    --%>
	 <style type="text/css">
           .alt {background: #f5f5f5;}
           .dragHandle {width:80px;}
           .showDragHandle {cursor: move;}
           .myDragClass {background-color:#d5d5d5;
                        -moz-border-radius: 5px;
            }
     </style>
     
 
 <script type="text/javascript" src="<%= Url.Content("~/Scripts/jquery.tablednd_0_5.js") %>"></script>
 <script type="text/javascript">

     $(document).ready(function() {


         $("table").tableDnD({
             onDragClass: "myDragClass",
             onDrop: function(table, row) {

                 var rows = $(table).find("tBody tr");

                 var newOrder = new Array();

                 for (i = 0; i < rows.length; i++) {
                     newOrder[i] = $(rows[i]).find("input[type='hidden']").val();
                 }
                 var url = '<%= Url.Action("UpdateProjectPriority", "Project") %>';
                 $.post(url, { projects: newOrder },
                        function(result) { // refresh the calendar
                            alert("Completed Update");
                        });
             }
         }); //tableDnD
     });

     $(function() { $("#tabs").tabs(); });

</script>



</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<%--------------------------------------------------------------------------------------------%>   
   <h1>Projects</h1>
    <p>
        <%= Html.ActionLink("Create New", "Create") %>
    </p>

<div id="tabs">
    <ul>
        <% for(int i = 0; i < Model.ProjectTypes.Count; i++) { %>
        <li><a href="#tab-<%= i %>"><span><%= Html.Encode(Model.ProjectTypes[i].Name) %></span></a></li>
        <% } %>
    </ul>
  
<%--------------------------------------%>

<% for (int i = 0; i < Model.ProjectTypes.Count; i++) { %>
    <% string projectType = Model.ProjectTypes[i].Name; %>
    <div id="tab-<%= i %>">
        
        <h2><%= Html.Encode(projectType) %></h2>
    
        <%      
                Html.Grid(Model.Projects.Where(p => p.ProjectType.Name == projectType).OrderBy(a => a.Priority))
               .Transactional()
               .Name(projectType)
               .Columns(col =>
                            {
                                col.Add(project =>
                                            { %>
                                            
                                                <%= Html.Hidden("projectId", project.Id) %>
                                            
                                                <%=
                                                    Html.ActionLink<ProjectController>(a => a.Edit(project.Id), "Edit")
                                                %>
                                            <% });
                                             
                                col.Add(project => project.Id);
                                col.Add(project => project.Name);
                                col.Add(project => project.Contact);
                                col.Add(project => project.Unit);
                                col.Add(project => project.Complexity);
                                col.Add(project => project.StartDateString);
                                col.Add(project => project.EndDateString);
                                col.Add(project => project.Deadline).Format("{0:d}");
                                col.Add(project => project.StatusCode.Name);
                            })
                        
                .Render(); %>
        
        
    </div>

<% } %>
</div>
 
</asp:Content>

