using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DBRouCity;

namespace RCITYWEB.Models
{
    public class ViajeVM
    {
       
        public int IdViaje { get; set; }
        public int IdUChofer { get; set; }
        public decimal latOrigen { get; set; }
        public decimal longOrigen { get; set; }
        public decimal latDestino { get; set; }
        public decimal longDestino { get; set; }
        public System.DateTime fecha { get; set; }
        public decimal precio { get; set; }
        public System.TimeSpan hora { get; set; }
        public string cocheDescripcion { get; set; }
        public int asientosDisponibles { get; set; }
        public Nullable<short> calificacion { get; set; }
        public string comentario { get; set; }
        public Nullable<short> cantParada { get; set; }
        public Nullable<decimal> tiempoParada { get; set; }

        public virtual Usuario Usuario { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ViajePasajero> viajePasajero { get; set; }
    }
}