<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Dogbert.Controllers.ViewModels.RequirementViewModel>" %>


<script type="text/javascript">
    $(document).ready(function() {

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
   
   
 
  
    });

    
  
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
                 <%= Html.HtmlEncode(Model.Requirement.Description)%>
            </div>
            
            <div>
                <label for="TechnicalDifficulty">Technical Difficulty:</label>
                <%= this.Label("Requirement.TechnicalDifficulty")%>
                  <span id="slider"></span>
            </div>
            <div>
              <label for="RequirementType">Requirement Type:</label>
                <%= Html.HtmlEncode(Model.Requirement.RequirementType.Name)%>
            </div>
            <div>
               <label for="PriorityType">PriorityTypee:</label>
                <%= Html.HtmlEncode(Model.Requirement.PriorityType.Name)%>
            </div>
            <div>
              <label for="Category">Category:</label>
                <%=  Html.HtmlEncode(Model.Requirement.Category.Name)%>                                                       
            </div>
            <div>
                <label for="IsComplete">IsComplete:</label>
                <%= this.CheckBox("Requirement.IsComplete")%>
            </div>
             <div>
                <label for="VersionCompleted">Version Completed:</label>
                <%= Html.HtmlEncode(Model.Requirement.VersionCompleted)%>
            </div>
            
        </fieldset>