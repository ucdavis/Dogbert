<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Dogbert.Core.Domain.Requirement>>" %>
<%@ Import Namespace="Dogbert.Core.Domain"%>
<%@ Import Namespace="Dogbert.Controllers"%>


    <table>
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
                Id
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <!--<%= Html.ActionLink("Edit", "Edit", new { id=item.Project.Id }) %>--> 
                <%int id = item.Id;%>
                <%= Html.ActionLink<RequirementController>(a => a.Edit(id), "Edit")%> |
                <%= Html.ActionLink("Details", "Details", new { /* id=item.PrimaryKey */ })%>
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
                <%= Html.Encode(item.Id) %>
            </td>
        </tr>
    
    <% } %>

    </table>


