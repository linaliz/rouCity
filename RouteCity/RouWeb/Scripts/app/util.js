Util = {};
Util.url = "";

Util.Dominio = function () {
    return "";
}

Util.EnviarFormulario = function (form, url, index, callback) {
    var formData = Util.ObtenerFormData(form);
    $.ajax(
        {
            type: "POST",
            url: url,
            data: JSON.stringify(formData),
            dataType: "json",
            cache: false,
            contentType: "application/json",
            processData: false
        }
    ).done(function (resp) {
        if (resp.Ok == true) {
            messageBox(resp.Mensaje.Key, resp.Mensaje.Value, "Aceptar", "", function () {
                window.location = baseUrl + index
            });
        } else {
            Util.MostrarErrores(resp.Errores)
        }
        callback(resp);

    });
}

Util.Enviar = function (data, url, bPost, callback) {

    $.ajax(
        {
            type: bPost ? "POST" : "GET",
            url: url,
            data: JSON.stringify(data),
            dataType: "json",
            cache: false,
            contentType: "application/json",
            processData: false
        }
    ).done(function (data) {
        callback(data);

    });

}

Util.ObtenerFormData = function (form) {
    var objetoDatos = {};
    var inputs = $(form).find('input, textarea, select');
    var top = inputs.length;

    for (var index = 0; index < top; index++) {
        var input = inputs[index];

        var tieneNombre = !!input.name;
        var esRadio = $(input).is('[type=radio]');
        var esCheck = $(input).is('[type=checkbox]');


        if (objetoDatos[input.name] == undefined) {
            if (tieneNombre && !esRadio && !esCheck) {

                var valor = $(input).val();
                objetoDatos[input.name] = valor;
            } else if (tieneNombre && (esCheck || esRadio)) {
                objetoDatos[input.name] = $(input).is(':checked');
            }
        }
    }
    return objetoDatos;
};

Util.MostrarErrores = function (errores) {
    var x = 0;
    var top = errores.length;
    if (top > 0) {
        var html = "";
        for (x = 0; x < top; x++) {
            html += '<div class="alert alert-danger mt-0 mb-1" role="alert">' + errores[x] + '</div>';
        }


        $("#Errores").append(html);
    }

};

Util.MostrarError = function (errorString) {

    $("#Errores").append('<div class="alert alert-danger mt-0 mb-1" role="alert">' + errorString + '</div>');

};

Util.LimpiarErrores = function () {
    $("#Errores").html("");
};

Util.FormatFechaYYYYMMDD = function (milliseconds = false) {
    //Si el parámetro está vacío cogemos la fecha de hoy
    if (!milliseconds) {

        var f = new Date();
        var fecha = f.getTime();
    }
    else {
        var fecha = new Date(milliseconds);
    }

    let ye = new Intl.DateTimeFormat('en', { year: 'numeric' }).format(fecha);
    let mo = new Intl.DateTimeFormat('en', { month: '2-digit' }).format(fecha);
    let da = new Intl.DateTimeFormat('en', { day: '2-digit' }).format(fecha);

    fechaFormateada = (`${ye}-${mo}-${da}`);
    return fechaFormateada;
};

Util.FormatFechaDDMMYYYY = function (milliseconds = false) {
    //Si el parámetro está vacío cogemos la fecha de hoy
    if (!milliseconds) {

        var f = new Date();
        var fecha = f.getTime();
    }
    else {
        var fecha = new Date(milliseconds);
    }

    let ye = new Intl.DateTimeFormat('en', { year: 'numeric' }).format(fecha);
    let mo = new Intl.DateTimeFormat('en', { month: '2-digit' }).format(fecha);
    let da = new Intl.DateTimeFormat('en', { day: '2-digit' }).format(fecha);

    fechaFormateada = (`${da}/${mo}/${ye}`);
    return fechaFormateada;
};

Util.FormatTimeHHMM = function (time = false) {
    //Si el parámetro está vacío cogemos la hora actual
    if (!time) {
        var hora = new Date();
    }
    else {

        var hora = new Date(1970, 0, 1, time.Hours, time.Minutes);

    }

    horaFormateada = hora.toTimeString().split(' ')[0]

    return horaFormateada;
};

Util.QuitarCerosIdentificadorProveedor = function (identificadorString) {
    //Esta variable esta echa solo para que se este una vez en el if aunque los siguientes sean ceros
    // Este metodo cuando encuntra un caracter que es distinto de cero coge lo demas y crea un identificador con ello
    let noEntrarMasEnEsteIf = false; //0109
    let nuevoIdentificador = "";

    for (let i = 0; i < identificadorString.length; i++) {

        if (identificadorString[i] != "0" && noEntrarMasEnEsteIf == false) {
            noEntrarMasEnEsteIf = true;

            for (let y = i; y < identificadorString.length; y++) {
                nuevoIdentificador = nuevoIdentificador + identificadorString[y];
            }

        }

    }

    if (nuevoIdentificador.length == 0) {
        nuevoIdentificador = "0";
    }

    return nuevoIdentificador;
};

Util.activarBotonGuardarCuandoEstoCambie = function () {

    //Los que tengan la clase 'activarBotonGuardarCuandoEstoCambie' cambien su valor se activara el boton guardar
    /*https://stackoverflow.com/questions/25845126/jquery-change-event-alternative*/
    $(".activarBotonGuardarCuandoEstoCambie").one('input', function () {
        $('button').prop("disabled", false);
    });

}