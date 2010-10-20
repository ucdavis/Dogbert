<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Dogbert.Core.Domain.UseCase>>" %>
<%@ Import Namespace="Dogbert.Controllers.Helpers" %>
<%@ Import Namespace="Dogbert.Controllers" %>

<style type="text/css">
    .style1
    {
        width: 169px;
    }
</style>

<div class="t-widget t-grid norightborder" style="border:none;">
<table cellpadding="0" cellspacing="0" style="border:0px;">
    <thead>
        <tr>
            
        </tr>
    </thead>
    <tbody>
    
        <!-- Get the category names -->
        <% foreach(var cat in Model.OrderBy(a=>a.RequirementCategory.Name).Select(a=>a.RequirementCategory).Distinct()) { %>
        
            <tr style="border-collapse:collapse;">
                <th colspan="5" class="t-header" style="text-align:left; border-collapse:collapse;"><%= Html.Encode(cat.Name) %></th>
            </tr>
            
            <!-- Go through and deal with the use cases them selves -->
            <% foreach(var uc in Model.Where(a=>a.RequirementCategory == cat).OrderBy(a=>a.Id)) { %>
          
                <tr class="t-grid-header">
                    <td><%= Html.ActionLink<UseCaseController>(a => a.Edit(uc.Id), "Edit")%>
                    </td>
                    <td class="space_on_the_right"><strong>Id: </strong><%= Html.Encode(uc.Id) %></td>
                    <td> <%= Html.Encode(uc.Name) %></td>
                    <td class="style1"> <%= "Modified: " + Html.Encode(uc.LastModified.ToShortDateString())%></td>
                </tr>
                <tr>
                    <td></td>
                    <td> <p><strong>Description:</strong></p></td>
                    <td>
                        <%= Html.HtmlEncode(uc.Description) %>
                    </td>
                    <td class="style1"></td>
                </tr>
                <tr>
                   
                    <td></td>
                    <td colspan="2">
                            <p><strong>Steps:</strong></p>
                            <div class="steps">
                                <ol class="steps_table">
                                    <% foreach (var step in uc.Steps) { %>
                                        <li>
                                            <!-- <td><%= Html.Encode(step.Order) %></td> -->
                                            <%= Html.HtmlEncode(step.Description) %>
                                        </li>
                                    <% } %>
                                </ol>
                            </div>
                    </td>
                     <td class="style1"></td>
                    <td></td>
                </tr>
              
            <% } %><!-- end use case loop-->
         
        <% } %><!-- end catetory loop-->
             
    </tbody>
</table>
</div>
