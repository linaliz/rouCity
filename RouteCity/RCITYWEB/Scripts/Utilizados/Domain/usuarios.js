
Usuario.Save = function () {

    messageBox("Guardar", "¿Está seguro de querer guardar los cambios?", "Si", "No", function () {

        Util.LimpiarErrores();
        var form = $('#formUsuario');
        var url = "/Usuarios/Save";
        var index = "Usuarios/Index";

        Util.EnviarFormulario(form, url, index);

    });
}