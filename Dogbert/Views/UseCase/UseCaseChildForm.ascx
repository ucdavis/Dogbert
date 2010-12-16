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
          <fieldset>
            <legend>Use Case Steps</legend>
            <% using (Html.BeginForm())
               { %>
                    <%= Html.AntiForgeryToken() %>
                    <%= Model.Project!=null ? Html.HiddenFor(p=>p.Project.Id) : ""%>
               <table>
                    <tr>
                        <td>
                              Select All Related Use Cases: (use cntl to select multiple)
                        </td>
                    </tr>
                    <tr>  
                        <td width="80">
                                  <div>
                                     <%= this.MultiSelect("Project.UseCases").Id("select1")
                                                                     .Options(Model.Project.UseCases
                                                                            .Where(x => x.Id != Model.UseCase.Id && !Model.UseCase.Children.Contains(x))
                                                                                              , x => x.Id, x => x.Name)
                                        .Selected(Model.UseCase != null && Model.UseCase.Children  != null ? Model.UseCase.Children : new List<UseCase>()) 
                                        .FirstOption("--Related Use Cases--")
                                        .HideFirstOptionWhen(Model.Project.UseCases != null)
                                     %>
                                  </div>
                        </td>
                        <td>
                               <a class= "form" href="#" id="add">add >></a>
                               <a class= "form" href="#" id="remove"><< remove</a>
                        </td>
                        <td>
                               <div>
                                     <%= this.MultiSelect("UseCase.Children").Id("select2")
                                        .Options(Model.UseCase.Children.Where(x => x.Id != Model.UseCase.Id),x=>x.Id, x=>x.Name)
                                     %>
                              </div>
                        </td>
                    </tr>
                           
                  </table>       
               
                <p>
                    <input type="submit" value="Save"/>
                </p>
                
        
                
      
            <%} %>
                      
        </fieldset>



    
   