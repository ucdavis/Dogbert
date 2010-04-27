<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Dogbert.Core.Domain.Project>>" %>
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
     
 
 <script type="text/javascript" src="<%= Request.ApplicationPath + @"Scripts/jquery.tablednd_0_5.js" %>"></script>
 <script type="text/javascript">
     $(document).ready(function() {
         $("#tabs").tabs();

         $("#WebApplications").tableDnD({
             onDragClass: "myDragClass",
             onDrop: function(table, row) {
                 var rows = $(table).find("tBody tr");
                 //debugger;
                 var newOrder = new Array();

                 for (i = 0; i < rows.length; i++) {
                     newOrder[i] = rows[i].id;
                 }

                 var url = '<%= Url.Action("UpdateProjectPriority", "Project") %>';
                 $.post(url, { projects: newOrder },
                            function(result) { // refresh the calendar
                                alert("Completed Update");
                            });
                 //                  debugger;
             }
         }); //tableDnD


     });

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
        <li><a href="#tab-1"><span>Web Applications</span></a></li>
        <li><a href="#tab-2"><span>Web Sites</span></a></li>
        <li><a href="#tab-3"><span>Sharepoint</span></a></li>
        <li><a href="#tab-4"><span>Infrastructure</span></a></li>
    </ul>
 
<%--Web Aps-----------------------------------------------------------------------------------%>   
    <div id="tab-1">
        <% using (Html.BeginForm()) {%>
        <h2>Web Applications</h2>
        <% Html.Grid(Model.Where(p => p.ProjectType.Name == "Web Application"))
               .Transactional()
               .Name("WebApplications")
               .PrefixUrlParameters(false)
               .RowAction(r=>r.HtmlAttributes.Add("id", r.DataItem.Id))
               .Columns(col =>
                            {
                                col.Add(project =>
                                            { %>
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
                            })
                .Render(); %>
    <%}%>
             
</div>
<div id="tab-2">
         <% using (Html.BeginForm()) {%>
        <h2>Web Sites</h2>
        <% Html.Grid(Model.Where(p => p.ProjectType.Name == "Web Site"))
               .Transactional()
               .Name("Web Site")
               .PrefixUrlParameters(false)
               .Columns(col =>
                            {
                                col.Add(project =>
                                            { %>
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
                            })
                .Render(); %>
    <%}%>  
</div>
<div id="tab-3">
        <% using (Html.BeginForm()) {%>
        <h2>Infrastructure</h2>
        <% Html.Grid(Model.Where(p => p.ProjectType.Name == "Infrastructure"))
               .Transactional()
               .Name("Infrastructure")
               .PrefixUrlParameters(false)
               .Columns(col =>
                            {
                                col.Add(project =>
                                            { %>
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
                            })
                .Render(); %>
    <%}%>  

</div>
<div id="tab-4">
        <% using (Html.BeginForm()) {%>
        <h2>Sharepoint</h2>
        <% Html.Grid(Model.Where(p => p.ProjectType.Name == "Sharepoint"))
               .Transactional()
               .Name("Sharepoint")
               .PrefixUrlParameters(false)
               .Columns(col =>
                            {
                                col.Add(project =>
                                            { %>
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
                            })
                .Render(); %>
    <%}%>  

</div>
</div> <%--//end tabs--%> 
</asp:Content>

