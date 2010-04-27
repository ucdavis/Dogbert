<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Dogbert.Controllers.ViewModels.ProjectExtViewModel>" %>
<%@ Import Namespace="xVal.Html"%>
<%@ Import Namespace="Dogbert.Core.Domain"%>

<%= Html.ClientSideValidation<Project>("Project") %>
    <%= Html.ValidationSummary() %>


    <% using (Html.BeginForm()) {%>

        <%= Html.AntiForgeryToken() %>
        <%= Model.Project!=null ? Html.HiddenFor(p=>p.Project.Id) : ""%>
 
        
        <fieldset>
            <legend>Fields</legend>
            <p>
                <%= this.TextBox("ProjectText.Text").Label("Text:")%>
            </p>
            <p>
             <%= this.Select("ProjectText.TextTypes")
                .Options(Model.TextTypes, x=>x.Id, x=>x.Name)
                .Selected(Model.ProjectText != null && Model.ProjectText.TextType != null ? Model.ProjectText.TextType.Id : "DE")             
                .FirstOption("--Select Project Type--")
                .HideFirstOptionWhen(Model.TextTypes!= null)
                .Label("Project Type:")%>
           </p>
  
            <p>
                <input type="submit" value="Save" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%=Html.ActionLink("Back to List", "Index") %>
    </div>
    
   