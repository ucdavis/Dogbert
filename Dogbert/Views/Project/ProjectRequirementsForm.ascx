<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Dogbert.Controllers.ViewModels.ProjectViewModel>" %>
<%@ Import Namespace="xVal.Html"%>
<%@ Import Namespace="Dogbert.Core.Domain"%>
<%@ Import Namespace="Dogbert.Helpers"%>

<%--<%= Html.ClientSideValidation<Project>("Project") %>--%>
    <%= Html.ValidationSummary() %>

<%--<%          Adds Project Text%>      --%>
        <fieldset>
            <legend>Project Requirements</legend>
            <% using (Html.BeginForm("CreateRequirements", "Project", FormMethod.Post))
               { %>
                <%= Html.AntiForgeryToken() %>
                <%= Html.Hidden("projectId", Model.Project.Id) %>
                <p>
                    <%= this.TextBox("Description").Label("Use Case Name:")%>
                </p>
           
                <p>
                 <%= this.Select("RequirementType")
                    .Options(Model.RequirementTypes, x=>x.Id, x=>x.Name)
                     .Selected(Model.Requirement  != null && Model.Requirement.RequirementType != null ? Model.Requirement.RequirementType.Id : 1)
                    .FirstOption("--Requirement Type--")
                    .HideFirstOptionWhen(Model.RequirementTypes != null)
                    .Label("Requirement Type:")%>
               </p>
               
                <p>
                 <%= this.Select("RequirementType")
                    .Options(Model.RequirementTypes, x=>x.Id, x=>x.Name)
                     .Selected(Model.Requirement  != null && Model.Requirement.RequirementType != null ? Model.Requirement.RequirementType.Id : 1)
                    .FirstOption("--Requirement Type--")
                    .HideFirstOptionWhen(Model.RequirementTypes != null)
                    .Label("Requirement Type:")%>
               </p>
               
               
         
           
                <p>
                    <input type="submit" value="Save" />
                </p>
            <%} %>
            
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
            
        </fieldset>


    
   