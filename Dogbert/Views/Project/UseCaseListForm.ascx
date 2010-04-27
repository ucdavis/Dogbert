<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Dogbert.Core.Domain.UseCase>>" %>
<%@ Import Namespace="xVal.Html"%>
<%@ Import Namespace="Dogbert.Core.Domain"%>
<%@ Import Namespace="Dogbert.Helpers"%>

 <table>
        <tr>
            <th></th>
            <th>
                Name
            </th>
            <th>
                Description
            </th>
            <th>
                RequirementCategory
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
               <%=  Html.ActionLink<Dogbert.Controllers.UseCaseController>(a => a.Edit(id), "Edit")%>
            </td>
            <td>
                <%= Html.Encode(item.Name) %>
            </td>
            
            <td>
                <%= Html.Encode(item.Description) %>
            </td>
            <td>
                <%= Html.Encode(item.RequirementCategory.Name) %>
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