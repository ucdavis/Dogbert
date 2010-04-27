<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Dogbert.Controllers.ViewModels.ProjectViewModel>" %>
<%@ Register Assembly="System.Web.DynamicData, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.DynamicData" TagPrefix="cc1" %>
<%@ Import Namespace="xVal.Html"%>
<%@ Import Namespace="Dogbert.Core.Domain"%>


<%= Html.ClientSideValidation<Project>("Project") %>
    <%= Html.ValidationSummary() %>
    

    <% using (Html.BeginForm()) {%>
        <%= Html.AntiForgeryToken() %>
        <%= Model.Project!=null ? Html.HiddenFor(p=>p.Project.Id) : ""%>
 
     

        <fieldset>
            <legend>Fields</legend>
            <p>
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
           
                <%= Html.TextBox("Project.Contact")%>
                    <%--.Label("Contact:")--%>
           </p>
           <p>
                <%= Html.TextBox("Project.ContactEmail")%>
                            <%--.Label("ContactEmail:")--%>
           </p>
            <p>
                <%= Html.TextBox("Project.Unit")%>
                    <%--.Label("Unit:")--%>
            </p>
            <p>
                <%= Html.TextBox("Project.Complexity")%>
                <%--.Label("Complexity:")--%>
            </p>
            <p>
                
               <%= Html.TextBox("Project.ProjectedStart")%>
                    <%--.Format("{0:d}").Label("Start Date:")                   --%>
                   
            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" 
                runat="server" ErrorMessage="RequiredFieldValidator" 
                ControlToValidate="Project_ProjectedStart"></asp:RequiredFieldValidator>--%>
                
            </p>
            <p>
                <%= Html.TextBox("Project.ProjectedEnd")%>
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

   