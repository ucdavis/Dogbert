<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Dogbert.Controllers.ViewModels.ProjectViewModel>" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Create
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Create Project</h2>
    
    <% Html.RenderPartial("ProjectForm"); %>
</asp:Content>

  <asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
  <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="stylesheet" type="text/css"/>
  <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.4/jquery.min.js" type="text/javascript"></script>
  <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js" type="text/javascript"></script>

<script type="text/javascript">
    $(document).ready(function() {
        $("#tabs").tabs();

        $("#" + "Project_ProjectedStart").datepicker(); //TextBox("Project.ProjectedStart")
        $("#" + "Project_ProjectedEnd").datepicker(); //TextBox("Project.ProjectedStart")


        $("#slider").slider({
            range: "min",
            value: $("#" + "Project_Complexity").val(),
            min: 1,
            max: 10,
            slide: function(event, ui) {
                $("#" + "Project_Complexity").val(ui.value);
            }
        });

        //set value on proj complexity
        $("#" + "Project_Complexity").val($("#slider").slider("value"));

    });

</script>

</asp:Content>

