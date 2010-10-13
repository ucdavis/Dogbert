<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Dogbert.Controllers.CreateProjectTextViewModel>" %>


<script src="<%= Url.Content("~/Scripts/tiny_mce/jquery.tinymce.js") %>" type="text/javascript"></script>
<script src="<%= Url.Content("~/Scripts/jquery.enableTinyMce.js") %>" type="text/javascript"></script>

<script type="text/javascript">
    $(document).ready(function() {
        $("textarea#Text").enableTinyMce({ script_location: '<%= Url.Content("~/Scripts/tiny_mce/tiny_mce.js") %>', overrideHeight: '225', overrideWidth: '925' });

    });
</script>

    <% using (Html.BeginForm()) { %>
        <%= Html.AntiForgeryToken() %>
        <%= Html.Hidden("projectId", Model.Project.Id) %>
        
        <p>
            <%--<%= this.TextArea("Text").Label("Text:")%>--%>
            <label for="Text">Text:</label>
            <%= Html.TextArea("Text", Model.ProjectText != null ? Model.ProjectText.Text : string.Empty) %>
        </p>
        <p>
         <%= this.Select("TextType")
            .Options(Model.TextType, x=>x.Id, x=>x.Name)
            .Selected(Model.ProjectText != null && Model.ProjectText.TextType != null ? Model.ProjectText.TextType.Id : "DE")             
            .FirstOption("--Text Type--")
            .HideFirstOptionWhen(Model.TextType != null)
            .Label("Text Type:")%>
       </p>

        <p>
            <input type="submit" value="Save" />
        </p>
    <%} %>