<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Dogbert.Controllers.ViewModels.UseCaseViewModel>" %>
<%@ Import Namespace="xVal.Html"%>
<%@ Import Namespace="Dogbert.Core.Domain"%>
<%@ Import Namespace="Dogbert.Helpers"%>

<%--<%= Html.ClientSideValidation<Project>("Project") %>--%>
     <%= Html.ValidationSummary() %>

          <fieldset>
            <legend>Use Case Steps</legend>
            <% using (Html.BeginForm())
               { %>
                    <%= Html.AntiForgeryToken() %>
               
   
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



    
   