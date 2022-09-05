
function pagar() {


    var body = {
        precio: 70,
        producto: "Microfono Hyperx Quadcast"
    }



    jQuery.ajax({
        url: '@Url.Action("Paypal", "PagosController")',
        type: "POST",
        data: JSON.stringify(body),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {

            console.log(data);
            $("div.jumbotron").LoadingOverlay("hide");

            if (data.status) {

                var jsonresult = JSON.parse(data.respuesta);

                console.log(jsonresult);

                var links = jsonresult.links;

                var resultado = links.find(item => item.rel === "approve")

                window.location.href = resultado.href

                /*console.log(links)*/
                /*console.log(resultado)*/
            } else {
                alert("Vuelva a intentarlo más tarde")
            }


        },
        beforeSend: function () {
            $("div.jumbotron").LoadingOverlay("show");
        }
    });


}