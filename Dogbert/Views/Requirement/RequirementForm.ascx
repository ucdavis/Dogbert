<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Dogbert.Controllers.ViewModels.RequirementViewModel>" %>

    <script src="<%= Url.Content("~/Scripts/tiny_mce/jquery.tinymce.js") %>" type="text/javascript"></script>
    <script src="<%= Url.Content("~/Scripts/jquery.enableTinyMce.js") %>" type="text/javascript"></script>
 
<script type="text/javascript">
    $(document).ready(function() {
        $("textarea#Requirement_Description").enableTinyMce({ script_location: '<%= Url.Content("~/Scripts/tiny_mce/tiny_mce.js") %>', overrideHeight: '225', overrideWidth: '925' });
    });//end ready

</script>
    
    <%= Html.AntiForgeryToken() %>
        <fieldset>
            <legend>Requirements</legend>
            <p>
                <label for="Description">Description:</label>
                <%--<%= this.TextArea("Requirement.Description") %>--%>
                <%= Html.TextArea("Requirement.Description", new { style = "width: 500px; height: 300px" })%>
                <%= Html.ValidationMessage("Requirement.Description", "*")%> 
            </p>
            <p>
                <label for="TechnicalDifficulty">Technical Difficulty:</label>
                <%= this.TextBox("Requirement.TechnicalDifficulty")%>
                <%= Html.ValidationMessage("Requirement.TechnicalDifficulty", "*")%>
            </p>
            <p>
                <%= this.Select("Requirement.RequirementType").Options(Model.RequirementTypes, x => x.Id, x => x.Name)
                        .FirstOption("--Select a Requirement Type--")
                        .HideFirstOptionWhen(Model.Requirement.RequirementType != null && Model.Requirement.RequirementType.IsActive)                
                        .Label("Requirement Type:")
                        .Selected(Model.Requirement.RequirementType != null ? Model.Requirement.RequirementType.Id : string.Empty )
                %>
                <%= Html.ValidationMessage("Requirement.RequirementType", "*")%>
            </p>
            <p>
                <%= this.Select("Requirement.PriorityType").Options(Model.PriorityTypes, x => x.Id, x => x.Name)
                        .FirstOption("--Select a Priority Type--")
                        .HideFirstOptionWhen(Model.Requirement.PriorityType != null && Model.Requirement.PriorityType.IsActive)
                        .Label("Priority Type:")
                        .Selected(Model.Requirement.PriorityType != null ? Model.Requirement.PriorityType.Id.ToString() : string.Empty)
                %>
                <%= Html.ValidationMessage("Requirement.PriorityType", "*")%>                
            </p>
            <p>
                <%= this.Select("Requirement.Category").Options(Model.Project.RequirementCategories.Where(a => a.IsActive).OrderBy(a=>a.Name), x => x.Id, x => x.Name)
                        .FirstOption("--Select a Category--")
                        .HideFirstOptionWhen(Model.Requirement.Category != null && Model.Requirement.Category.IsActive)
                        .Label("Category Type:")
                        .Selected(Model.Requirement.Category != null ? Model.Requirement.Category.Id.ToString(): string.Empty)
                                                                    
                %>
                <%= Html.ValidationMessage("Requirement.Category", "*")%>                               
            </p>
            <p>
                <label for="IsComplete">IsComplete:</label>
                <%= this.CheckBox("Requirement.IsComplete")%>
                <%= Html.ValidationMessage("Requirement.IsComplete", "*")%>
            </p>
             <p>
                <label for="VersionCompleted">Version Completed:</label>
                <%= this.TextBox("Requirement.VersionCompleted")%>
                <%= Html.ValidationMessage("Requirement.VersionCompleted", "*")%>
            </p>
            
        </fieldset>

   
