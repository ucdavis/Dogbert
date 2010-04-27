<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<ProjectFile>>" %>
<%@ Import Namespace="Dogbert.Core.Domain"%>
<%@ Import Namespace="Dogbert.Controllers"%>

    <table>
        <tr>
            <th></th>
            <th>
                File Name
            </th>
            <th>
                Date Added
            </th>
            <th>
                Date Changed
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%ProjectFile projectFile = item;%>
                <%= Html.ActionLink<ProjectFileController>(a => a.Edit(projectFile.Id), "Edit")%> |
                <%= Html.ActionLink<ProjectFileController>(a => a.Remove(projectFile.Id), "Remove") %>
            </td>
            <td>
                <%= Html.Encode(item.FileName) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:g}", item.DateAdded)) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:g}", item.DateChanged)) %>
            </td>
        </tr>
    
    <% } %>

    </table>



