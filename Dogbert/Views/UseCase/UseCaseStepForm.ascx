<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Dogbert.Controllers.ViewModels.UseCaseViewModel>" %>
<%@ Import Namespace="xVal.Html"%>
<%@ Import Namespace="Dogbert.Core.Domain"%>
<%@ Import Namespace="Dogbert.Helpers"%>

<script type="text/javascript">

    $(document).ready(function() {
    $("textarea#UseCaseStep_Description").tinymce({
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

      }); //end ready

</script>


<%--<%= Html.ClientSideValidation<Project>("Project") %>--%>
     <%= Html.ValidationSummary() %>

          <fieldset>
            <legend>Use Case Steps</legend>
            <% using (Html.BeginForm())
               { %>
                    <%= Html.AntiForgeryToken() %>
                <p>
                    <%= this.TextBox ("UseCaseStep.Order").Label("Order:")%>
               </p>
   
                <p>
                    <%= this.TextArea("UseCaseStep.Description").Label("Description:")%>
               </p>
               <p>
                    <%= this.CheckBox("UseCaseStep.Optional").Label("Optional")%>
               </p>
      
                <p>
                    <input type="submit" value="Save" />
                </p>
                
      
            <%} %>
                      
        </fieldset>



    
   