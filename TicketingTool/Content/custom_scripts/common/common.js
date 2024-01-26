$(document).ready(function () {
    $('.numberonly').keypress(function (e) {
        var charCode = (e.which) ? e.which : event.keyCode
        if (String.fromCharCode(charCode).match(/[^0-9]/g))
            return false;
    });

});

$(document).ajaxSend(function (event, xhr, options) {
    $('.spinner').css('display', 'block');
}).ajaxComplete(function (event, xhr, options) {
    $('.spinner').css('display', 'none');
}).ajaxError(function (event, jqxhr, settings, exception) {
    $('.spinner').css('display', 'none');
});

$(window).on('load', function () {
    $('.spinner').css('display', 'none');
});