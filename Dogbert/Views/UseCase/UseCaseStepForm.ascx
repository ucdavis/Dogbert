<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Dogbert.Controllers.ViewModels.UseCaseViewModel>" %>
<%@ Import Namespace="xVal.Html"%>
<%@ Import Namespace="Dogbert.Core.Domain"%>
<%@ Import Namespace="Dogbert.Helpers"%>


<script src="<%= Url.Content("~/Scripts/jquery.enableTinyMce.js") %>" type="text/javascript"></script>

<script type="text/javascript">

    $(document).ready(function() {

        $("textarea#UseCaseStep_Description").enableTinyMce({ script_location: '<%= Url.Content("~/Scripts/tiny_mce/tiny_mce.js") %>', overrideHeight: '225', overrideWidth: '925' });
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



    
   