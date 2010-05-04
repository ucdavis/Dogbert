<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ProjectFileViewModel>" %>
<%@ Import Namespace="Dogbert.Core.Resources"%>
<%@ Import Namespace="Dogbert.Controllers.Helpers"%>
<%@ Import Namespace="Dogbert.Controllers.ViewModels"%>
<%@ Import Namespace="Dogbert.Core.Domain"%>
<%@ Import Namespace="xVal.Html"%>

        <p>
            <%= this.Select("ProjectFile.Type").Options(Model.FileTypes, x => x.Id, x => x.Name)
                    .FirstOption("--Select a File Type--")
                    .HideFirstOptionWhen(Model.ProjectFile.Type != null && Model.ProjectFile.Type.IsActive)                
                    .Label("File Type:")
                    .Selected(Model.ProjectFile.Type != null ? Model.ProjectFile.Type.Id.ToString() : string.Empty )
            %>
            <%= Html.ValidationMessage("ProjectFile.Type", "*")%>
        </p>    
        <p> 
           <%= this.Select("ProjectFile.TextType").Options(Model.TextTypes, x => x.Id, x => x.Name)
            .FirstOption("--Select a Text Type--")
            .HideFirstOptionWhen(Model.ProjectFile.TextType != null && Model.ProjectFile.TextType.IsActive)                
            .Label("File Text Type:")
            .Selected(Model.ProjectFile.TextType != null ? Model.ProjectFile.TextType.Id.ToString() : string.Empty )
            %>
            <%= Html.ValidationMessage("ProjectFile.TextType", "*")%>
         </p> 
         <p>
            <label for="ProjectFile.Caption">Caption:</label>
            <%= Html.TextBox("ProjectFile.Caption")%>
            <%= Html.ValidationMessage("ProjectFile.Caption")%>
         </p>
      
       
    