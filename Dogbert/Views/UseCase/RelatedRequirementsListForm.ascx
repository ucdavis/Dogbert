<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Dogbert.Controllers.ViewModels.UseCaseViewModel>" %>
<%@ Import Namespace="xVal.Html"%>
<%@ Import Namespace="Dogbert.Core.Domain"%>
<%@ Import Namespace="Dogbert.Helpers"%>

    

<%--<%= Html.ClientSideValidation<Project>("Project") %>--%>
    <%= Html.ValidationSummary() %>
              
        <fieldset>
            <legend>Related Requirements</legend>
            <% using (Html.BeginForm())
               { %>
                    <%= Html.AntiForgeryToken() %>
                    <%= Html.Hidden("UseCaseId", Model.UseCase.Id) %>
                    <%= Html.Hidden("projectID", Model.UseCase.Project.Id) %>

               <% Html.Grid(Model.UseCase.Requirements)
               .Transactional()
               .Name("RelatedRequirements")
               .PrefixUrlParameters(false)
               .RowAction(r=>r.HtmlAttributes.Add("Id", r.DataItem.Id))
               .Columns(col =>
                  {
                                col.Add(a => a.Id);
                                col.Add(a =>{ %> <%=Html.HtmlEncode(a.Description)%>
                                         <% }).Title("Description");
                            })
                .Render(); %>
              
      
            <%} %>
                      
        </fieldset>
         



    
   