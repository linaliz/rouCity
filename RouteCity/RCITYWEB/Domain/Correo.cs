using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace RCITYWEB.Domain
{
    public class Correo
    {
        public int Enviarcorreo(string emailUsuario)
        {
            int repuesta = 0;
            string body =
                "<body>" +
                "<h1>Confirmación de Cuenta </h1>" +
                "<p>Bienvenido a RouteCity.  Para confirmar su cuenta pulse <a href=\"https://www.Rcity.es\">Aqui</a>. </p>" +
                "</body>";

            try
            {
                string correo = ConfigurationManager.AppSettings["correo"];
                string clave = ConfigurationManager.AppSettings["clave"];
                string servidor = ConfigurationManager.AppSettings["servidor"];
                int puerto = int.Parse(ConfigurationManager.AppSettings["puerto"]);
                //Data del correo
                MailMessage mail = new MailMessage();
                mail.Subject = "Confirmación a RouteCity";
                mail.IsBodyHtml = true;
                mail.Body = body;
                mail.From = new MailAddress(correo);
                mail.To.Add(new MailAddress(emailUsuario));
                //Envio del correo
                SmtpClient smtp = new SmtpClient();
                smtp.Host = servidor;
                smtp.EnableSsl = true;
                smtp.Port = puerto;
                smtp.Credentials = new NetworkCredential(correo, clave);
                smtp.Send(mail);
                repuesta = 1;
            }
            catch (Exception ex)
            {
                repuesta = 0;
            }
            return repuesta;
        }
    }
}