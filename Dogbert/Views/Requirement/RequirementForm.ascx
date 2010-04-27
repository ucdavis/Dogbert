<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<RequirementViewModel>" %>
<%@ Import Namespace="Dogbert.Core.Resources"%>
<%@ Import Namespace="Dogbert.Controllers.Helpers"%>
<%@ Import Namespace="Dogbert.Controllers.ViewModels"%>
<%@ Import Namespace="Dogbert.Core.Domain"%>
<%@ Import Namespace="xVal.Html"%>

    <%= Html.ClientSideValidation<RequirementViewModel>()%>

    <% using (Html.BeginForm()) {%>
    <%= Html.AntiForgeryToken() %>
        <fieldset>
            <legend>Fields</legend>
            <p>
                <label for="Description">Description:</label>
                <%= this.TextBox("Requirement.Description")%>
                <%= Html.ValidationMessage("Requirement.Description", "*")%> 
            </p>
            <p>
                <label for="TechnicalDifficulty">Technical Difficulty:</label>
                <%= this.TextBox("Requirement.TechnicalDifficulty")%>
                <%= Html.ValidationMessage("Requirement.TechnicalDifficulty", "*")%>
            </p>
            <p>
                <%= this.Select("Requirement.RequirementType").Options(Model.RequirementTypes, x => x.Id, x => x.Name)
                        .FirstOption("--Select a Requirement Type--")
                        .HideFirstOptionWhen(Model.Requirement.RequirementType != null)                
                        .Label("Requirement Type:")
                        .Selected(Model.Requirement.RequirementType != null ? Model.Requirement.RequirementType.Id : string.Empty )
                %>
                <%= Html.ValidationMessage("Requirement.RequirementType", "*")%>
            </p>
            <p>
                <%= this.Select("Requirement.PriorityType").Options(Model.PriorityTypes, x => x.Id, x => x.Name)
                        .FirstOption("--Select a Priority Type--")
                        .HideFirstOptionWhen(Model.Requirement.PriorityType != null)
                        .Label("Priority Type:")
                        .Selected(Model.Requirement.PriorityType != null ? Model.Requirement.PriorityType.Id.ToString() : string.Empty)
                %>
                <%= Html.ValidationMessage("Requirement.PriorityType", "*")%>                
            </p>
            <p>
                <%= this.Select("Requirement.Category").Options(Model.Categories, x => x.Id, x => x.Name)
                        .FirstOption("--Select a Category--")
                        .HideFirstOptionWhen(Model.Requirement.Category != null)
                        .Label("Category Type:")
                        .Selected(Model.Requirement.Category != null ? Model.Requirement.Category.Id.ToString(): string.Empty)
                                                                    
                %>
                <%= Html.ValidationMessage("Requirement.Category", "*")%>                               
            </p>
            <p>
                <label for="IsComplete">IsComplete:</label>
                <%= this.CheckBox("Requirement.IsComplete")%>
                <%= Html.ValidationMessage("Requirement.IsComplete", "*")%>
            </p>
            <p>
                <input type="submit" value="Save" />
            </p>
        </fieldset>

    <% } %>
