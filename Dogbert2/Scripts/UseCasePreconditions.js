function InitializePrecondition() {

    // set the template for the row
    $.template("precondition-template", $("#precondition-template table tbody").html());

    // initialize the dialog
    $("#precondition-dialog").dialog({
        autoOpen: false,
        modal: true,
        width: 400,
        buttons: {
            "Save": addPrecondition,
            "Cancel": function () { $(this).dialog("close"); }
        }
    });

    // click action for adding preconditions
    $("#add-precondition").click(function () {
        // blank the control
        $("#precondition-description").val("");
        $("#precondition-index").val(-1);

        // open the dialog
        $("#precondition-dialog").dialog("open");
    });

    // delete of a precondition
    $(".delete-precondition").live("click", function(){
        $(this).parents("tr").remove();
    });

    // edit of a precondition
    $(".edit-precondition").live("click", function(){
        var $row = $(this).parents("tr");

        var description = $row.find(".description-hdn");

        $("#precondition-dialog").dialog("open");

        $("#precondition-description").val(description.val());
        $("#precondition-index").val($row.data("id"));
    });
}

// event for saving of precondition
function addPrecondition() {

    var $tbody = $("#preconditions tbody");

    // determine if adding or updating
    var existingIndex = parseInt($("#precondition-index").val());
    var index;
    if (existingIndex >= 0)
    {
        index = existingIndex;
    }
    else
    {
        index = $tbody.children().length;
    }
        
    // get description
    var description = $("#precondition-description").val();
        
    // set the values for the template
    var values = [{Index: index, Description: description}];
    var newRow = $.tmpl("precondition-template", values);

    if (existingIndex >= 0)
    {
        // find the existing row
        var $row = $tbody.find("tr").filter(function(){
                                var indexId = $("#precondition-index").val(); 
                                return $(this).data("id") == indexId;
                            });

        newRow.find(".id-hdn").val($row.find(".id-hdn").val());
        $row.replaceWith(newRow);
    }
    else
    {
        $tbody.append(newRow);
    }

    // close the dialog
    $(this).dialog("close");
}