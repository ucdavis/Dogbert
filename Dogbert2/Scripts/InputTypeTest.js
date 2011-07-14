// Code was taken from http://nicolahibbert.com/html5-forms-support-detection-javascript/
// Adapted to add the hasSupport function

var uiSupport = [];

$(function () {
    var inputs = ['search', 'tel', 'url', 'email', 'datetime', 'date', 'month', 'week', 'time', 'datetime-local', 'number', 'color', 'range'],
    len = inputs.length;

    for (var i = 0; i < len; i++) {
        var input = document.createElement('input');
        input.setAttribute('type', inputs[i]);
        var notText = input.type !== 'text';

        if (notText && input.type !== 'search' && input.type !== 'tel') {
            input.value = 'testing';
            if (input.value !== 'testing') {
                uiSupport.push(input.type);
                //console.log(uiSupport);
            }
        }
    }
});

// checks if a specific feature is in the database
function hasSupport(name) {
    for (i = 0; i < uiSupport.length; i++) {

        //alert(uiSupport[i] + "|" + name);

        if (uiSupport[i] == name) {
            return true;
        }
    }

    return false;
}