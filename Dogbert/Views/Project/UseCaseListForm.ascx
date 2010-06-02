<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Dogbert.Core.Domain.UseCase>>" %>
<%@ Import Namespace="Dogbert.Controllers.Helpers" %>


<%--<% Html.Grid(Model)  
       .Name("UseCases")
       .PrefixUrlParameters(false)
       .CellAction(cell =>
                       {
                         switch (cell.Column.Member)
                         {
                             case "Optional":
                                 cell.Text = cell.DataItem.Optional ? "x" : string.Empty;
                                 break;
                         }
                       })
       .Columns(col =>
                    {
                        col.Bound(a => a.Order).Title("Step #");
                        col.Bound(a => a.Description);
                        col.Bound(a => a.Optional);
                    })
       .Groupable(settings => settings.Groups(groups=>
                                                  {
                                                      groups.Add(a => a.UseCase.RequirementCategory.Name);
                                                      groups.Add(a => a.UseCase.Name);
                                                  }))
       .Render();
       %>
--%>


<table border="solid">
    <thead>
        <tr>
            
        </tr>
    </thead>
    <tbody>
    
        <!-- Get the category names -->
        <% foreach(var cat in Model.OrderBy(a=>a.RequirementCategory.Name).Select(a=>a.RequirementCategory).Distinct()) { %>
        
            <tr>
                <th colspan="5" style="text-align:left;"><%= Html.Encode(cat.Name) %></th>
            </tr>
            
            <!-- Go through and deal with the use cases them selves -->
            <% foreach(var uc in Model.Where(a=>a.RequirementCategory == cat).OrderBy(a=>a.Id)) { %>
            
                <tr>
                    <td></td>
                    <td></td>
                    <td><%= Html.Encode(uc.Id) %></td>
                    <td><%= Html.Encode(uc.Name) %></td>
                    <td><%= Html.Encode(uc.LastModified) %></td>
                </tr>
                <tr>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td colspan=2>
                        <p><strong>Description:</strong></p>
                        
                        <%= Html.HtmlEncode(uc.Description) %>
                    </td>
                </tr>
                
                <tr>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td colspan=2>
                        <p><strong>Steps:</strong></p>
                        
                        <div class="steps">
                        
                            <table class="steps_table">
                            
                                <% foreach (var step in uc.Steps) { %>
                                    <tr>
                                        <td><%= Html.Encode(step.Order) %></td>
                                        <td><%= Html.HtmlEncode(step.Description) %></td>
                                    </tr>
                                <% } %>
                            
                            </table>
                        
                        </div>
                    </td>
                </tr>
            
            <% } %>
        
        <% } %>
        
    </tbody>
</table>


 <%--<table>
        <tr>
            <th></th>
            <th>
                Id
            </th>
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
            
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <!--<%= Html.ActionLink("Edit", "Edit", new { id=item.Project.Id }) %>--> 
                <%int id = item.Id;%>
                <%=  Html.ActionLink<Dogbert.Controllers.UseCaseController>(a => a.Edit(id), "Edit")%>
                <%=  Html.ActionLink<Dogbert.Controllers.UseCaseController>(a => a.Remove(id), "Remove")%>
    
            </td>
            <td>
                <%= Html.Encode(item.Id) %>
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
        </tr>
    
    <% } %>

    </table>--%>