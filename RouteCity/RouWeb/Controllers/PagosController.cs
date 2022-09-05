using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using RouWeb.Models;

namespace RouWeb.Controllers
{
    public class PagosController : Controller
    {
        // GET: Pagos
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> result()
        {
            //ViewBag.Token = token;

            //id de la autorizacion para obtener el dinero
            string token = Request.QueryString["token"];

            bool status = false;


            using (var client = new HttpClient())
            {

                //Client ID y Secret
                var userName = "AUNOFSixITKvmLTwNJkj5mtOqhiiMXJad31-cWqatHLQrCPKfPMwvAADdVJvSflliZYCgmMaTd9tw_5W";
                var passwd = "EF4fzK6VCGd3c1jVU9_PqzH1QX9m0m7l5ok6DUgy_RjtDLDJPUoCFDv-jmCOnqM9Hpt7ptv1jKaOM6ux";

                client.BaseAddress = new Uri("https://api-m.sandbox.paypal.com");

                var authToken = Encoding.ASCII.GetBytes($"{userName}:{passwd}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authToken));

                var data = new StringContent("{}", Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync($"/v2/checkout/orders/{token}/capture", data);


                status = response.IsSuccessStatusCode;

                ViewData["Status"] = status;
                if (status)
                {
                    var jsonRespuesta = response.Content.ReadAsStringAsync().Result;

                    PaypalTransaction objeto = JsonConvert.DeserializeObject<PaypalTransaction>(jsonRespuesta);

                    ViewData["IdTransaccion"] = objeto.purchase_units[0].payments.captures[0].id;
                }

            }


            return View();
        }


        [HttpPost]
        public async Task<JsonResult> Paypal(string precio, string viaje)
        {



            bool status = false;
            string respuesta = string.Empty;

            using (var client = new HttpClient())
            {

                //Client ID y Secret
                var userName = "AUNOFSixITKvmLTwNJkj5mtOqhiiMXJad31-cWqatHLQrCPKfPMwvAADdVJvSflliZYCgmMaTd9tw_5W";
                var passwd = "EF4fzK6VCGd3c1jVU9_PqzH1QX9m0m7l5ok6DUgy_RjtDLDJPUoCFDv-jmCOnqM9Hpt7ptv1jKaOM6ux";

                client.BaseAddress = new Uri("https://api-m.sandbox.paypal.com");

                var authToken = Encoding.ASCII.GetBytes($"{userName}:{passwd}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authToken));


                var orden = new PaypalOrder()
                {
                    intent = "CAPTURE",
                    purchase_units = new List<PurchaseUnit>() {

                        new PurchaseUnit() {

                            amount = new Amount()
                            {
                                currency_code = "EUR",
                                value = precio
                            },
                            description = viaje
                        }
                    },
                    application_context = new ApplicationContext()
                    {
                        brand_name = "RouCity",
                        landing_page = "NO_PREFERENCE",
                        user_action = "PAY_NOW", //Accion para que paypal muestre el monto de pago
                        return_url = "http://localhost:65485/Pagos/result",// cuando se aprovo la solicitud del cobro
                        cancel_url = "http://localhost:65485/Viaje/Index"// cuando cancela la operacion
                    }
                };


                var json = JsonConvert.SerializeObject(orden);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync("/v2/checkout/orders", data);


                status = response.IsSuccessStatusCode;


                if (status)
                {
                    respuesta = response.Content.ReadAsStringAsync().Result;

                    //Conectar a la BD
                    //SqlConnection conn = new SqlConnection("Server=localhost:3306;Database=pruebacon;User Id=root;Password=pass");
                    //conn.Open();

                    //Save transaction to database
                    PaypalTransaction objeto = JsonConvert.DeserializeObject<PaypalTransaction>(respuesta);
                    //SaveToDB(conn, objeto.id, precio, 1, 1);

                    //conn.Close();
                }



            }

            return Json(new { status = status, respuesta = respuesta }, JsonRequestBehavior.AllowGet);

        }

    }
}
