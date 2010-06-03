<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IList<Dogbert.Core.Domain.Project>>" %>
<%@ Import Namespace="Dogbert.Helpers"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Designer
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

        <%
        Html.Grid(Model)
            .Name("Projects")
            .Columns(col =>
                   {
                       col.Add(a => a.Name);
                       col.Add(a => a.Unit);
                       col.Add(a => a.StartDateString).Title("Start Date");
                       col.Add(a => a.EndDateString).Title("End Date");
                       col.Add(a => a.Complexity);
                       col.Add(a => a.StatusCode.Name).Title("Status");
                   })
            .Render(); %>

</asp:Content>

