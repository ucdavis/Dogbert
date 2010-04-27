<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Dogbert.Core.Domain.ProjectFile>>" %>
<%@ Import Namespace="Dogbert.Core.Domain"%>
<%@ Import Namespace="Dogbert.Controllers"%>

    <table>
        <tr>
            <th></th>
            <th>
                FileName
            </th>
            <th>
                DateAdded
            </th>
            <th>
                DateChanged
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%ProjectFile projectFile = item;%>
                <%= Html.ActionLink<ProjectFileController>(a => a.Edit(projectFile.Id), "Edit")%>
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



