<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Dogbert.Controllers.ViewModels.UseCaseViewModel>" %>
<%@ Import Namespace="xVal.Html"%>
<%@ Import Namespace="Dogbert.Core.Domain"%>
<%@ Import Namespace="Dogbert.Helpers"%>


     <%= Html.ValidationSummary() %>
          <fieldset>
            <legend>Use Case Steps</legend>
            <% using (Html.BeginForm())
               { %>
                    <%= Html.AntiForgeryToken() %>
             
                    <%= Model.Project!=null ? Html.HiddenFor(p=>p.Project.Id) : ""%>
               <p>
                 <%= this.MultiSelect("Project.UseCases")
                    .Options(Model.Project.UseCases, x=>x.Id, x=>x.Name)
                    .Selected(Model.Project.UseCases != null && Model.UseCase.Children != null ? Model.UseCase.Children : new List<UseCase>()) 
                    .FirstOption("--Related Use Cases--")
                    .HideFirstOptionWhen(Model.Project.UseCases != null)
                    .Label("Re-Select Related Use Cases:")%>
               </p>
               
               
             <%--  <p>
                    <%= this.CheckBoxList("UseCase.UseCases")
                    .Options(new MultiSelectList(Model.UseCases, "Id", "Name", Model.UseCase.Children.Select(x => x.Id)))
                    .Id("UseCase.Children").Label("Related UseCases:")
                    .Selected(Model.UseCase != null && Model.UseCase.Children != null ? Model.UseCase.Children : new List<UseCase>())
                    
                     %>
               </p>--%>
               
                <p>
                    <input type="submit" value="Save"/>
                </p>
                
        
                
      
            <%} %>
                      
        </fieldset>



    
   