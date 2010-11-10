<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Dogbert.Controllers.ViewModels.ProjectViewModel>" %>
<%@ Register Assembly="System.Web.DynamicData, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.DynamicData" TagPrefix="cc1" %>
<%@ Import Namespace="xVal.Html"%>
<%@ Import Namespace="Dogbert.Core.Domain"%>

  <style type="text/css">
    #slider { margin: 0px; 
              margin-top: 0px;
              margin-left: 5px;
              width: 300px;
              display: inline-block; }
    #Project_Complexity {width: 30px; }
    
  
  </style>
  


<%= Html.ClientSideValidation<Project>("Project") %>
    <%= Html.ValidationSummary() %>
    

    <% using (Html.BeginForm()) {%>
        <%= Html.AntiForgeryToken() %>
        <%= Model.Project!=null ? Html.HiddenFor(p=>p.Project.Id) : ""%>
 

        <fieldset>
            <legend>Fields</legend>
            <p>
                <label for="Project.Name">Project Name:</label>
                <%= Html.TextBox("Project.Name")%>
            </p>
            <p>
             <%= this.Select("Project.ProjectType")
                .Options(Model.ProjectTypes, x=>x.Id, x=>x.Name)
                .Selected(Model.Project != null && Model.Project.ProjectType != null ? Model.Project.ProjectType.Id : "WS")
                .FirstOption("--Select Project Type--")
                .HideFirstOptionWhen(Model.Project != null)
                .Label("Project Type:")%>
           </p>
           
             <p>
             <%= this.Select("Project.StatusCode")
                .Options(Model.StatusCode, x=>x.Id, x=>x.Name)
                .Selected(Model.Project != null && Model.Project.StatusCode  != null ? Model.Project.StatusCode.Id : "PE")
                .FirstOption("--Select Status--")
                .HideFirstOptionWhen(Model.Project != null)
                .Label("Project Status:")%>
           </p>
           <p>    
                <label for="Project.Contact">Contact:</label>
                <%= Html.TextBox("Project.Contact")%>
                    <%--.Label("Contact:")--%>
           </p>
           <p>
                <label for="Project.ContactEmail">Contact Email:</label>
                <%= Html.TextBox("Project.ContactEmail")%>
                <%= Html.ValidationMessage("Project.ContactEmail", "*Email format is not Valid")%>

           </p>
            
            <p>
                <label for="Project.Unit">Unit:</label>
                <%= Html.TextBox("Project.Unit")%>
                    <%--.Label("Unit:")--%>
            </p>
           <div>
           
            <p > 
                <label for="Project.Complexity">Complexity:</label>
                <%= Html.TextBox("Project.Complexity")%>
                <span id="slider"></span>
            </p>
          </div>
            <p>
                <label for="Project.ProjectedStart">Projected Start:</label>
                <%if (Model.Project == null){%>
                     <%= Html.TextBox("Project.ProjectedStart")%>
                <%}
                else{%>
                     <%=Html.TextBox("Project.ProjectedStart", (Model.Project.ProjectedStart.HasValue ? Model.Project.ProjectedStart.Value.ToShortDateString() : string.Empty))%> 
                <%}%>

               <%= Html.ValidationMessage("Project.ProjectedStart", "*Invalid Date (format: mm/dd/yy)")%>
                    <%--.Format("{0:d}").Label("Start Date:")                   --%>
                
            </p>
            <p>
                <label for="Project.ProjectedEnd">Projected End:</label>
                <%if (Model.Project == null){%>
                     <%= Html.TextBox("Project.ProjectedEnd")%>
                <%}
                else{%>
                     <%=Html.TextBox("Project.ProjectedEnd", (Model.Project.ProjectedEnd.HasValue ? Model.Project.ProjectedEnd.Value.ToShortDateString() : string.Empty))%> 
                <%}%>
                
                <%= Html.ValidationMessage("Project.ProjectedEnd", "*Invalid Date (format: mm/dd/yy)")%>
                    <%--.Format("{0:d}").Label("End Date/Deadline:")--%>
            </p>
            
          
            <p>
                    <%-- <select id="Select1">
                         <option></option>
                     </select>--%>
             <%= this.Select("Project.ProjectManager")
                .Options(Model.Users, x=>x.Id, x=>x.FullName)
                .Selected(Model.Project != null && Model.Project.ProjectManager != null ? Model.Project.ProjectManager.Id : 0)
                .FirstOption("--Select Project Manager--")
                .HideFirstOptionWhen(Model.Project != null)
                .Label("Project Manager:")%>
           </p>
            <p>
             <%= this.Select("Project.LeadProgrammer")
                 .Options(Model.Users, x=>x.Id, x=>x.FullName)
                 .Selected(Model.Project != null && Model.Project.LeadProgrammer != null ? Model.Project.LeadProgrammer.Id : 0)
                 .FirstOption("--Select Lead Programmer--")
                 .HideFirstOptionWhen(Model.Project != null)
                 .Label("Lead Programmer:")%>
           </p>

            <p>
            </p>
           
            <p>
                <input type="submit" value="Save" />
            </p>
        </fieldset>

    <% } %>

   