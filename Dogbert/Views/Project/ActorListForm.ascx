<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Dogbert.Core.Domain.Actor>>" %>
<%@ Import Namespace="Dogbert.Controllers"%>
<%@ Import Namespace="Dogbert.Helpers"%>

    <h3>Changes to Actor list applies to all projects </h3>
     
    <%      
        Html.Grid(Model.OrderBy(a => a.Name).Where(a => a.IsActive))
       .Transactional()
       .Name("Actors")
       .Columns(col =>
                    {
                        col.Add(at =>
                                    {%>
                                    
                                        <%--<%= Html.ActionLink<ActorController>(a => a.Edit(Model.Id)) %>--%>
                                    
                                    <%});
                        col.Add(a => a.Name);
                    })
                
        .Render(); %>





