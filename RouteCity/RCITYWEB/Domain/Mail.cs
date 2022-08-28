using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Net.Mail;
using System.Threading.Tasks;



namespace RCITYWEB.Domain
{
    public class Mail 
    {
        //private ConfiguracionEmailOperations configuracionEmailOperations = new ConfiguracionEmailOperations();
        ///*https://anexsoft.com/tutorial-sobre-programacion-asincrona-con-c-y-ejemplos-practicos*/
        //public bool EnviarMail(string pDatosdestinatario, string pDatosAsunto, string pDatosMensaje, /*HttpPostedFileBase pAdjunto*/string rutaPDF)
        //{
        //    try
        //    {
        //        string pDatosMail = "";
        //        string pDatosClave = "";
        //        string pDatosNombre = "";
        //        string pDatosSmtp = "";
        //        int pDatosPuerto = 0;
        //        string pDatosMailUsuario = "";

        //        ConfiguracionEmailViewModel configuracionEmailViewModel = new ConfiguracionEmailViewModel();
        //        configuracionEmailViewModel = configuracionEmailOperations.getConfiguracionEmail();

        //        pDatosMail = configuracionEmailViewModel.UsuarioEmail;
        //        pDatosClave = configuracionEmailViewModel.ContrasenaEmail;
        //        pDatosNombre = configuracionEmailViewModel.AliasEmail;
        //        pDatosSmtp = configuracionEmailViewModel.SmtpEmail;
        //        pDatosPuerto = configuracionEmailViewModel.PuertoEmail;

        //        using (Attachment pdfEmail = new Attachment(rutaPDF))
        //        {
        //            char[] limitador = { ',' };
        //            //Configuración del Mensaje
        //            MailMessage mail = new MailMessage();
        //            SmtpClient SmtpServer = new SmtpClient(pDatosSmtp);
        //            //Especificamos el mail desde el que se enviará el Email y el nombre de la persona que lo envía
        //            mail.From = new MailAddress(pDatosMail, pDatosNombre, Encoding.UTF8);
        //            //Aquí ponemos el asunto del mail
        //            mail.Subject = pDatosAsunto;
        //            //Aquí ponemos el mensaje que incluirá el mail
        //            mail.Body = pDatosMensaje;
        //            //Especificamos a quien enviaremos el Email, no es necesario que sea Gmail, puede ser cualquier otro proveedor
        //            if (pDatosdestinatario == "") { }
        //            else
        //            {
        //                string[] cadena = pDatosdestinatario.Split(limitador);
        //                foreach (string word in cadena) mail.To.Add(word.Trim());
        //            }
        //            //Si queremos enviar archivos adjuntos tenemos que especificar la ruta en donde se encuentran

        //            if (File.Exists(rutaPDF) == true && rutaPDF != null)
        //            {
        //                /*Attachment pdfEmail = new Attachment(rutaPDF);*/
        //                mail.Attachments.Add(pdfEmail);

        //            }

        //            //Configuracion del SMTP
        //            SmtpServer.Port = pDatosPuerto; //Puerto que utiliza Gmail para sus servicios
        //                                            //Especificamos las credenciales con las que enviaremos el mail
        //            SmtpServer.Credentials = new System.Net.NetworkCredential(pDatosMailUsuario, pDatosClave);

        //            if (pDatosPuerto == 25)
        //            {
        //                SmtpServer.EnableSsl = false;
        //            }
        //            else
        //            {
        //                SmtpServer.EnableSsl = true;
        //            }
        //            SmtpServer.Send(mail);

        //            /*SmtpServer.Dispose();*/
        //        }
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.Write(ex.Message);
        //        return false;
        //    }

        //}

    }
}

