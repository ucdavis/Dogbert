<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Dogbert.Core.Domain.Requirement>>" %>
<%@ Import Namespace="Dogbert.Core.Domain"%>
<%@ Import Namespace="Dogbert.Controllers"%>
<%@ Import Namespace="Dogbert.Helpers" %>

    
    <% Html.Grid(Model.OrderBy(a => a.Category.Id))
            .Name("Requirements")
            .CellAction(cell =>
                            {
                                switch (cell.Column.Member)
                                {
                                    case "DateAdded":
                                        cell.Text = string.Format("{0:d}", cell.DataItem.DateAdded);
                                        break;
                                    case "LastModified":
                                        cell.Text = string.Format("{0:d}", cell.DataItem.LastModified);
                                        break;
                                    case "IsComplete":
                                        cell.Text = cell.DataItem.IsComplete ? "x" : string.Empty;
                                        break;
                                }
                            })
            .Columns(col =>
                   {
                       col.Add(a =>
                                   {%>
                                    <%= Html.ActionLink<RequirementController>(b => b.Edit(a.Id), "Edit")%>
                                   <%});
                       col.Add(a =>
                                   {%>
                                    <%=Html.HtmlEncode(a.Description)%>
                                    <%}
                           ).Title(("Description"));
                       col.Add(a => a.TechnicalDifficulty);
                       col.Add(a => a.IsComplete);
                       col.Add(a => a.DateAdded);
                       col.Add(a => a.LastModified);
                       //col.Add(a => a.Category.Name).Title("Category");
                   })
            .Groupable(settings => settings.Groups(groups=>{ groups.Add(a => a.Category.Name);}).Enabled(false))
            .Render(); %>


  <%--  <table>
        <tr>
            <th></th>
            <th>
                Description
            </th>
            <th>
                TechnicalDifficulty
            </th>
            <th>
                IsComplete
            </th>
            <th>
                DateAdded
            </th>
            <th>
                LastModified
            </th>
            <th>
                Category
            </th>
        </tr>

    <% foreach (var item in Model.OrderBy(a => a.Category.Id)) { %>
    
        <tr>
            <td>
                <!--<%= Html.ActionLink("Edit", "Edit", new { id=item.Project.Id }) %>--> 
                <%int id = item.Id;%>
                <%= Html.ActionLink<RequirementController>(a => a.Edit(id), "Edit")%>
            </td>
            <td>
                <%= Html.Encode(item.Description) %>
            </td>
            <td>
                <%= Html.Encode(item.TechnicalDifficulty) %>
            </td>
            <td>
                <%= Html.Encode(item.IsComplete) %> 
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:g}", item.DateAdded)) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:g}", item.LastModified)) %>
            </td>
            <td>
                <%= Html.Encode(item.Category.Name) %>
            </td>
        </tr>
    
    <% } %>

    </table>--%>


