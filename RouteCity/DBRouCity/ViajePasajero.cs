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
    
    public partial class ViajePasajero
    {
        public int idViaje { get; set; }
        public int idPasajero { get; set; }
        public bool confirmado { get; set; }
    
        public virtual Viaje Viaje { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
