using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RCITYWEB.Models
{
    public class Respuesta
    {
        public Respuesta()
        {
            Ok = true;
            Errores = new List<string>();
        }
        public bool Ok { get; set; }

        public List<String> Errores { get; set; }
        public CodigoDescripcion<String> Mensaje { get; set; }
    }
}