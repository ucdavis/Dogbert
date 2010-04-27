<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Dogbert.Controllers.ViewModels.UseCaseViewModel>" %>
<%@ Import Namespace="xVal.Html"%>
<%@ Import Namespace="Dogbert.Core.Domain"%>
<%@ Import Namespace="Dogbert.Helpers"%>


<script type="text/javascript">

    $(document).ready(function() {
    $("textarea#UseCase_Description").tinymce({
            script_url: '../../Scripts/tiny_mce/tiny_mce.js',
            // General options
            theme: "advanced",
            plugins: "safari,style,save,searchreplace,print,contextmenu,paste",

            // Theme options
            theme_advanced_buttons1: "print,|,bold,italic,underline,|,styleselect,formatselect,fontselect,fontsizeselect",
            theme_advanced_buttons2: "cut,copy,paste,pastetext,pasteword,|,search,replace,|,undo,redo,|,bullist,numlist",
            theme_advanced_buttons3: "",
            theme_advanced_toolbar_location: "top",
            theme_advanced_toolbar_align: "left",
            theme_advanced_statusbar_location: "bottom",
            theme_advanced_resizing: false,

            // dimensions stuff
            height: "200",

            // Example content CSS (should be your site CSS)
            //content_css: "css/Main.css",

            // Drop lists for link/image/media/template dialogs
            template_external_list_url: "js/template_list.js",
            external_link_list_url: "js/link_list.js",
            external_image_list_url: "js/image_list.js",
            media_external_list_url: "js/media_list.js"
        });

        $("textarea#UseCase_Precondition").tinymce({
            script_url: '../../Scripts/tiny_mce/tiny_mce.js',
            // dimensions stuff
            height: "100",
            width: "400"
            
        });
    
        $("textarea#UseCase_Postcondition").tinymce({
            // dimensions stuff
            height: "100",
            width: "400"
        });
    });//end ready

</script>

<%--<%= Html.ClientSideValidation<Project>("Project") %>--%>
 <%--    <%= Html.ValidationSummary() %>
 --%>     
        <%= Html.AntiForgeryToken() %>
        <fieldset>
            <legend>Use Case</legend>
                   <%-- <%= Html.Hidden("Id", Model.UseCase.Id) %>
                    <%= Html.Hidden("projectID", Model.UseCase.Project.Id) %>--%>
                <p>
                    <%= this.TextBox("UseCase.Name").Label("Use Case Name:")%>
                </p>
                <p>
                    <%= this.TextArea("UseCase.Description").Label("Description:")%>
               </p>
      
                <p>
                    <%= this.TextArea("UseCase.Precondition").Label("Precondition:")%>
               </p>
      
                <p>
                    <%= this.TextArea("UseCase.Postcondition").Label("Postcondition:")%>
               </p>
              
               
                <p>
                 <%= this.Select("UseCase.RequirementCategory")
                    .Options(Model.Project.RequirementCategories.Where(a => a.IsActive), x=>x.Id, x=>x.Name)
                    .Selected(Model.UseCase != null && Model.UseCase.RequirementCategory != null ? Model.UseCase.RequirementCategory.Id : 1)
                    .FirstOption("--Requirement Category--")
                    .HideFirstOptionWhen(Model.RequirementCategories!= null)
                    .Label("Requirement Category:")%>
               </p>
           
               
                <p>
                 <%= this.MultiSelect("UseCase.Actors")
                    .Options(Model.Actors, x=>x.Id, x=>x.Name)
                    .Selected(Model.UseCase != null && Model.UseCase.Actors != null ? Model.UseCase.Actors : new List<Actor>()) 
                    .FirstOption("--Actors--")
                    .HideFirstOptionWhen(Model.Actors!= null)
                    .Label("Select Actors:")%>
                   
               </p>
       
                      
        </fieldset>
         



    
   