<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Dogbert.Controllers.ViewModels.UseCaseViewModel>" %>
<%@ Import Namespace="xVal.Html"%>
<%@ Import Namespace="Dogbert.Core.Domain"%>
<%@ Import Namespace="Dogbert.Helpers"%>

    

<%--<%= Html.ClientSideValidation<Project>("Project") %>--%>
    <%= Html.ValidationSummary() %>
              
        <fieldset>
            <legend>Use Case Children</legend>
            <% using (Html.BeginForm())
               { %>
                    <%= Html.AntiForgeryToken() %>
                    <%= Html.Hidden("UseCaseId", Model.UseCase.Id) %>
                    <%= Html.Hidden("projectID", Model.UseCase.Project.Id) %>

               <% Html.Grid(Model.UseCase.Children)
               .Transactional()
               .Name("UseCaseChildren")
               .PrefixUrlParameters(false)
               .RowAction(r=>r.HtmlAttributes.Add("Id", r.DataItem.Id))
               .Columns(col =>
                  {
                                col.Add(a => a.Name);
                            })
                .Render(); %>
              
      
            <%} %>
                      
        </fieldset>
         



    
   