function InitializeSteps() {

    // compile the template
    $.template("step-template", $("#step-template table tbody").html());

    // set up the dialog
    $("#step-dialog").dialog({
        autoOpen: false,
        modal: true,
        width: 400,
        buttons: {
            "Save": addStep,
            "Cancel": function () { $(this).dialog("close"); }
        }
    });

    // bring up dialog to create a new step
    $("#add-step").click(function () {

        // blank the controls
        $("#step-description").val("");
        $("#step-optional").attr("checked", false);
        $("#step-index").val(-1);

        // open the dialog
        $("#step-dialog").dialog("open");

    });

    $(".edit-step").live("click", function () {

        // get a reference to the current row
        var $row = $(this).parents("tr");

        // read in the values
        var description = $row.find(".description-hdn");
        var optional = $row.find(".optional-hdn");

        // open the dialog
        $("#step-dialog").dialog("open");

        // set the values
        $("#step-description").val(description.val());
        $("#step-optional").attr("checked", optional.val() == 'True');
        $("#step-index").val($row.data("id"));

    });

    $(".delete-step").live("click", function () {

        // delete the row
        $(this).parents("tr").remove();

        // reinitialize the table dragging
        initializeTableDragging();

        // update the step numbers
        reorderSteps();

    });

    // initialize the table dragging for the first time
    initializeTableDragging();

}

function addStep() {

    // read the values out of the controls
    var $tbody = $("#steps tbody");

    // deal with the current indexes
    var existingIndex = parseInt($("#step-index").val());
    var index, number;
    if (existingIndex >= 0) {
        index = existingIndex;
        number = existingIndex + 1;        
    }
    else {
        index = $tbody.children().length;
        number = $tbody.children().length + 1;
    }
    
    // read out the values
    var description = $("#step-description").val();
    var optional = ($("#step-optional").attr("checked") ? "x" : "");
    var optionalValue = $("#step-optional").is(':checked');

    // create a new row from the template
    var values = [{ Index: index, Number: number, Description: description, Optional: optional, OptionalValue: optionalValue}];
    var newRow = $.tmpl("step-template", values);

    // update an existing step
    if (existingIndex >= 0) {

        // get the existing row
        var $row = $tbody.find("tr").filter(function () {
            var indexId = $("#step-index").val();

            return $(this).data("id") == indexId;
        });

        // set the information
        newRow.data("id", existingIndex);
        newRow.find(".id-hdn").val($row.find(".id-hdn").val());
        $row.replaceWith(newRow);

    }
    else {
        $tbody.append(newRow);
    }

    initializeTableDragging();

    // close the dialog
    $(this).dialog("close");
}

function initializeTableDragging() {
    $("#steps").tableDnD({
        onDragClass: "dragging",
        onDrop: function (table, row) {
            // re-adjust the odd, even row classes
            $("tbody tr").removeClass("odd");
            $("tbody tr").removeClass("even");

            $("tbody tr:even").addClass("even");
            $("tbody tr:odd").addClass("odd");

            reorderSteps();
        }
    });
}

function reorderSteps() {

    // get all the rows
    var rows = $("#steps tbody tr");

    $.each(rows, function (index, item) {

        // get the controls
        var orderCtl = $(item).find(".usecase-order");
        var orderHdn = $(item).find(".order-hdn");
        var descriptionHdn = $(item).find(".description-hdn");
        var optionalHdn = $(item).find(".optional-hdn");
        var idHdn = $(item).find(".id-hdn");

        // update all the values now
        $(item).data("id", index);

        idHdn.attr("name", "UseCase.UseCaseSteps[" + index + "].Id");
        idHdn.attr("id", "UseCase_UseCaseSteps[" + index + "]_Id");

        orderCtl.html(index + 1);
        orderHdn.attr("name", "UseCase.UseCaseSteps[" + index + "].Order");
        orderHdn.attr("id", "UseCase_UseCaseSteps[" + index + "]_Order");
        orderHdn.val(index + 1);
        orderCtl.append(orderHdn);

        descriptionHdn.attr("name", "UseCase.UseCaseSteps[" + index + "].Description");
        descriptionHdn.attr("id", "UseCase_UseCaseSteps[" + index + "]_Description");

        optionalHdn.attr("name", "UseCase.UseCaseSteps[" + index + "].Optional");
        optionalHdn.attr("id", "UseCase_UseCaseSteps[" + index + "]_Optional");

    });

}
