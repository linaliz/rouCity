//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DBRouCity
{
    using System;
    using System.Collections.Generic;
    
    public partial class Valoracion
    {
        public int idvaloracion { get; set; }
        public int idusuario { get; set; }
        public int idviaje { get; set; }
        public int valoracionEstrellas { get; set; }
        public string reseña { get; set; }
    
        public virtual Viaje Viaje { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
