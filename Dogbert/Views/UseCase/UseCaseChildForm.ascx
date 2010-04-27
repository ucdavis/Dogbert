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
             
   
               <p>
                 <%= this.MultiSelect("UseCase.UseCases")
                    .Options(Model.UseCases , x=>x.Id, x=>x.Name)
                    .Selected(Model.UseCase != null && Model.UseCase.Children != null ? Model.UseCase.Children : new List<UseCase>()) 
                    .FirstOption("--Related Use Cases--")
                    .HideFirstOptionWhen(Model.UseCases != null)
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



    
   