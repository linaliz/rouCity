using DBRouCity;
using System;
using RCITYWEB.Models;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using RCITYWEB.ViewModels;

namespace RCITYWEB.Operations
{
    public class UsuarioOper
    {
        private RouCityEntities db = new RouCityEntities();
        public Usuario UserLogin(string user, string pass)
        {
            var dbUser = (from d in db.Usuario
                         where d.email == user.Trim()
                         && d.pass == pass.Trim()
                         select d).FirstOrDefault();

            return dbUser;
        }
        public List<UsuarioViewModel> GetListado(bool? filtroBaja)
        {

            var q =
               from u in this.db.Usuario
               orderby u.nombre, u.apellidos
               select new UsuarioViewModel
               {
                   idUsuario = u.idUsuario,
                   Nombre = u.nombre,
                   Apellidos = u.apellidos,
                   baja = (bool)u.baja

               };

            //Filtro
            if (filtroBaja == false)
            {
                q = q.Where(u => u.baja == false);
            }


            // Devolvemos datos
            return q.ToList();

        }
        public UsuarioViewModel GetUsuario(int id)
        {

            var q =
               from u in this.db.Usuario
               where u.idUsuario == id
               select new UsuarioViewModel
               {
                   idUsuario = u.idUsuario,
                   Nombre = u.nombre,
                   Apellidos= u.apellidos,
                   Pass = u.pass,
                   baja = (bool)u.baja

               };

            // Devolvemos datos
            return q.FirstOrDefault();

        }
        public int GetCountUsuario(string email)
        {

            var q =
               from u in this.db.Usuario
               where u.email.ToLower().Equals(email.ToLower())
               select new UsuarioViewModel
               {
                   idUsuario = u.idUsuario,
                   Email = u.email

               };

            // Devolvemos datos
            return q.Count();

        }
        public bool Guardar(Usuario usuario)
        {
            bool confirm =false;
            try
            {
                
                db.Usuario.Add(usuario);
                db.SaveChanges();
            }
            catch (Exception e) {
                Console.WriteLine("Error especificar datos correctos " + e.Message);    
            }
           
            return confirm;
        }
        public int Save(Usuario usuario)
        {
            int id = 0;

            try
            {
                if (usuario.idUsuario > 0)
                {
                    Usuario nuevoUsuario = db.Usuario.FirstOrDefault(u => u.idUsuario == usuario.idUsuario);

                    db.Entry(nuevoUsuario).CurrentValues.SetValues(usuario);
                }
                else
                {
                    db.Usuario.Add(usuario);
                    usuario.idrol = 1;
                }

                db.SaveChanges();
                id = usuario.idUsuario;
            }
            catch (Exception ex)
            {
                id = -1;
            }
            return id;
        }
        public int DarDeBaja(int id)
        {

            try
            {
                Usuario usuario = db.Usuario.FirstOrDefault(u => u.idUsuario == id);
                usuario.baja = true;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                id = -1;
            }
            return id;
        }
        public int DarDeAlta(int id)
        {

            try
            {
                Usuario usuario = db.Usuario.FirstOrDefault(u => u.idUsuario == id);
                usuario.baja = false;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                id = -1;
            }
            return id;
        }
        public UsuarioViewModel Login(Usuario usuario)
        {
            var q =
              from u in this.db.Usuario
              orderby u.email
              where u.email.ToLower().Equals(usuario.email.ToLower()) &&
                u.pass.ToString().Equals(usuario.pass.ToString()) &&
                u.baja == false
              select new UsuarioViewModel
              {
                  idUsuario = u.idUsuario,
                  Email = u.email,
                  idRol = (int)u.idrol
              };

            // Devolvemos datos
            return q.FirstOrDefault();
        }
        public Usuario GenerarHash(Usuario usuario)
        {
            var q =
                from u in this.db.Usuario
                orderby u.email
                where u.idUsuario == usuario.idUsuario
                select u;


            var us = q.FirstOrDefault();

            us.hash = usuario.hash;
            db.SaveChanges();

            return us;
        }
        public Usuario ComprobarHash(Usuario usuario)
        {
            var q =
                from u in this.db.Usuario
                orderby u.email
                where u.idUsuario == usuario.idUsuario &&
                    u.hash == usuario.hash
                select u;


            return q.FirstOrDefault();
        }
        public string getPassowrdPorIdUsuario(int idUsuario)
        {

            string Contrasena = (from u in this.db.Usuario
                                 where u.idUsuario == idUsuario
                                 select new { u.pass }).Single().pass;


            return Contrasena;
        }
        public int GetUltimaUsuario()
        {
            int idUltimoUsuario = 0;
            try
            {
                idUltimoUsuario = db.Usuario.Max(x => x.idUsuario);
            }
            catch
            {
                idUltimoUsuario = 0;
            }
            return idUltimoUsuario;
        }


        //public Usuario getUsuario(int id)
        //{
        //    var user = (from u in db.Usuario
        //                         where u.idUsuario == id
        //                         select u).FirstOrDefault();
        //    return user;
        //}


    }
}