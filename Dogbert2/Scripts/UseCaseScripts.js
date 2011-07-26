$(function () {

    // compile the template
    $.template("rowTemplate", $("#row-template table tbody").html());

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

    // make the add step a button
    $("#add-step").button();

    // bring up dialog to create a new step
    $("#add-step").click(function () {

        // blank the controls
        $("#step-description").val("");
        $("#step-optional").attr("checked", false);

        // open the dialog
        $("#step-dialog").dialog("open");

    });

    $(".edit-usecase").live("click", function () { });
    $(".delete-usecase").live("click", function () {

        // delete the row
        $(this).parents("tr").remove();

        initializeTableDragging();

        reorderSteps();

    });

    initializeTableDragging();

});

function addStep() {

    var tbody = $("#steps tbody");

    var index = tbody.children().length;
    var number = tbody.children().length + 1;
    var description = $("#step-description").val();
    var optional = ($("#step-optional").attr("checked") ? "x" : "");
    var optionalValue = $("#step-optional").is(':checked'); 

    var values = [{ Index: index,  Number: number, Description: description, Optional: optional, OptionalValue: optionalValue }];
    $.tmpl("rowTemplate", values).appendTo("#steps tbody");

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

        // update all the values now
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
