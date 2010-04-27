<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Dogbert.Controllers.ViewModels.UseCaseViewModel>" %>
<%@ Import Namespace="xVal.Html"%>
<%@ Import Namespace="Dogbert.Core.Domain"%>
<%@ Import Namespace="Dogbert.Helpers"%>


     <%= Html.ValidationSummary() %>
          <fieldset>
            <legend>Use Case Requirements</legend>
            <% using (Html.BeginForm())
               { %>
                    <%= Html.AntiForgeryToken() %>
             
                    <%= Model.Project!=null ? Html.HiddenFor(p=>p.Project.Id) : ""%>
              <p>
                 <%= this.MultiSelect("UseCase.Requirements")
                    .Options(Model.Project.Requirements, x => x.Id,  x => x.Description)
                    .Selected(Model.UseCase != null && Model.UseCase.Requirements  != null ? Model.UseCase.Requirements : new List<Requirement>()) 
                    .FirstOption("--Related Requirements--")
                    .HideFirstOptionWhen(Model.Project.Requirements != null)
                    .Label("Re-Select Related Requirements:")%>
              </p>
               
               
            <%--  <p>
                    <%= this.CheckBoxList("UseCase.Requirements")
                    .Options(new MultiSelectList(Model.Project.Requirements, "Id", "Description", Model.UseCase.Requirements.Select(x => x.Id)))
                    .Id("UseCase.Requirements").Label("Related Requirements:")
                    .Selected(Model.UseCase != null && Model.UseCase.Requirements != null ? Model.UseCase.Requirements : new List<Requirement>())
                     %>
               </p>--%>
               
                <p>
                    <input type="submit" value="Save"/>
                </p>
                
        
                
      
            <%} %>
                      
        </fieldset>



    
   