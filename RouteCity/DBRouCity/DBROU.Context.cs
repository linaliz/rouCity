﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class RouCityEntities : DbContext
    {
        public RouCityEntities()
            : base("name=RouCityEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<Accion> Accion { get; set; }
        public virtual DbSet<Modulo> Modulo { get; set; }
        public virtual DbSet<Pago> Pago { get; set; }
        public virtual DbSet<RolAccion> RolAccion { get; set; }
        public virtual DbSet<Valoracion> Valoracion { get; set; }
        public virtual DbSet<Viaje> Viaje { get; set; }
        public virtual DbSet<ViajePasajero> ViajePasajero { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
    }
}