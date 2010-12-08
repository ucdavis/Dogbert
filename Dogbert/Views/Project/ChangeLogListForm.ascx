<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Dogbert.Core.Domain.ChangeLog>>" %>
<%@ Import Namespace="Dogbert.Core.Domain"%>
<%@ Import Namespace="Dogbert.Controllers"%>
<%@ Import Namespace="Dogbert.Helpers" %>

    



        <% Html.Grid(Model.OrderByDescending(a => a.Updated))
            .Name("ChangeLog")
            .CellAction(cell =>
                            {
                                switch (cell.Column.Member)
                                {
                                    case "Created":
                                        cell.Text = string.Format("{0:d}", cell.DataItem.Created);
                                        break;
                                    case "Updated":
                                        cell.Text = string.Format("{0:d}", cell.DataItem.Updated);
                                        break;
                                }
                            })
            .Columns(col =>
                   {
                       col.Add(a =>
                                   {%>
                                    <%= Html.ActionLink<ChangeLogController>(b => b.Edit(a.Id), "Edit")%>
                                   <%});
                       col.Add(a =>
                                   {%>
                                    <%=Html.HtmlEncode(a.Change)%>
                                    <%}
                           ).Title(("Description"));
                       col.Add(a => a.Reason);
                       col.Add(a => a.RequestedBy);
                       col.Add(a => a.Created).Title("Date Added");
                       col.Add(a => a.Updated).Title("Last Modified");
                   })
           // .Groupable(settings => settings.Groups(groups=>{ groups.Add(a => a.Category.Name);}))
            .Render(); %>

