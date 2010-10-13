<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Dogbert.Controllers.ViewModels.UseCaseViewModel>" %>
<%@ Import Namespace="xVal.Html"%>
<%@ Import Namespace="Dogbert.Core.Domain"%>
<%@ Import Namespace="Dogbert.Helpers"%>

    <script src="<%= Url.Content("~/Scripts/tiny_mce/jquery.tinymce.js") %>" type="text/javascript"></script>
    <script src="<%= Url.Content("~/Scripts/jquery.enableTinyMce.js") %>" type="text/javascript"></script>

<script type="text/javascript">
    $(document).ready(function() {
        $("textarea#UseCase_Description").enableTinyMce({ script_location: '<%= Url.Content("~/Scripts/tiny_mce/tiny_mce.js") %>', overrideHeight: '225', overrideWidth: '925' });
        $("textarea#UseCase_Precondition").enableTinyMce({ script_location: '<%= Url.Content("~/Scripts/tiny_mce/tiny_mce.js") %>', overrideHeight: '225', overrideWidth: '925' });
        $("textarea#UseCase_Postcondition").enableTinyMce({ script_location: '<%= Url.Content("~/Scripts/tiny_mce/tiny_mce.js") %>', overrideHeight: '225', overrideWidth: '925' });
    });//end ready

</script>



<%--<%= Html.ClientSideValidation<Project>("Project") %>--%>
     <%= Html.ValidationSummary() %>
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
                    .Options(Model.RequirementCategories, x=>x.Id, x=>x.Name)
                    .Selected(Model.UseCase != null && Model.UseCase.RequirementCategory != null ? Model.UseCase.RequirementCategory.Id : 1)
                    .FirstOption("--Requirement Category--")
                    .HideFirstOptionWhen(Model.RequirementCategories!= null)
                    .Label("Requirement Category:")%>
               </p>
               
               <p>
                   <%--List Actors--%>
                   <% Html.Grid(Model.UseCase.Actors)
                   .Transactional()
                   .Name("UseCaseActors")
                   .PrefixUrlParameters(false)
                   .Columns(col =>
                      {             col.Add(a => a.Name);
                                })
                    .Render(); %>
                </p>
                <p>
                 <%= this.MultiSelect("UseCase.Actors")
                    .Options(Model.Actors, x=>x.Id, x=>x.Name)
                    .Selected(Model.UseCase != null && Model.UseCase.Actors != null ? Model.UseCase.Actors : new List<Actor>()) 
                    .FirstOption("--Actors--")
                    .HideFirstOptionWhen(Model.Actors!= null)
                    .Label("Re-Select Actors:")%>
               </p>
               
   
        </fieldset>
         



    
   