$(document).ready(function (e) {
    $("#registro").on('click', function () {
        console.log(this);
        $.ajax({
            type: "GET",
            url: "/Login/Registro",
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (response) {
                $('#resultado-modal').html(response);
                $('#fn-modal').modal('show');
            },
            error: function (response) {
                console.log(response.responseText);
            }
        });
    });
});