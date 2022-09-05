using System;

namespace RCITYWEB.ViewModels
{
    public class UsuarioViewModel
    {
        public int idUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public byte[] Foto { get; set; }
        public string Email { get; set; }
        public string Pass { get; set; }
        public Nullable<System.DateTime> FechaCreado { get; set; }
        public string Sexo { get; set; }
        public Nullable<System.DateTime> FechaNac { get; set; }
        public string Telefono { get; set; }
        public string Descripción { get; set; }
        public string Preferencia { get; set; }
        public string NumeroCuenta { get; set; }
        public int idRol { get; set; }
        public bool Confirmado { get; set; }
        public bool baja { get; set; }
        public string ConfirmarPass { get; set; }
        public string hash { get; set; }
    }
}