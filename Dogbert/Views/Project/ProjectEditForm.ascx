<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Dogbert.Controllers.ViewModels.ProjectViewModel>" %>
<%@ Import Namespace="Dogbert.Helpers"%>
<%@ Import Namespace="Dogbert.Controllers" %>


    <%= Html.ValidationSummary() %>

        <fieldset>
            <legend>Project Text</legend>
    
                <%= Html.ActionLink<ProjectTextController>(a=>a.Create(Model.Project.Id), "Create New") %>
              
              <% Html.Grid(Model.Project.ProjectTexts.OrderBy(a => a.TextType.Id))
               .Transactional()
               .Name("Texts")
               .PrefixUrlParameters(false)
               .RowAction(r=>r.HtmlAttributes.Add("Id", r.DataItem.Id))
               .Columns(col =>
                  {
                      col.Add(pText =>
                          { %>
                            <%=
                                Html.ActionLink<Dogbert.Controllers.ProjectTextController>(a => a.Edit(pText.Id), "Edit")
                            %>
                            <% });
                            col.Add(pText => pText.TextType.Name);
                            col.Add(pText =>{ %> <%=Html.HtmlEncode(pText.Text)%>
                                <% }).Title("Text");
                            })
                .Render(); %>
             
            
        </fieldset>


    
   