<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Dogbert.Controllers.ViewModels.UseCaseViewModel>" %>
<%@ Import Namespace="Dogbert.Core.Domain"%>
<%@ Import Namespace="xVal.Html"%>
<%@ Import Namespace="Dogbert.Controllers.ViewModels"%>
<%@ Import Namespace="Dogbert.Controllers.Helpers"%>
<%@ Import Namespace="Dogbert.Core.Resources"%>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%= Html.ClientSideValidation<UseCase>("UseCase")%>
     <h2>View/Edit UseCase: <%= this.Label("UseCase.Name")%> </h2>

    <%= Html.ValidationSummary("Edit was unsuccessful. Please correct the errors and try again.") %>

    <% using (Html.BeginForm()) {%>
    <% Html.RenderPartial("UseCaseForm"); %>
    <p>
        <input type="submit" value="Save" />
    </p>

    <% } %>
    
     <div>
        <% Html.RenderPartial("UseCaseStepListForm"); %>
     </div>
    
       


    <div>
       <%=Html.EditProjectUrl(Model.Project.Id, StaticValues.Tab_UseCases)%>
    </div>

</asp:Content>



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


