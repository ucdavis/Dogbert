function InitializePostcondition() {

    // set the template for the row
    $.template("postcondition-template", $("#postcondition-template table tbody").html());

    // initialize the dialog
    $("#postcondition-dialog").dialog({
        autoOpen: false,
        modal: true,
        width: 400,
        buttons: {
            "Save": addPostcondition,
            "Cancel": function () { $(this).dialog("close"); }
        }
    });

    // click action for adding postconditions
    $("#add-postcondition").click(function () {
        // blank the control
        $("#postcondition-description").val("");
        $("#postcondition-index").val(-1);

        // open the dialog
        $("#postcondition-dialog").dialog("open");
    });

    // delete of a postcondition
    $(".delete-postcondition").live("click", function () {
        $(this).parents("tr").remove();
    });

    // edit of a postcondition
    $(".edit-postcondition").live("click", function () {
        var $row = $(this).parents("tr");

        var description = $row.find(".description-hdn");

        $("#postcondition-dialog").dialog("open");

        $("#postcondition-description").val(description.val());
        $("#postcondition-index").val($row.data("id"));
    });
}

// event for saving of postcondition
function addPostcondition() {

    var $tbody = $("#postconditions tbody");

    // determine if adding or updating
    var existingIndex = parseInt($("#postcondition-index").val());
    var index;
    if (existingIndex >= 0) {
        index = existingIndex;
    }
    else {
        index = $tbody.children().length;
    }

    // get description
    var description = $("#postcondition-description").val();

    // set the values for the template
    var values = [{ Index: index, Description: description}];
    var newRow = $.tmpl("postcondition-template", values);

    if (existingIndex >= 0) {
        // find the existing row
        var $row = $tbody.find("tr").filter(function () {
            var indexId = $("#postcondition-index").val();
            return $(this).data("id") == indexId;
        });

        newRow.find(".id-hdn").val($row.find(".id-hdn").val());
        $row.replaceWith(newRow);
    }
    else {
        $tbody.append(newRow);
    }

    // close the dialog
    $(this).dialog("close");
}