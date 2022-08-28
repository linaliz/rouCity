REVIZAR ESTE MAIL QUE ES MAS CORTO

  public class Email
    {
        private ConfiguracionEmailOperations configuracionEmailOperations = new ConfiguracionEmailOperations();
        /*https://anexsoft.com/tutorial-sobre-programacion-asincrona-con-c-y-ejemplos-practicos*/
        public bool EnviarMail(string pDatosdestinatario, string pDatosAsunto, string pDatosMensaje, /*HttpPostedFileBase pAdjunto*/string rutaPDF)
        {
            try
            {
                string pDatosMail = "";
                string pDatosClave = "";
                string pDatosNombre = "";
                string pDatosSmtp = "";
                int pDatosPuerto = 0;
                string pDatosMailUsuario = "";

                ConfiguracionEmailViewModel configuracionEmailViewModel = new ConfiguracionEmailViewModel();
                configuracionEmailViewModel = configuracionEmailOperations.getConfiguracionEmail();

                pDatosMail = configuracionEmailViewModel.UsuarioEmail;
                pDatosClave = configuracionEmailViewModel.ContrasenaEmail;
                pDatosNombre = configuracionEmailViewModel.AliasEmail;
                pDatosSmtp = configuracionEmailViewModel.SmtpEmail;
                pDatosPuerto = configuracionEmailViewModel.PuertoEmail;

                using (Attachment pdfEmail = new Attachment(rutaPDF))
                {
                    char[] limitador = { ',' };
                    //Configuración del Mensaje
                    MailMessage mail = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient(pDatosSmtp);
                    //Especificamos el mail desde el que se enviará el Email y el nombre de la persona que lo envía
                    mail.From = new MailAddress(pDatosMail, pDatosNombre, Encoding.UTF8);
                    //Aquí ponemos el asunto del mail
                    mail.Subject = pDatosAsunto;
                    //Aquí ponemos el mensaje que incluirá el mail
                    mail.Body = pDatosMensaje;
                    //Especificamos a quien enviaremos el Email, no es necesario que sea Gmail, puede ser cualquier otro proveedor
                    if (pDatosdestinatario == "") { }
                    else
                    {
                        string[] cadena = pDatosdestinatario.Split(limitador);
                        foreach (string word in cadena) mail.To.Add(word.Trim());
                    }
                    //Si queremos enviar archivos adjuntos tenemos que especificar la ruta en donde se encuentran

                    if (File.Exists(rutaPDF) == true && rutaPDF != null)
                    {
                        /*Attachment pdfEmail = new Attachment(rutaPDF);*/
                        mail.Attachments.Add(pdfEmail);

                    }

                    //Configuracion del SMTP
                    SmtpServer.Port = pDatosPuerto; //Puerto que utiliza Gmail para sus servicios
                                                    //Especificamos las credenciales con las que enviaremos el mail
                    SmtpServer.Credentials = new System.Net.NetworkCredential(pDatosMailUsuario, pDatosClave);
                    
                    if (pDatosPuerto == 25)
                    {
                        SmtpServer.EnableSsl = false;
                    }
                    else
                    {
                        SmtpServer.EnableSsl = true;
                    }
                    SmtpServer.Send(mail);

                    /*SmtpServer.Dispose();*/
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return false;
            }
        }
		
		****
		namespace Agrodomo.Controllers
{
    [AuthFilter]
    public class DiarioCargasController : Controller
    {
        private AgroDB db = new AgroDB();

        // GET: DiarioCargas
        public ActionResult Index(bool? mostrarTodasLasCargas = false)
        {
            ViewBag.mostrarTodasLasCargas = mostrarTodasLasCargas;
            List<CargaViewModel> listado = new CargaDomain((int)Session["idUsuario"]).GetListado(mostrarTodasLasCargas);

            return View(listado);
        }        

        [HttpPost]
        public JsonResult Save(CargaSimplificada carga)
        {
            Respuesta respuesta = new Respuesta();
            var urlPDF = HttpContext.Server.MapPath("..");
            respuesta = new CargaDomain((int)Session["idUsuario"]).Save(carga, urlPDF);

            return Json(respuesta);
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            Respuesta respuesta = new Respuesta();
            respuesta = new CargaDomain((int)Session["idUsuario"]).Delete(id);

            return Json(respuesta);
        }

        [HttpPost]
        public JsonResult DetallesCarga(int idCarga)
        {
            var carga = new CargaDomain((int)Session["idUsuario"]).ObtenerCargaSimplificada(idCarga);
            return Json(carga);
        }
        [HttpPost]
        public JsonResult ObtenerIdentificadorMinutaCompraYNombreAlmacenYDireccionAlmacenPorIdCargaAlmacenOrigen(int idCargaOrigen)
        {
            var IndetificadorMCyNombreAlmacen = new CargaDomain((int)Session["idUsuario"]).GetIdentificadorMinutaCompraYNombreAlmacenYDireccionAlmacenPorIdCargaAlmacenOrigen(idCargaOrigen);
            return Json(IndetificadorMCyNombreAlmacen);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
	
	**un ejemplo de almacen domain
	using Agrodomo.Models;
using Agrodomo.Operations;
using Agrodomo.ViewModels;
using AgrodomoBBDD;
using System.Collections.Generic;
using System.Web.Helpers;

namespace Agrodomo.Domain
{
    public class AlmacenDomain
    {
        
        private AlmacenOperations almacenOperations = new AlmacenOperations();
        private LogDomain logDomain;
        private MinutaCompraOperations minutaCompraOperations = new MinutaCompraOperations();
        private MinutaVentaOperations minutaVentaOperations = new MinutaVentaOperations();
        public AlmacenDomain(int idUsuario)
        {
            logDomain = new LogDomain(idUsuario);
        }
        public List<AlmacenViewModel> GetListado(int idClienteProveedor, bool? filtroBaja)
        {
            return almacenOperations.GetListado(idClienteProveedor, filtroBaja);
        }

        public AlmacenViewModel ObtenerAlmacen(int idAlmacen)
        {
            AlmacenViewModel a = almacenOperations.GetAlmacen(idAlmacen);

            return a;
        }

        public AlmacenViewModel ObtenerAlmacenPorDefecto(int idClienteProveedor)
        {
            AlmacenViewModel almacenPorDefecto = almacenOperations.GetAlmacenPorDefecto(idClienteProveedor);

            return almacenPorDefecto;
        }

        public Respuesta Save(Almacen a)
        {
            Respuesta respuesta = ValidarFormulario(a);
            if (respuesta.Ok == true)
            {
                int res = almacenOperations.Save(a);
                if (res > 0)
                {
                    logDomain.insertLog(new Log()
                    {
                        Clase = "AlmacenDomain",
                        Metodo = "Insertar",
                        Mensaje = Json.Encode(a)
                    });
                    respuesta.Mensaje = new CodigoDescripcion<string>()
                    {
                        Key = "Almacén",
                        Value = "Almacén actualizado correctamente"
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
            Respuesta respuesta = ValidarDarDeBajaAlmacenes(id);

            if (respuesta.Ok == true)
            {
                int res = almacenOperations.DarDeBaja(id);

                if (res > 0)
                {
                    logDomain.insertLog(new Log()
                    {
                        Clase = "AlmacenDomain",
                        Metodo = "DarDeBaja",
                        Mensaje = Json.Encode(id)
                    });
                    respuesta.Mensaje = new CodigoDescripcion<string>()
                    {
                        Key = "Almacen",
                        Value = "Almacen eliminado correctamente"
                    };
                }
                else
                {
                    respuesta.Ok = false;
                    respuesta.Errores.Add("Error interno en servidor");
                }
            }
            else
            {
                respuesta.Ok = false;
            }
            return respuesta;
        }
        public Respuesta DarDeAlta(int id)
        {
            Respuesta respuesta = new Respuesta();

            int res = almacenOperations.DarDeAlta(id);

            if (res > 0)
            {
                logDomain.insertLog(new Log()
                {
                    Clase = "AlmacenDomain",
                    Metodo = "DarDeAlta",
                    Mensaje = Json.Encode(id)
                });
                respuesta.Mensaje = new CodigoDescripcion<string>()
                {
                    Key = "Almacen",
                    Value = "Almacen se ha dado de Alta correctamente"
                };
            }
            else
            {
                respuesta.Ok = false;
                respuesta.Errores.Add("Error interno en servidor");
            }
            return respuesta;
        }

        private Respuesta ValidarFormulario(Almacen a)
        {
            Respuesta respuesta = new Respuesta();
            respuesta = ValidarCampos.ValidarString("Nombre", a.nombreAlmacen, respuesta, 50, 3, true, false, false, false, false, true, false, false);
            respuesta = ValidarCampos.ValidarString("Dirección", a.direccion, respuesta, 150, 3, true, false, false, false, false, true, false, false);
            respuesta = ValidarCampos.ValidarString("Localidad", a.localidad, respuesta, 50, 3, true, false, false, false, false, true, false, false);
            respuesta = ValidarCampos.ValidarString("Provincia", a.provincia, respuesta, 50, 3, true, false, false, false, false, true, false, false);
            respuesta = ValidarCampos.ValidarString("CP", a.cp, respuesta, 10, 3, true, false, false, false, true, false, false, false);
            respuesta = ValidarCampos.ValidarString("CoordenadasGPS", a.coordenadasGPS, respuesta, 50, 3, false, false, false, false, false, false, false, false);
            respuesta = ValidarCampos.ValidarNumero("Importe Reducido", a.importeReducido.ToString(), respuesta, 0, 9999, false, false, true);
            respuesta = ValidarCampos.ValidarNumero("Porcentaje Reducido", a.porcentajeReducido.ToString(), respuesta, 0, 100, true, true, false);
            respuesta = ValidarCampos.ValidarClaveForanea("Zona", a.idZona.ToString(), respuesta);

            if (a.idAlmacen == 0)
                respuesta = this.ValidarDuplicados(a, respuesta);

            if (a.almacenPorDefecto == true)
            {
                var almacenes = new AlmacenOperations().GetAlmacenByClienteProveedor(a.idClienteProveedor);
                foreach (var al in almacenes)
                { 
                    if (al.idAlmacen != a.idAlmacen)
                    {
                        almacenOperations.ModificarAlmacenPorDefecto(al.idAlmacen);
                    }
                }
            }
            else
            {
                var almacenDefecto = almacenOperations.GetAlmacenPorDefecto(a.idClienteProveedor);
                if (almacenOperations.HayAlmacenPorDefecto(a.idClienteProveedor) == true && a.idAlmacen == almacenDefecto.idAlmacen)
                {
                    respuesta.Errores.Add("No se puede eliminar el almacen por defecto");
                    respuesta.Ok = false;
                    return respuesta;
                }
            }

            return respuesta;

        }

        public Respuesta ValidarDuplicados(Almacen a, Respuesta respuesta)
        {

            if (almacenOperations.GetCountAlmacen(a.nombreAlmacen) > 0)
                respuesta.Errores.Add("El nombre del almacén está duplicado");

            if (respuesta.Errores.Count > 0)
                respuesta.Ok = false;

            return respuesta;
        }

        public Respuesta ValidarDarDeBajaAlmacenes(int id)
        {
            Respuesta respuesta = new Respuesta();

            int totalMinutasCompraAbiertasYcerradaParcial = minutaCompraOperations.cuantasMinutasDeCompraEstanAbiertasYcerradaParcialesDeEseAlmacen(id);
            
            int totalMinutasVentaAbiertasYcerradaParcial = minutaVentaOperations.cuantasMinutasDeVentaEstanAbiertasYcerradaParcialesDeEseAlmacen(id);

            int totalMinutasAbiertasYcerradaParcial = totalMinutasCompraAbiertasYcerradaParcial + totalMinutasVentaAbiertasYcerradaParcial;

            respuesta.Ok = true;

            if (totalMinutasAbiertasYcerradaParcial > 0)
            {
                respuesta.Errores.Add("No se puede dar de baja el Almacén mientras haya alguna minuta abierta o cerrada parcial");
                respuesta.Ok = false;
            }

            var almacen = almacenOperations.GetAlmacen(id);

            if (almacenOperations.GetAlmacenPorDefecto(almacen.idClienteProveedor).idAlmacen == almacen.idAlmacen)
            {
                respuesta.Errores.Add("El Almacén por defecto no se puede dar de baja");
                respuesta.Ok = false;
            }

            /*else if (almacenOperations.HayStock(almacen.idAlmacen) > 0)
            {
                respuesta.Errores.Add("El Almacén tiene stock no se puede dar de baja");
                respuesta.Ok = false;
                return respuesta;
            }
            else
            {
                respuesta.Ok = true;
                return respuesta;
            }*/

            return respuesta;
        }
        
    }
}

**Controler de almacen
using System.Collections.Generic;
using System.Web.Mvc;
using Agrodomo.Domain;
using Agrodomo.Filters;
using Agrodomo.Models;
using Agrodomo.ViewModels;
using AgrodomoBBDD;

namespace Agrodomo.Controllers
{
    [AuthFilter]
    public class AlmacenController : Controller
    {
        private AgroDB db = new AgroDB();

        public ActionResult Index(int idClienteProveedor, bool? filtroBaja)
        {

            List<AlmacenViewModel> listado = new AlmacenDomain((int)Session["idUsuario"]).GetListado(idClienteProveedor, filtroBaja);
            return View(listado);
        }

        public ActionResult Form(int idClienteProveedor, int? idAlmacen)
        {
            var almacen = new AlmacenViewModel();
            if (idAlmacen != null)
            {
                ViewBag.accion = "Editar";
                almacen = new AlmacenDomain((int)Session["idUsuario"]).ObtenerAlmacen((int)idAlmacen);

            }
            else
            {
                almacen.idClienteProveedor = idClienteProveedor;
                ViewBag.accion = "Crear";

            }
            return View(almacen);
        }

        [HttpPost]
        public JsonResult Save(Almacen almacen)
        {
            Respuesta respuesta = new Respuesta();
            respuesta = new AlmacenDomain((int)Session["idUsuario"]).Save(almacen);

            return Json(respuesta);
        }

        [HttpPost]
        public JsonResult DarDeBaja(int id)
        {
            Respuesta respuesta = new Respuesta();
            respuesta = new AlmacenDomain((int)Session["idUsuario"]).DarDeBaja(id);

            return Json(respuesta);
        }
        [HttpPost]
        public JsonResult DarDeAlta(int id)
        {
            Respuesta respuesta = new Respuesta();
            respuesta = new AlmacenDomain((int)Session["idUsuario"]).DarDeAlta(id);

            return Json(respuesta);
        }

        [HttpPost]
        public JsonResult AlmacenPorDefecto(int idCLienteProveedor) 
        {
            var almacenPorDefecto = new AlmacenDomain((int)Session["idUsuario"]).ObtenerAlmacenPorDefecto(idCLienteProveedor);
            return Json(almacenPorDefecto);
        }

        [HttpPost]
        public JsonResult DetallesAlmacen(int idAlmacen)
        {
            AlmacenViewModel almacen = new AlmacenDomain((int)Session["idUsuario"]).ObtenerAlmacen(idAlmacen);
            return Json(almacen);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        
    }
}

****Operations de almacen
using Agrodomo.Models;
using Agrodomo.ViewModels;
using AgrodomoBBDD;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;

namespace Agrodomo.Operations
{
    public class AlmacenOperations
    {
        private AgroDB db = new AgroDB();

        public List<AlmacenViewModel> GetListado(int idClienteProveedor, bool? filtroBaja)
        {

            var q =
               from a in this.db.Almacen
               where a.idClienteProveedor == idClienteProveedor/* && a.baja == false*/
               orderby a.idAlmacen
               select new AlmacenViewModel
               {
                   idAlmacen = a.idAlmacen,
                   idClienteProveedor = a.idClienteProveedor,
                   nombreAlmacen = a.nombreAlmacen,
                   almacenPropio = a.almacenPropio,
                   direccion = a.direccion,
                   localidad = a.localidad,
                   provincia = a.provincia,
                   cp = a.cp,
                   coordenadasGPS = a.coordenadasGPS,
                   almacenPorDefecto = a.almacenPorDefecto,
                   baja = (bool)a.baja,
                   idZona = a.idZona,
                   Zona = a.Zona,
                   //zona = a.Zona,
                   reduccionPrecio = (bool)a.reduccionPrecio,
                   importeReducido = (decimal)a.importeReducido,
                   porcentajeReducido = (int)a.porcentajeReducido
               };
            
            //Filtro
            if (filtroBaja == false)
            {
                q = q.Where(a => a.baja == false);
            }

            // Devolvemos datos
            return q.ToList();

        }

        public AlmacenViewModel GetAlmacen(int id)
        {

            var q =
               from a in this.db.Almacen
               where a.idAlmacen == id /*&& a.baja == false*/
               select new AlmacenViewModel
               {
                   idAlmacen = a.idAlmacen,
                   idClienteProveedor = a.idClienteProveedor,
                   nombreAlmacen = a.nombreAlmacen,
                   almacenPropio = a.almacenPropio,
                   direccion = a.direccion,
                   localidad = a.localidad,
                   provincia = a.provincia,
                   cp = a.cp,
                   coordenadasGPS = a.coordenadasGPS,
                   almacenPorDefecto = a.almacenPorDefecto,
                   baja = (bool)a.baja,
                   idZona = a.idZona,
                   Zona = a.Zona,
                   reduccionPrecio = (bool)a.reduccionPrecio,
                   importeReducido = (decimal)a.importeReducido,
                   porcentajeReducido = (int)a.porcentajeReducido
               };

            // Devolvemos datos
            return q.FirstOrDefault();

        }
        
        public List<AlmacenViewModel> GetAlmacenByClienteProveedor(int idClienteProveedor)
        {

            var q =
               from a in this.db.Almacen
               where a.idClienteProveedor == idClienteProveedor && a.baja == false
               select new AlmacenViewModel
               {
                   idAlmacen = a.idAlmacen,
                   idClienteProveedor = a.idClienteProveedor,
                   nombreAlmacen = a.nombreAlmacen,
                   almacenPropio = a.almacenPropio,
                   direccion = a.direccion,
                   localidad = a.localidad,
                   provincia = a.provincia,
                   cp = a.cp,
                   coordenadasGPS = a.coordenadasGPS,
                   almacenPorDefecto = a.almacenPorDefecto,
                   baja = (bool)a.baja,
                   idZona = a.idZona,
                   Zona = a.Zona,
                   reduccionPrecio = (bool)a.reduccionPrecio,
                   importeReducido = (decimal)a.importeReducido,
                   porcentajeReducido = (int)a.porcentajeReducido

               };
            // Devolvemos datos
            return q.ToList();

        }

        public AlmacenViewModel GetAlmacenPorDefecto(int idClienteProveedor)
        {
            db.Configuration.ProxyCreationEnabled = false;
            int idAlmacenPorDefecto = 0;
            try
            {
               idAlmacenPorDefecto = (from a in this.db.Almacen
                                           where a.idClienteProveedor == idClienteProveedor
                                           && a.baja == false
                                           && a.almacenPorDefecto == true

                                           select new { a.idAlmacen }).Single().idAlmacen;
            }catch(Exception e)
            {
                idAlmacenPorDefecto = 0;
            }
            

            AlmacenViewModel almacenPorDefecto = GetAlmacen(idAlmacenPorDefecto);

            return almacenPorDefecto;
        }

        public int GetCountAlmacen(string nombre)
        {

            var q =
               from a in this.db.Almacen
               where a.nombreAlmacen.ToLower().Equals(nombre.ToLower())
               select new AlmacenViewModel
               {
                   idAlmacen = a.idAlmacen,
                   nombreAlmacen = a.nombreAlmacen,

               };

            // Devolvemos datos
            return q.Count();

        }

        public int Save(Almacen almacen)
        {
            int id = 0;

            try
            {
                if (almacen.idAlmacen > 0)
                {
                    Almacen nuevoAlmacen = db.Almacen.FirstOrDefault(a => a.idAlmacen == almacen.idAlmacen);

                    db.Entry(nuevoAlmacen).CurrentValues.SetValues(almacen);
                }
                else
                {
                    db.Almacen.Add(almacen);
                }

                db.SaveChanges();
                id = almacen.idAlmacen;
            }
            catch (DbEntityValidationException e)
            {
                id = -2;
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
                Almacen almacen = db.Almacen.FirstOrDefault(a => a.idAlmacen == id);

                almacen.baja = true;
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
                Almacen almacen = db.Almacen.FirstOrDefault(a => a.idAlmacen == id);

                almacen.baja = false;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                id = -1;
            }
            return id;
        }
        public CodigoDescripcion<int>[] getListadoCodigoDescripcionSoloDeAlta(string filtroNombre, int idClienteProveedor)
        {

            var q =
               from a in this.db.Almacen
               where a.baja == false
               where idClienteProveedor != 0 ? a.idClienteProveedor == idClienteProveedor : 1 == 1 &&
                    filtroNombre.CompareTo("") != 0 ? a.nombreAlmacen.Contains(filtroNombre) : 1 == 1
                    //Asi no funcionaba (hay que poner dos where)
                    //&& a.baja == false
               select new CodigoDescripcion<int>
               {
                   Key = a.idAlmacen,
                   Value = a.nombreAlmacen
               };



            return q.ToArray();

        }
        public CodigoDescripcion<int>[] getListadoCodigoDescripcionTodos(string filtroNombre, int idClienteProveedor)
        {

            var q =
               from a in this.db.Almacen
               where idClienteProveedor != 0 ? a.idClienteProveedor == idClienteProveedor : 1 == 1 &&
                    filtroNombre.CompareTo("") != 0 ? a.nombreAlmacen.Contains(filtroNombre) : 1 == 1
               //Asi no funcionaba (hay que poner dos where)
               //&& a.baja == false
               select new CodigoDescripcion<int>
               {
                   Key = a.idAlmacen,
                   Value = a.nombreAlmacen
               };



            return q.ToArray();

        }

        public ICollection<Almacen> ListaAlmacenesProveedores(int idProveedor)
        {
            var listaAlmacenes = db.Almacen.Where(a => a.idClienteProveedor == idProveedor).ToList();
            return listaAlmacenes;
        }

        public void ModificarAlmacenPorDefecto(int idAlmacen)
        {
            var almacenPorDefecto = false;

            Almacen almacen = db.Almacen.FirstOrDefault(a => a.idAlmacen == idAlmacen);
            almacen.almacenPorDefecto = almacenPorDefecto;
            db.SaveChanges();
        }

        public bool HayAlmacenPorDefecto(int idClienteProveedor)
        {
            db.Configuration.ProxyCreationEnabled = false;
            int almacenPD = (from a in this.db.Almacen
                         where a.idClienteProveedor == idClienteProveedor && a.almacenPorDefecto == true
                         select new { a.idAlmacen }).Count();

            if (almacenPD > 0)
                return true;
            else
                return false;
        }

        public int HayStock(int idAlmacen)
        {
            db.Configuration.ProxyCreationEnabled = false;
            int stock = (from s in this.db.Stock
                         where s.idAlmacen == idAlmacen
                         select new { s.idStock }).Count();
            
            return stock;
        }
       
    }
}