<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<ProjectFile>>" %>
<%@ Import Namespace="Dogbert.Core.Domain"%>
<%@ Import Namespace="Dogbert.Controllers"%>
<%@ Import Namespace="Dogbert.Helpers"%>

    <%= Html.ValidationSummary() %>

        <fieldset>
            <legend>Project Text</legend>
      
              
              <% Html.Grid(Model.OrderBy(a => a.Type.Name))
               .Transactional()
               .Name("Texts")
               .PrefixUrlParameters(false)
               .RowAction(r=>r.HtmlAttributes.Add("Id", r.DataItem.Id))
               .Columns(col =>
                  {
                        col.Add(item =>
                          { %>
                            <%=Html.ActionLink<ProjectFileController>(a => a.ViewFile(item.Id), "View")
                            %>
                            <% }).Width(25);
                        col.Add(item =>
                          { %>
                            <%=Html.ActionLink<ProjectFileController>(a => a.Edit(item.Id), "Update")
                            %>
                            <% }).Width(25);
                        col.Add(item =>
                          { %>
                            <%=Html.ActionLink<ProjectFileController>(a => a.Remove(item.Id), "Remove")
                            %>
                            <% }).Width(25);
                         col.Add(item => item.FileName);
                         col.Add(item =>{ %> <%=Html.HtmlEncode(item.Type.Name)%>
                                <% }).Title("File Type");
                         col.Add(item =>
                          { %>
                            <%=Html.Encode(String.Format("{0:g}", item.DateAdded))
                            %>
                            <% }).Title("Date Added");
                            
                         col.Add(item =>
                          { %>
                            <%=Html.Encode(String.Format("{0:g}", item.DateChanged))
                            %>
                            <% }).Title("Date Changed");
                            })
                .Render(); %>
             
            
        </fieldset>



    



