<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Dogbert.Controllers.ViewModels.UseCaseViewModel>" %>
<%@ Import Namespace="xVal.Html"%>
<%@ Import Namespace="Dogbert.Core.Domain"%>
<%@ Import Namespace="Dogbert.Helpers"%>



 <script type="text/javascript">
     $().ready(function() {
        $("#add").click(function() {
            return !$("#select1 option:selected").remove().appendTo("#select2");
         });
         $("#remove").click(function() {
            return !$("#select2 option:selected").remove().appendTo("#select1");
        });
        $("form").submit(function() {
            $('#select2 option').each(function(i) {
                $(this).attr("selected", "selected");
            });
        });
     });
 </script>
 
  <style type="text/css">
          a.form {
           display: block;
           border: 1px solid #aaa;
           text-decoration: none;
           background-color: #fafafa;
           color: #123456;
           margin: 2px;
           clear:both;
          }
         
        
          select {
           width: 400px;
           height: 220px;
          }
 </style>


     <%= Html.ValidationSummary() %>
          <fieldset class="rrForm">
            <legend>Use Case Requirements</legend>
            <% using (Html.BeginForm())
               { %>
                    <%= Html.AntiForgeryToken() %>
             
                    <%= Model.Project!=null ? Html.HiddenFor(p=>p.Project.Id) : ""%>
  <table>
        <tr>
            <td>
                  <div>
                        Select All Related Requirements: (use cntl to select multiple)
                  </div>
            </td>
        </tr>
        <tr>  
            <td width="80">
                  <div>
                     <%= this.MultiSelect("Project.Requirements").Id("select1")
                        .Options(Model.Project.Requirements
                                     .Where(a => !Model.UseCase.Requirements.Contains(a))
                                             , x => x.Id, x => x.ShortDescription)
                        .Selected(Model.UseCase != null && Model.UseCase.Requirements != null ? Model.UseCase.Requirements : new List<Requirement>()) 
                        .FirstOption("--Related Requirements--")
                        .HideFirstOptionWhen(Model.Project.Requirements != null)
                        %>
                  </div>
            </td>
            <td>
                   <a class= "form" href="#" id="add">add >></a>
                   <a class= "form" href="#" id="remove"><< remove</a>
            </td>
            <td>
                   <div>
                     <%= this.MultiSelect("UseCase.Requirements").Id("select2")
                          .Options(Model.RelatedRequirements)
                    
                     %>
                  </div>
            </td>
        </tr>
               
      </table>         
            <%--  <p>
                    <%= this.CheckBoxList("UseCase.Requirements")
                    .Options(new MultiSelectList(Model.Project.Requirements, "Id", "Description", Model.UseCase.Requirements.Select(x => x.Id)))
                    .Id("UseCase.Requirements").Label("Related Requirements:")
                    .Selected(Model.UseCase != null && Model.UseCase.Requirements != null ? Model.UseCase.Requirements : new List<Requirement>())
                     %>
               </p>--%>
               
                <div>
                    <input type="submit" value= "save"/>
                </div>
                
        
                
      
            <%} %>
                      
        </fieldset>
        



    
   