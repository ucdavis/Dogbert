<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Dogbert.Core.Domain.UseCase>>" %>
<%@ Import Namespace="Dogbert.Core.Domain"%>
<%@ Import Namespace="Dogbert.Controllers"%>
<%@ Import Namespace="Dogbert.Helpers" %>

    
    <% Html.Grid(Model.OrderBy(a => a.RequirementCategory.Name))
            .Name("RequirementCategory")
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
                                }
                            })
            .Columns(col =>
                   {
                 
                       col.Add(a =>{%>
                                    <%= Html.ActionLink<UseCaseController>(b => b.Edit(a.Id), "Edit")%>
                                    <%});
                       col.Add(a =>{%>
                                    <%=Html.HtmlEncode(a.Description)%>
                                    <%}).Title("Description");
                       col.Add(a => a.DateAdded);
                       
                       
                   })
                   
            //.Groupable(settings => settings.Groups(groups => { groups.Add(a => a.RequirementCategory.Name); }))
            .Groupable(settings => settings.Groups(groups =>
                           {
                               groups.Add(a => a.RequirementCategory.Name);
                 
                           }))
                        
            .Render(); %>
            
  
 


