<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Dogbert.Controllers.ViewModels.ProjectViewModel>" %>
<%@ Import Namespace="xVal.Html"%>
<%@ Import Namespace="Dogbert.Core.Domain"%>
<%@ Import Namespace="Dogbert.Helpers"%>

<%--<%= Html.ClientSideValidation<Project>("Project") %>--%>
    <%= Html.ValidationSummary() %>

<%--<%          Adds Project Text%>      --%>
        <fieldset>
            <legend>Use Case</legend>
               <% Html.Grid(Model.Project.UseCases)
               .Transactional()
               .Name("UseCases")
               .PrefixUrlParameters(false)
               .RowAction(r=>r.HtmlAttributes.Add("Id", r.DataItem.Id))
               .Columns(col =>
                  {
                      col.Add(pUseCase =>
                          { %>
                            <%=
                                Html.ActionLink<Dogbert.Controllers.ProjectController>(a => a.EditUseCase(pUseCase.Id), "Edit")
                            %>
                            <% });
                                col.Add(pUseCase => pUseCase.Name);
                                col.Add(pUseCase => pUseCase.Description);
                                col.Add(pUseCase => pUseCase.RequirementCategory.Name);
                  
                            })
                .Render(); %>
            
             <h3>Add New Use Case</h3>
            <% using (Html.BeginForm("CreateUseCase", "Project", FormMethod.Post))
               { %>
                <%= Html.AntiForgeryToken() %>
                <%= Html.Hidden("projectId", Model.Project.Id) %>
                <p>
                    <%= this.TextBox("Name").Label("Use Case Name:")%>
                </p>
                <p>
                    <%= this.TextArea("Description").Label("Description:")%>
               </p>
      
                <p>
                    <%= this.TextArea("Precondition").Label("Precondition:")%>
               </p>
      
                <p>
                    <%= this.TextArea("Postcondition").Label("Postcondition:")%>
               </p>
           
                <p>
                 <%= this.Select("RequirementCategory")
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


    
   