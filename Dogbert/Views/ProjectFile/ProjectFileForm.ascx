<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ProjectFileViewModel>" %>
<%@ Import Namespace="Dogbert.Core.Resources"%>
<%@ Import Namespace="Dogbert.Controllers.Helpers"%>
<%@ Import Namespace="Dogbert.Controllers.ViewModels"%>
<%@ Import Namespace="Dogbert.Core.Domain"%>
<%@ Import Namespace="xVal.Html"%>

    <%= Html.AntiForgeryToken() %>
    <%= Html.Hidden("projectId", Model.Project.Id) %>
    <fieldset>
        <legend>Fields</legend>

        <p>
            <%= this.Select("ProjectFile.Type").Options(Model.FileTypes, x => x.Id, x => x.Name)
                    .FirstOption("--Select a File Type--")
                    .HideFirstOptionWhen(Model.ProjectFile.Type != null)                
                    .Label("File Type:")
                    .Selected(Model.ProjectFile.Type != null ? Model.ProjectFile.Type.Id.ToString() : string.Empty )
            %>
            <%= Html.ValidationMessage("ProjectFile.Type", "*")%>
        </p>

        <p>
            <label for="file">Project File:</label>
            <input type="file" name="file" id="file" />
        </p>
       
    </fieldset>