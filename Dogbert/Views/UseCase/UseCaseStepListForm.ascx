<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Dogbert.Controllers.ViewModels.UseCaseViewModel>" %>
<%@ Import Namespace="xVal.Html"%>
<%@ Import Namespace="Dogbert.Core.Domain"%>
<%@ Import Namespace="Dogbert.Helpers"%>

    

<%--<%= Html.ClientSideValidation<Project>("Project") %>--%>
    <%= Html.ValidationSummary() %>
              
        <fieldset>
            <legend>Use Case Steps</legend>
            <% using (Html.BeginForm())
               { %>
                    <%= Html.AntiForgeryToken() %>
                    <%= Html.Hidden("UseCaseId", Model.UseCase.Id) %>
                    <%= Html.Hidden("projectID", Model.UseCase.Project.Id) %>

               <% Html.Grid(Model.UseCase.Steps)
               .Transactional()
               .Name("UseCaseSteps")
               .PrefixUrlParameters(false)
               .RowAction(r=>r.HtmlAttributes.Add("Id", r.DataItem.Id))
               .Columns(col =>
                  {
                      col.Add(steps =>
                          { %>
                            <%=
                                Html.ActionLink<Dogbert.Controllers.UseCaseController>(a => a.EditUseCaseSteps(steps.Id), "Edit")
                            %>
                            <% });
                                col.Add(steps => steps.Description);
                                col.Add(steps => steps.Optional);
                            })
                .Render(); %>
              
      
            <%} %>
                      
        </fieldset>
         



    
   