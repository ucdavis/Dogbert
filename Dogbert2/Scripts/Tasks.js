$(function () {

    // set the requirement template
    $.template("requirement-template", $("#requirement-template").html());
    $.template("selected-template", $("#selected-template").html());

    // select a new category
    $("#Task_RequirementCategory").change(function () {

        var categoryId = $(this).val();

        if (categoryId != "") {
            $.getJSON(url,
                        { projectId: projectId, categoryId: categoryId },
                        function (result) {

                            $("#requirement-container").empty();

                            $.each(result, function (index, item) {
                                var values = [{ Id: item.Id, RequirementId: item.RequirementId, Description: item.Description}];
                                $.tmpl("requirement-template", values).appendTo("#requirement-container");
                            });

                        }
                    );
        }
    });

    $(".requirement").live('click', function () {

        // removing from selected
        if ($(this).hasClass("selected")) {
            // delete the hidden control
            $(this).find("input").remove();
        }
        // add to selected
        else {

            var values = [{ Id: $(this).data("id")}];
            var tmpl = $.tmpl("selected-template", values)
            $(this).append(tmpl);

        }

        // toggle the class
        $(this).toggleClass("selected");
    });
});