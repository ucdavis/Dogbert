<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Dogbert.Controllers.ViewModels.ProjectViewModel>" %>
<%@ Import Namespace="xVal.Html"%>
<%@ Import Namespace="Dogbert.Core.Domain"%>
<%@ Import Namespace="Dogbert.Helpers"%>

<script src="<%= Url.Content("~/Scripts/jquery.enableTinyMce.js") %>" type="text/javascript"></script>

<script type="text/javascript">
    $(document).ready(function() {
        $("textarea#ProjectText_Text").enableTinyMce({ script_location: '<%= Url.Content("~/Scripts/tiny_mce/tiny_mce.js") %>', overrideHeight: '225', overrideWidth: '925' });
    });

</script>

<%--<%= Html.ClientSideValidation<Project>("Project") %>--%>
    <%= Html.ValidationSummary() %>

        <div>
        <%=Html.ActionLink<Dogbert.Controllers.ProjectController>(a => a.Edit(Model.ProjectText.Project.Id), "Back to Project")%>
        </div>
        
        <fieldset>
            <legend>Project Text</legend>
            <% using (Html.BeginForm())
               { %>
                <%= Html.AntiForgeryToken() %>
                <%= Html.Hidden("Id", Model.ProjectText.Id) %>
                <p>
                    <%= this.TextArea("ProjectText.Text").Label("Text:")%>
                </p>
                <p>
                 <%= this.Select("ProjectText.TextType")
                    .Options(Model.TextTypes, x=>x.Id, x=>x.Name)
                    .Selected(Model.ProjectText != null && Model.ProjectText.TextType != null ? Model.ProjectText.TextType.Id : "DE")             
                    .FirstOption("--Text Type--")
                    .HideFirstOptionWhen(Model.TextTypes!= null)
                    .Label("Text Type:")%>
               </p>
      
                <p>
                    <input type="submit" value="Save" />
                </p>
            <%} %>
            
            
        </fieldset>


    
   