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
                        col.Add(project => project.Id).Width(15);
                        col.Add(project => project.Name);
                    })
                
        .Render(); %>





