<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Dogbert.Controllers.ViewModels.ChangeLogViewModel>" %>

    <script src="<%= Url.Content("~/Scripts/tiny_mce/jquery.tinymce.js") %>" type="text/javascript"></script>
    <script src="<%= Url.Content("~/Scripts/jquery.enableTinyMce.js") %>" type="text/javascript"></script>
 
<script type="text/javascript">
    $(document).ready(function() {
  
    });   //end ready
    
</script>

    <%= Html.AntiForgeryToken() %>
        <fieldset class="changelogform">
            <legend>Change Log</legend>
            
            <div> 
                <label for="Change">Description:</label>
                <%= Html.HtmlEncode(Model.ChangeLog.Change)%>
            </div>
           
            <div> 
                <label for="RequestedBy">Requested By:</label>
                <%= Html.HtmlEncode(Model.ChangeLog.RequestedBy)%>
            </div>
          
            <div> 
                <label for="Reason">Reason:</label>
                <%= Html.HtmlEncode(Model.ChangeLog.Reason)%>
            </div>
        </fieldset>