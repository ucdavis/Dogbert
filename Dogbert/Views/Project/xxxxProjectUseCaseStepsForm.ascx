<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Dogbert.Controllers.ViewModels.ProjectViewModel>" %>
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
                                Html.ActionLink<Dogbert.Controllers.ProjectController>(a => a.EditUseCaseSteps(steps.Id), "Edit")
                            %>
                            <% });
                                col.Add(steps => steps.Description);
                                col.Add(steps => steps.Optional);
                            })
                .Render(); %>

                <h3>Add New Steps</h3>
                <p>
                    <%= this.TextArea("UseCaseStep.Description").Label("Description:")%>
               </p>
               <p>
                    <%= this.CheckBox("UseCaseSet.Optional").Label("Optional")%>
               </p>
      
                <p>
                    <input type="submit" value="Save" />
                </p>
                
      
            <%} %>
                      
        </fieldset>

    
   