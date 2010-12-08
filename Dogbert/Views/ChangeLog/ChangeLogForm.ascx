<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Dogbert.Controllers.ViewModels.ChangeLogViewModel>" %>

    <script src="<%= Url.Content("~/Scripts/tiny_mce/jquery.tinymce.js") %>" type="text/javascript"></script>
    <script src="<%= Url.Content("~/Scripts/jquery.enableTinyMce.js") %>" type="text/javascript"></script>
 
<script type="text/javascript">
    $(document).ready(function() {
    $("textarea#ChangeLog_Change").enableTinyMce({ script_location: '<%= Url.Content("~/Scripts/tiny_mce/tiny_mce.js") %>', overrideHeight: '225', overrideWidth: '925', overrideOnchange: 'myCustomOnChangeHandler' });

    });   //end ready
   
   
    $("form").submit(function() {
    tinyMCE.triggerSave();
  
    });
    
</script>

    <%= Html.AntiForgeryToken() %>
        <fieldset class="changelogform">
            <legend>Change Log</legend>
            
            <div> 
                <label for="Change">Description:</label>
                <%= Html.TextAreaFor(x => x.ChangeLog.Change, new { style = "width: 500px; height: 300px" })%>
                <%= Html.ValidationMessageFor(x => x.ChangeLog.Change)%> 
            </div>
           
            <div> 
                <label for="RequestedBy">Requested By:</label>
                <%= Html.TextBoxFor(x => x.ChangeLog.RequestedBy, new { style = "width: 250px" })%>
                <%= Html.ValidationMessageFor(x => x.ChangeLog.RequestedBy)%> 
            </div>
          
            <div> 
                <label for="Reason">Reason:</label>
                <%= Html.TextBoxFor(x => x.ChangeLog.Reason, new { style = "width: 250px" })%>
                <%= Html.ValidationMessageFor(x => x.ChangeLog.Reason)%> 
            </div>
     
            
        </fieldset>