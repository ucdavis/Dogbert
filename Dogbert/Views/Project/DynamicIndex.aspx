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


         $(".main-tables table").tableDnD({
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

         $("#DesignerProjects table").tableDnD({
             onDragClass: "myDragClass",
             onDrop: function(table, row) {
                 var rows = $(table).find("tBody tr");
                 var newOrder = new Array();

                 for (i = 0; i < rows.length; i++) {
                     newOrder[i] = $(rows[i]).find("input[type='hidden']").val();
                 }

                 var url = '<%= Url.Action("UpdateDesignerPriority", "Project") %>';
                 $.post(url, { projects: newOrder },
                        function(result) { // refresh the calendar
                            alert("Completed Update");
                        });
             }
         });

         $(".remove-designer").click(function(action) {
            var url = '<%= Url.Action("RemoveFromDesignerProjects", "Project") %>';
             $.post(url, { projects: $(this).attr("pid") },
                            function(result) { // refresh the calendar
                                if (result == "True") $(this).remove();
                            });
         });

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
        <li><a href="#tab-<%= Model.ProjectTypes.Count %>"><span>Designer Projects</span></a></li>
    </ul>
  
<%--------------------------------------%>

<% for (int i = 0; i < Model.ProjectTypes.Count; i++) { %>
    <% string projectType = Model.ProjectTypes[i].Name; %>
    <div id="tab-<%= i %>">
        
        <h2><%= Html.Encode(projectType) %></h2>
    
        <%      
                Html.Grid(Model.Projects.Where(p => p.ProjectType.Name == projectType).OrderBy(a => a.Priority))
               .Transactional()
               .HtmlAttributes(new {@class="main-tables"})
               .Name(projectType)
               .Columns(col =>
                            {
                                col.Add(project =>
                                            { %>
                                            
                                                <%= Html.Hidden("projectId", project.Id) %>
                                            
                                                <%=
                                                    Html.ActionLink<ProjectController>(a => a.Edit(project.Id), "Edit")
                                                %>
                                                
                                                |
                                                
                                                <%= Html.ActionLink<ReportController>(a=>a.SRS(project.Id), "SRS") %>
                                            <% });
                                             
                                col.Add(project => project.Id);
                                col.Add(project => project.Name);
                                col.Add(project => project.Contact);
                                col.Add(project => project.Unit);
                                col.Add(project => project.Complexity);
                                col.Add(project => project.StartDateString).Title("Start");
                                col.Add(project => project.EndDateString).Title("End");
                                col.Add(project => project.Deadline).Format("{0:d}");
                                col.Add(project => project.StatusCode.Name).Title("Status");
                            })
                        
                .Render(); %>
        
        
    </div>

<% } %>

    <div id="tab-<%= Model.ProjectTypes.Count %>">
            <h2>Designer Projects</h2>
    
            <%      
                    Html.Grid(Model.DesignerProjects)
                   .Transactional()
                   .Name("DesignerProjects")
                   .Columns(col =>
                                {
                                    col.Add(project =>
                                                { %>
                                                
                                                    <%= Html.Hidden("projectId", project.Id) %>
                                                
                                                    <%=
                                                        Html.ActionLink<ProjectController>(a => a.Edit(project.Id), "Edit")
                                                    %>
                                                    
                                                    |
                                                    
                                                    <%= Html.ActionLink<ReportController>(a=>a.SRS(project.Id), "SRS") %>
                                                    
                                                    |
                                                    
                                                    <a href="#" class="remove-designer" pid="<%= Html.Encode(project.Id)%>">Remove</a>
                                                    
                                                <% });
                                                 
                                    col.Add(project => project.Id);
                                    col.Add(project => project.Name);
                                    col.Add(project => project.Contact);
                                    col.Add(project => project.Unit);
                                    col.Add(project => project.Complexity);
                                    col.Add(project => project.StartDateString).Title("Start");
                                    col.Add(project => project.EndDateString).Title("End");
                                    col.Add(project => project.Deadline).Format("{0:d}");
                                    col.Add(project => project.StatusCode.Name).Title("Status");
                                })
                            
                    .Render(); %>
    </div>

</div>
 
</asp:Content>

