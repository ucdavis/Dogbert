<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IQueryable<Dogbert.Core.Domain.Project>>" %>
<%@ Import Namespace="Dogbert.Helpers"%>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function() { $(".t-grouping-header").hide(); });
    </script>
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <%
        Html.Grid(Model.Where(a=>a.ProjectType.IsActive).OrderBy(a=>a.Priority).ToList())
            .Name("Projects")
            .CellAction(cell=>
                            {
                                //cell.Text = cell.Column.Member;
                            })
            .Columns(col =>
                   {
                       col.Add(a => a.Name);
                       col.Add(a => a.Unit);
                       col.Add(a => a.StartDateString).Title("Start Date");
                       col.Add(a => a.EndDateString).Title("End Date");
                       col.Add(a => a.Complexity);
                       col.Add(a => a.StatusCode.Name).Title("Status");
                   })
            .Groupable(settings => settings.Groups(groups=>{ groups.Add(a => a.ProjectType.Name);}))
            .Render(); %>
    
</asp:Content>
