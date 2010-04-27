<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Dogbert.Controllers.ViewModels.ProjectViewModel>" %>
<%@ Import Namespace="xVal.Html"%>
<%@ Import Namespace="Dogbert.Core.Domain"%>
<%@ Import Namespace="Dogbert.Helpers"%>

<%--<%= Html.ClientSideValidation<Project>("Project") %>--%>
    <%= Html.ValidationSummary() %>

<%--<%          Add Project Text%>      --%>
        <fieldset>
            <legend>Project Text</legend>
            <% using (Html.BeginForm("CreateText", "Project", FormMethod.Post))
               { %>
                <%= Html.AntiForgeryToken() %>
                <%= Html.Hidden("projectId", Model.Project.Id) %>
                <p>
                    <%= this.TextArea("Text").Label("Text:")%>
                </p>
                <p>
                 <%= this.Select("TextType")
                    .Options(Model.TextTypes, x=>x.Id, x=>x.Name)
                    .Selected(Model.ProjectText != null && Model.ProjectText.TextType != null ? Model.ProjectText.TextType.Id : "DE")             
                    .FirstOption("--Text Type--")
                    .HideFirstOptionWhen(Model.TextTypes!= null)
                    .Label("Text Type:")%>
               </p>
      
                <p>
                    <input type="submit" value="Save" />
                </p>
            <%} %>
            
               <% Html.Grid(Model.Project.ProjectTexts)
               .Transactional()
               .Name("Texts")
               .PrefixUrlParameters(false)
               .RowAction(r=>r.HtmlAttributes.Add("Id", r.DataItem.Id))
               .Columns(col =>
                  {
                      col.Add(pText =>
                          { %>
                            <%=
                                Html.ActionLink<Dogbert.Controllers.ProjectController>(a => a.EditText(pText.Id), "Edit")
                            %>
                            <% });
                            col.Add(pText => pText.TextType.Name);
                            col.Add(pText => pText.Text);
                            })
                .Render(); %>
            
        </fieldset>


    
   