using DBRouCity;
using System;
using RCITYWEB.Models;
using System.Web.Mvc;
using System.Collections.Generic;
using RCITYWEB.Operations;
using RCITYWEB.ViewModels;
using RCITYWEB.Domain.Util;
using System.Web.Helpers;

namespace RCITYWEB.Domain
{
  
        public class UsuarioDomain
        {
            private UsuarioOper UsuarioOper = new UsuarioOper();
            
            public UsuarioDomain()
            { }
           
            public List<UsuarioViewModel> GetListado(bool? filtroBaja)
            {
                return UsuarioOper.GetListado(filtroBaja);
            }

            public UsuarioViewModel ObtenerUsuario(int id)
            {
                UsuarioViewModel p = UsuarioOper.GetUsuario(id);

                return p;
            }

            public Respuesta Save(Usuario u)
            {
                Respuesta respuesta = ValidarFormulario(u);

                if (u.email != null)
                    u.email = u.email.Trim();
                if (respuesta.Ok == true)
                {
                    string passwordBD = null;
                    if (u.idUsuario != 0)
                    {
                        passwordBD = UsuarioOper.getPassowrdPorIdUsuario(u.idUsuario);//Aqui sacariamos la antigua contraseña
                    }
                    string cadenaSinEditar = "#@!%&";//Esta seria la cadena que estaria si no editamos la contraseña
                    if (String.Equals(u.pass, cadenaSinEditar) == false)
                    {
                        u.pass = CryptoUtil.base64Encode(CryptoUtil.EncodePasswordMd5(u.pass));
                    }
                    else if (String.Equals(u.pass, cadenaSinEditar) == true)
                    {
                        u.pass = passwordBD;
                    }

                    //Solo hay un rol asique ponemos este
                    u.idrol = 1;

                    int res = UsuarioOper.Save(u);
                    if (res > 0)
                    {
                       
                        respuesta.Mensaje = new CodigoDescripcion<string>()
                        {
                            Key = "Usuario",
                            Value = "Usuario actualizado correctamente"
                        };
                    }
                    else
                    {
                        respuesta.Ok = false;
                        respuesta.Errores.Add("Error interno en servidor");
                    }
                }
                return respuesta;
            }

            public Respuesta DarDeBaja(int id)
            {
                Respuesta respuesta = new Respuesta();

                int res = UsuarioOper.DarDeBaja(id);
                if (res > 0)
                {

                    respuesta.Mensaje = new CodigoDescripcion<string>()
                    {
                        Key = "Usuario",
                        Value = "Usuario eliminado correctamente"
                    };
                }
                else
                {
                    respuesta.Ok = false;
                    respuesta.Errores.Add("Error interno en servidor");
                }

                return respuesta;
            }
            public Respuesta DarDeAlta(int id)
            {
                Respuesta respuesta = new Respuesta();

                int res = UsuarioOper.DarDeAlta(id);
                if (res > 0)
                {
                  
                    respuesta.Mensaje = new CodigoDescripcion<string>()
                    {
                        Key = "Usuario",
                        Value = "Usuario se ha dado de Alta correctamente"
                    };
                }
                else
                {
                    respuesta.Ok = false;
                    respuesta.Errores.Add("Error interno en servidor");
                }

                return respuesta;
            }

            private Respuesta ValidarFormulario(Usuario u)
            {
                Respuesta respuesta = new Respuesta();
                respuesta = ValidarCampos.ValidarString("Nombre", u.nombre, respuesta, 50, 3, true, false, false, false, false, false, false, false);
                respuesta = ValidarCampos.ValidarString("Contraseña", u.pass, respuesta, 10, 5, true, false, false, false, false, false, false, false);


                if (u.idUsuario == 0)
                    respuesta = ValidarDuplicados(u, respuesta);



                return respuesta;

            }

            public Respuesta ValidarDuplicados(Usuario u, Respuesta respuesta)
            {

                if (UsuarioOper.GetCountUsuario(u.email) > 0)
                    respuesta.Errores.Add("Este email ya existe");

                if (respuesta.Errores.Count > 0)
                    respuesta.Ok = false;

                return respuesta;
            }

            public UsuarioViewModel Login(Usuario usuario)
            {
                UsuarioViewModel u = UsuarioOper.Login(usuario);
                if (u != null)
                {
                    var user = UsuarioOper.GenerarHash(new Usuario()
                    {
                        idUsuario = u.idUsuario,
                        hash = CryptoUtil.base64Encode(CryptoUtil.EncodePassword(DateTime.Now.Ticks.ToString()))
                    });
                    u.hash = user.hash;
                }


                return u;
            }

            public string ComprobarHash(Usuario usuario)
            {
                String r = "";
                var user = UsuarioOper.ComprobarHash(usuario);
                if (user != null)
                {
                    user = UsuarioOper.GenerarHash(user);
                    r = user.hash;
                }

                return r;
            }
            public int GetUltimaUsuario()
            {
                return UsuarioOper.GetUltimaUsuario();
            }



        }
}