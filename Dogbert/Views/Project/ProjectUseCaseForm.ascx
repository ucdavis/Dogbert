<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Dogbert.Controllers.ViewModels.UseCaseViewModel>" %>
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
                                Html.ActionLink<Dogbert.Controllers.UseCaseController>(a => a.Edit(pUseCase.Id), "Edit")
                            %>
                            <% });
                                col.Add(pUseCase => pUseCase.Name);
                                col.Add(pUseCase => pUseCase.Description);
                                col.Add(pUseCase => pUseCase.RequirementCategory.Name);
                  
                            })
                .Render(); %>
         
       
                </fieldset>
                    
            <%} %>
            
         



    
   