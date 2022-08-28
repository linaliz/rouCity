  public ActionResult Login2(Usuarios u)
        {

            if (!String.IsNullOrEmpty(u.Contrasena)) u.Contrasena = CryptoUtil.base64Encode(CryptoUtil.EncodePasswordMd5(u.Contrasena));
            var usuario = new UsuarioDomain().Login(u);
            // Usuario encontrado == true
            if (usuario != null)
            {
                // Creo una cookie para la sesion
                HttpCookie Cookie = new HttpCookie("cookie-sesion")
                {
                    Value = usuario.Usuario,
                    Expires = DateTime.Now.AddDays(1)
                };
                HttpContext.Response.Cookies.Add(Cookie);
                Usuarios usu = new Usuarios()
                {
                    idUsuario = usuario.idUsuario,
                    idRol = usuario.idRol,
                    hash = usuario.hash,
                    Usuario = usuario.Usuario
                };
                Session["Usuario"] = usu;

                Session["idUsuario"] = usuario.idUsuario;

                return RedirectToAction("Index", "MinutaCompra");
            }

            ViewBag.ErrorMessage = "Usuario y/o contraseña erróneos";
            return View("Login");
        }



        // GET: Usuarios/Create
        public ActionResult Registro()
        {
           
            return View();
        }

        // POST: Usuarios/Create
        // Registro de Usuarios
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registro([Bind(Include = "Nombre,Email,Pass")] Usuario  usuario)
        {

            var oUser = (from d in db.Usuario
                         where d.email == usuario.email.Trim() 
                         select d).FirstOrDefault();
           

            if (oUser!=null) {
                ViewBag.errorMailUnique = "Ya hay alguien usando ese correo"; 
            }else if (usuario.pass.Length<8)
            {
                ViewBag.errorPassMin = "Tu contraseña debe tener más de 8 carácteres";
            }else
            {
                {
                    db.Usuario.Add(usuario);
                    db.SaveChanges();
                    return RedirectToAction("Login");
                }
            }
           
            return View(usuario);
        }
        public ActionResult Cerrar()
        {
            HttpCookie cookie = HttpContext.Request.Cookies["Sesion"];
            cookie.Expires = DateTime.Now.AddDays(-10);
            cookie.Value = null;
            HttpContext.Response.Cookies.Add(cookie);
            //Response.Cookies.Clear();

            //return View("Index");
            return RedirectToAction("Index", "Home");
        }