<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Dogbert.Controllers.ViewModels.RequirementViewModel>" %>

    <script src="<%= Url.Content("~/Scripts/tiny_mce/jquery.tinymce.js") %>" type="text/javascript"></script>
    <script src="<%= Url.Content("~/Scripts/jquery.enableTinyMce.js") %>" type="text/javascript"></script>
 
<script type="text/javascript">
    $(document).ready(function() {
        $("textarea#Requirement_Description").enableTinyMce({ script_location: '<%= Url.Content("~/Scripts/tiny_mce/tiny_mce.js") %>', overrideHeight: '225', overrideWidth: '925', overrideOnchange: 'myCustomOnChangeHandler' });

        //set slider
        $("#slider").slider({
            range: "min",
            value: $("#" + "Requirement_TechnicalDifficulty").val(),
            min: 1,
            max: 10,
            slide: function(event, ui) {
                $("#" + "Requirement_TechnicalDifficulty").val(ui.value);
            }
        });

        //set value on proj complexity
        $("#" + "Requirement_TechnicalDifficulty").val($("#slider").slider("value"));
    
    
    });   //end ready
   
   
    $("form").submit(function() {
    tinyMCE.triggerSave();
  
    });

    //doesn't work!!
    function myCustomOnChangeHandler(inst) {
        tinyMCE.triggerSave();
        tinyMCE.get("textarea#Requirement_Description").save();
        $("textarea#Requirement_Description").validate().element(this);
        $("textarea#Requirement_Description").val(content); // put it in the textarea
    }

  
   
  
</script>


  <style type="text/css">
    #slider { margin: 0px; 
              margin-top: 0px;
              margin-left: 5px;
              width: 300px;
              display: inline-block; }
    #Requirement_TechnicalDifficulty{width: 30px; }
    
  
  </style>
    
    <%= Html.AntiForgeryToken() %>
        <fieldset>
            <legend>Requirements</legend>
            
            <div> 
                <label for="Description">Description:</label>
                <%= Html.TextArea("Requirement.Description", new { style = "width: 500px; height: 300px" })%>
                <%= Html.ValidationMessage("Requirement.Description")%> 
            </div>
            
            <div>
                <label for="TechnicalDifficulty">Technical Difficulty:</label>
                <%= this.TextBox("Requirement.TechnicalDifficulty")%>
                <%= Html.ValidationMessage("Requirement.TechnicalDifficulty")%>
                  <span id="slider"></span>
            </div>
            <div>
                <%= this.Select("Requirement.RequirementType").Options(Model.RequirementTypes, x => x.Id, x => x.Name)
                        .FirstOption("--Select a Requirement Type--")
                        .HideFirstOptionWhen(Model.Requirement.RequirementType != null && Model.Requirement.RequirementType.IsActive)                
                        .Label("Requirement Type:")
                        .Selected(Model.Requirement.RequirementType != null ? Model.Requirement.RequirementType.Id : string.Empty )
                %>
                <%= Html.ValidationMessage("Requirement.RequirementType")%>
            </div>
            <div>
                <%= this.Select("Requirement.PriorityType").Options(Model.PriorityTypes, x => x.Id, x => x.Name)
                        .FirstOption("--Select a Priority Type--")
                        .HideFirstOptionWhen(Model.Requirement.PriorityType != null && Model.Requirement.PriorityType.IsActive)
                        .Label("Priority Type:")
                        .Selected(Model.Requirement.PriorityType != null ? Model.Requirement.PriorityType.Id.ToString() : string.Empty)
                %>
                <%= Html.ValidationMessage("Requirement.PriorityType")%>                
            </div>
            <div>
                <%= this.Select("Requirement.Category").Options(Model.Project.RequirementCategories.Where(a => a.IsActive).OrderBy(a=>a.Name), x => x.Id, x => x.Name)
                        .FirstOption("--Select a Category--")
                        .HideFirstOptionWhen(Model.Requirement.Category != null && Model.Requirement.Category.IsActive)
                        .Label("Category Type:")
                        .Selected(Model.Requirement.Category != null ? Model.Requirement.Category.Id.ToString(): string.Empty)
                                                                    
                %>
                <%= Html.ValidationMessage("Requirement.Category")%>                               
            </div>
            <div>
                <label for="IsComplete">IsComplete:</label>
                <%= this.CheckBox("Requirement.IsComplete")%>
                <%= Html.ValidationMessage("Requirement.IsComplete")%>
            </div>
             <div>
                <label for="VersionCompleted">Version Completed:</label>
                <%= this.TextBox("Requirement.VersionCompleted")%>
                <%= Html.ValidationMessage("Requirement.VersionCompleted")%>
            </div>
            
        </fieldset>

   
