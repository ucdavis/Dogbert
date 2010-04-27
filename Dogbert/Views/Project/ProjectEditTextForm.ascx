<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Dogbert.Controllers.ViewModels.ProjectViewModel>" %>
<%@ Import Namespace="xVal.Html"%>
<%@ Import Namespace="Dogbert.Core.Domain"%>
<%@ Import Namespace="Dogbert.Helpers"%>


<script type="text/javascript">

    $(document).ready(function() {
    $("textarea#ProjectText_Text").tinymce({
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
            height: "400",
            width: "800",

            // Example content CSS (should be your site CSS)
            //content_css: "css/Main.css",

            // Drop lists for link/image/media/template dialogs
            template_external_list_url: "js/template_list.js",
            external_link_list_url: "js/link_list.js",
            external_image_list_url: "js/image_list.js",
            media_external_list_url: "js/media_list.js"
        });
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


    
   