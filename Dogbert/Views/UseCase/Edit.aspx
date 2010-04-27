<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Dogbert.Controllers.ViewModels.UseCaseViewModel>" %>
<%@ Import Namespace="Dogbert.Core.Domain"%>
<%@ Import Namespace="xVal.Html"%>
<%@ Import Namespace="Dogbert.Controllers.ViewModels"%>
<%@ Import Namespace="Dogbert.Controllers.Helpers"%>
<%@ Import Namespace="Dogbert.Core.Resources"%>

<%--USECASE/EDIT--%>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%= Html.ClientSideValidation<UseCase>("UseCase")%>
     <h2>View/Edit UseCase: <%= this.Label("UseCase.Name")%> </h2>
     <h4>Project: <%= this.Label("Project.Name")%> </h4>

    <%= Html.ValidationSummary("Edit was unsuccessful. Please correct the errors and try again.") %>

    <div id="tabs">
        <ul>
            <li><a href="#tab-1"><span>Use Case Details</span></a></li>
            <li><a href="#tab-2"><span>Use Case Steps</span></a></li>
            <li><a href="#tab-3"><span>Related Uses Cases</span></a></li>
            <li><a href="#tab-4"><span>Related Requirements</span></a></li>
        </ul>
        
        <div id="tab-1">
         <%= Html.ClientSideValidation<UseCase>("UseCase")%>
         <%= Html.ValidationSummary("Edit was unsuccessful. Please correct the errors and try again.") %>
         <% using (Html.BeginForm()) {%>
            <% Html.RenderPartial("UseCaseEditForm"); %>
            <p>
                <input type="submit" value="Save" />
            </p>

            <% } %>
         
        </div>
        
        
          <div id= "tab-2">
            <% Html.RenderPartial("UseCaseStepListForm"); %>
             <p>
                <%= Html.ActionLink<Dogbert.Controllers.UseCaseController>(a => a.CreateUseCaseStep(Model.UseCase.Id), "Create UseCaseStep")%>
            </p>
         </div>
         
         <div id= "tab-3">
            <% Html.RenderPartial("RelateduseCaseListForm"); %>
            <p>
                <%= Html.ActionLink<Dogbert.Controllers.UseCaseController>(a => a.EditChildren (Model.UseCase.Id), "Update Related Use Cases")%>
            </p> 
            
         </div>
         
         <div id= "tab-4">
            <% Html.RenderPartial("RelatedRequirementsListForm"); %>
            <p>
                <%= Html.ActionLink<Dogbert.Controllers.UseCaseController>(a => a.EditRelatedRequirements (Model.UseCase.Id), "Update Related Requirements")%>
            </p> 
            
         </div>
    
    <div>
       <%= string.Format("<a href='{0}#{1}'>Back to Project</a>", Url.RouteUrl(new {controller="Project", action="Edit", id=Model.Project.Id}), StaticValues.Tab_UseCases) %>
    </div>
    
  </div> <%-- end tabs--%>
</asp:Content>



<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script src='../../Scripts/tiny_mce/jquery.tinymce.js' type="text/javascript"></script>
    <script type="text/javascript">
        $(function() {
            var tabs = $("#tabs");
            $("#tabs").tabs();
        });

       
    </script>

    

</asp:Content>


