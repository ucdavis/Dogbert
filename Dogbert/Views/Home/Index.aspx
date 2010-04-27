<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IQueryable<Dogbert.Core.Domain.Project>>" %>
<%@ Import Namespace="Dogbert.Helpers"%>
<%@ Import Namespace="Dogbert.Controllers" %>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <% Html.Grid(Model.OrderBy(a => a.ProjectType).ThenBy(a => a.Priority))
           .PrefixUrlParameters(false)
           .Name("Projects")
           .Columns(col =>
                        {
                            col.Add(a => a.Name);
                            col.Add(a => a.Unit);
                            col.Add(a => a.StartDateString).Title("Start Date");
                            col.Add(a => a.EndDateString).Title("End Date");
                            col.Add(a => a.Complexity);
                            col.Add(a => a.ProjectType.Name).Title("Project Type");
                            col.Add(a => a.StatusCode.Name).Title("Status");
                        })
           .Sortable()
           .Render();
            %>
    
    
    <%= Html.ActionLink<ProjectController>(a => a.DynamicIndex(), "Projects")%>
    
</asp:Content>
