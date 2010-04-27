<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Dogbert.Core.Domain.Project>" %>
<%@ Import Namespace="Dogbert.Controllers"%>
<%@ Import Namespace="Dogbert.Helpers"%>

    <p>
        <%= Html.ActionLink<RequirementCategoryController>(a => a.Create(Model.Id), "Create") %>
    </p>

    <%      
        Html.Grid(Model.RequirementCategories.Where(a => a.IsActive))
       .Transactional()
       .Name("RequirementCategories")
       .Columns(col =>
                    {
                        col.Add(project =>
                                    {%>
                                    
                                        <%--<%= Html.ActionLink<RequirementCategoryController>(a => a.Create(Model.Id)) %>--%>
                                    
                                    <%});
                        col.Add(project => project.Id);
                        col.Add(project => project.Name);
                    })
                
        .Render(); %>





