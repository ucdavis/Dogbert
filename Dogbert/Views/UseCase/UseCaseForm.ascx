<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Dogbert.Controllers.ViewModels.UseCaseViewModel>" %>
<%@ Import Namespace="xVal.Html"%>
<%@ Import Namespace="Dogbert.Core.Domain"%>
<%@ Import Namespace="Dogbert.Helpers"%>

<%--<%= Html.ClientSideValidation<Project>("Project") %>--%>
 <%--    <%= Html.ValidationSummary() %>
 --%>     
        <%= Html.AntiForgeryToken() %>
        <fieldset>
            <legend>Use Case</legend>
                   <%-- <%= Html.Hidden("Id", Model.UseCase.Id) %>
                    <%= Html.Hidden("projectID", Model.UseCase.Project.Id) %>--%>
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
                    .Options(Model.Project.RequirementCategories.Where(a => a.IsActive), x=>x.Id, x=>x.Name)
                    .Selected(Model.UseCase != null && Model.UseCase.RequirementCategory != null ? Model.UseCase.RequirementCategory.Id : 1)
                    .FirstOption("--Requirement Category--")
                    .HideFirstOptionWhen(Model.RequirementCategories!= null)
                    .Label("Requirement Category:")%>
               </p>
           
               
                <p>
                 <%= this.MultiSelect("UseCase.Actors")
                    .Options(Model.Actors, x=>x.Id, x=>x.Name)
                    //.Selected(Model.UseCase != null && Model.UseCase.Actors != null ? Model.UseCase.Actors.i: 1)
                    .Selected(Model.UseCase != null && Model.UseCase.Actors != null ? Model.UseCase.Actors : new List<Actor>()) 
                    .FirstOption("--Actors--")
                    .HideFirstOptionWhen(Model.Actors!= null)
                    .Label("Select Actors:")%>
                   
                    
               </p>
       
                      
        </fieldset>
         



    
   