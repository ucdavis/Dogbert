<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Dogbert.Controllers.ViewModels.ProjectViewModel>" %>
<%@ Import Namespace="xVal.Html"%>
<%@ Import Namespace="Dogbert.Core.Domain"%>
<%@ Import Namespace="Dogbert.Helpers"%>

    

<%--<%= Html.ClientSideValidation<Project>("Project") %>--%>
    <%= Html.ValidationSummary() %>

        <div>
        
        <%=Html.ActionLink<Dogbert.Controllers.ProjectController>(a => a.Edit(Model.UseCase.Project.Id), "Back to Project")%>
                       
        </div>
        
        <fieldset>
            <legend>Project Use Case</legend>
            <% using (Html.BeginForm())
               { %>
                    <%= Html.AntiForgeryToken() %>
                    <%= Html.Hidden("Id", Model.UseCase.Id) %>
                    <%= Html.Hidden("projectID", Model.UseCase.Project.Id) %>
                <p>
                    <%= this.TextBox("UseCase.Name").Label("Use Case Name:")%>
                </p>
                <p>
                    <%= this.TextArea("UseCase.Description").Label("Description:")%>
               </p>
      
                <p>
                    <%= this.TextArea("UseCase.Precondition").Label("Precondition:")%>
               </p>
      
                <p>
                    <%= this.TextArea("UseCase.Postcondition").Label("Postcondition:")%>
               </p>
           
                <p>
                 <%= this.Select("UseCase.RequirementCategory")
                    .Options(Model.RequirementCategories, x=>x.Id, x=>x.Name)
                    .Selected(Model.UseCase != null && Model.UseCase.RequirementCategory != null ? Model.UseCase.RequirementCategory.Id : 1)
                    .FirstOption("--Requirement Category--")
                    .HideFirstOptionWhen(Model.RequirementCategories!= null)
                    .Label("Requirement Category:")%>
               </p>
               
         
           
                <p>
                    <input type="submit" value="Save" />
                </p>
            <%} %>
                      
        </fieldset>

    
   