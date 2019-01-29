$(document).ready(function () {
    $('#loadingDiv')
        .hide()
        .ajaxStart(function () {
            $(this).show();
        })
        .ajaxStop(function () {
            $(this).hide();
        });
})