$(function () {

    if (!hasSupport("datetime")) {
        $('input[type="datetime"]').datepicker();
    }

    if (!hasSupport("range")) {
        $.each($('input[type="range"]'), function (index, item) {

            var id = item.id;
            var value = $(item).val();

            // create the div we need
            var hidden = $("<input>").attr("type", "hidden").attr("id", id).attr("name", id.replace("_", "."));
            var slider = $("<div>").attr("id", "slider_" + id);

            // insert after the existing item
            hidden.insertAfter($(item));
            slider.insertAfter($(item));

            $("#slider_" + id).slider({
                min: 0, max: 10,    // hard coded for now
                value: value,
                slide: function (event, ui) {
                    $("#" + id).val(ui.value);
                }
            });

            // drop the item
            $(item).remove();
        });
    }

});