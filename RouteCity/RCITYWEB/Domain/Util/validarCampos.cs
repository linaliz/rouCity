using RCITYWEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace RCITYWEB.Domain.Util
{
    public static class ValidarCampos
    {


        public static Respuesta ValidarString(string nombreCampo, string valor, Respuesta respuesta, int? longitudMaxima = 0, int? longitudMinima = 0, bool? obligatorio = false,
                                                bool? esEmail = false, bool? esTelefono = false, bool? esIban = false, bool? esCodigoPostal = false, bool? permiteEspacios = true, bool? esMatricula = false, bool? esNif = false)
        {
            if (valor != null)
                valor = valor.Trim();

            if (obligatorio == true && string.IsNullOrWhiteSpace(valor))
                respuesta.Errores.Add("El campo " + nombreCampo + " es obligatorio");
            else
            {
                if (string.IsNullOrWhiteSpace(valor) == false && valor.Length < longitudMinima)
                    respuesta.Errores.Add("El campo " + nombreCampo + " tiene que tener al menos " + longitudMinima + " caracteres.");

                if (string.IsNullOrWhiteSpace(valor) == false && (longitudMaxima > 0 && (valor.Length > longitudMaxima)))
                    respuesta.Errores.Add("El campo " + nombreCampo + " tiene que tener un maximo de " + longitudMaxima + " caracteres.");

                if (string.IsNullOrWhiteSpace(valor) == false && (esEmail == true && !EmailValido(valor)))
                    respuesta.Errores.Add("El campo " + nombreCampo + " debe contener un email válido.");

                if (string.IsNullOrWhiteSpace(valor) == false && (esTelefono == true && !TelefonoValido(valor)))
                    respuesta.Errores.Add("El campo " + nombreCampo + " debe contener un teléfono válido.");

                if (string.IsNullOrWhiteSpace(valor) == false && (esIban == true && !IBANValido(valor)))
                    respuesta.Errores.Add("El campo " + nombreCampo + " debe contener un IBAN válido.");

                if (string.IsNullOrWhiteSpace(valor) == false && (esCodigoPostal == true && !CPValido(valor)))
                    respuesta.Errores.Add("El campo " + nombreCampo + " debe contener un código postal válido.");

                if (string.IsNullOrWhiteSpace(valor) == false && (permiteEspacios == false && ValidarEspacios(valor)))
                    respuesta.Errores.Add("El campo " + nombreCampo + " no debe contener espacios en blanco.");

                if (string.IsNullOrWhiteSpace(valor) == false && (esMatricula == true && !MatriculaValida(valor)))
                    respuesta.Errores.Add("El campo " + nombreCampo + " debe contener una matrícula válida.");

                if (string.IsNullOrWhiteSpace(valor) == false && (esNif == true && !validarNifActualizado(valor)))
                    respuesta.Errores.Add("El campo " + nombreCampo + " debe contener un NIF válido.");
            }

            if (respuesta.Errores.Count > 0)
                respuesta.Ok = false;

            return respuesta;
        }

        public static Respuesta ValidarNumero(string nombreCampo, string valor, Respuesta respuesta, int? valorMinimo = 0, int? valorMaximo = 0, bool? obligatorio = false,
                                                bool? esInt = false, bool? esDecimal = false)
        {
            int n;
            decimal m;
            if (obligatorio == true && string.IsNullOrWhiteSpace(valor))
                respuesta.Errores.Add("El campo " + nombreCampo + " es obligatorio y tiene que ser un número.");
            else
            {
                if (!string.IsNullOrWhiteSpace(valor))
                {
                    if (esInt == true)
                    {
                        if (Int32.TryParse(valor, out n) == true)
                        {
                            if (Int32.Parse(valor) < valorMinimo)
                                respuesta.Errores.Add("El campo " + nombreCampo + " tiene que tener un valor mínimo de " + valorMinimo + ".");

                            if (Int32.Parse(valor) > valorMaximo)
                                respuesta.Errores.Add("El campo " + nombreCampo + " tiene que tener un valor máximo de " + valorMaximo + ".");
                        }
                        else
                        {
                            respuesta.Errores.Add("El campo " + nombreCampo + " debe ser un entero.");
                        }
                    }

                    if (esDecimal == true)
                    {
                        if (Decimal.TryParse(valor, out m) == true)
                        {
                            if (Decimal.Parse(valor) < valorMinimo)
                                respuesta.Errores.Add("El campo " + nombreCampo + " tiene que tener un valor mínimo de " + valorMinimo + ".");

                            if (Decimal.Parse(valor) > valorMaximo)
                                respuesta.Errores.Add("El campo " + nombreCampo + " tiene que tener un valor máximo de " + valorMaximo + ".");
                        }
                        else
                        {
                            respuesta.Errores.Add("El campo " + nombreCampo + " debe ser un decimal.");
                        }
                    }
                }

            }

            if (respuesta.Errores.Count > 0)
                respuesta.Ok = false;

            return respuesta;
        }


        public static Respuesta ValidarClaveForanea(string nombreCampo, string valor, Respuesta respuesta)
        {

            if (valor.CompareTo("0") == 0)
            {
                /*respuesta.Errores.Add("Es necesario seleccionar un/a " + nombreCampo);*/
                respuesta.Errores.Add("El campo " + nombreCampo + " es obligatorio");
            }
            if (respuesta.Errores.Count > 0)
                respuesta.Ok = false;

            return respuesta;

        }

        private static bool EmailValido(string valor)
        {
            return Regex.IsMatch(valor, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
        }

        private static bool TelefonoValido(string valor)
        {
            //Valida teléfonos móviles (6,7) y fijos (8,9) españoles, con o sin prefijo español (+34, 0034, 34), sin separar o separados por espacios o guiones.

            return Regex.IsMatch(valor, @"(\+34|0034|34)?[ -]*(6|7|8|9)[ -]*([0-9][ -]*){8}$");
        }

        private static bool IBANValido(string valor)
        {
            //Valida todos los IBAN internacionales
            return Regex.IsMatch(valor, @"[a-zA-Z]{2}[0-9]{2}[a-zA-Z0-9]{4}[0-9]{7}([a-zA-Z0-9]?){0,16}");
        }

        private static bool CPValido(string valor)
        {
            //Los códigos postales deben tener 5 cifras y comprenderse entre 01000 y 52999
            return Regex.IsMatch(valor, @"^(?:0[1-9]|[1-4]\d|5[0-2])\d{3}$");
        }

        public static bool ValidarEspacios(string valor)
        {
            if (valor.IndexOf(" ") != -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool MatriculaValida(string valor)
        {
            //Formato Matriculas de 4 numeros y 3 letras formato Europa España, sin separar o separados por espacios o guiones.
            return Regex.IsMatch(valor, @"(([0-9][0-9][0-9][0-9][A-Z][A-Z][A-Z]))");
        }

       
        /*
         * VALIDACIONES DE FECHAS
         */
        public static Respuesta ValidarFechaNoNula(string nombreCampo, DateTime? fecha, Respuesta respuesta)
        {
            if (fecha == null)
            {
                respuesta.Errores.Add("El campo " + nombreCampo + " es obligatorio");
            }
            if (respuesta.Errores.Count > 0)
                respuesta.Ok = false;

            return respuesta;

        }
        // Validamos que la fecha de inicio no sea despues que la fecha final
        public static Respuesta ValidarFechaDesdeFechaHasta(DateTime fechaDesde, DateTime fechaHasta, Respuesta respuesta)
        {
            if (fechaDesde != null && fechaHasta != null)
            {
                if (fechaHasta < fechaDesde)
                {
                    respuesta.Errores.Add("La fecha hasta debe ser mayor o igual a la fecha desde");
                }
            }
            if (respuesta.Errores.Count > 0)
                respuesta.Ok = false;

            return respuesta;
        }
 
        public static bool ValidaNIFNIE(string data)
        {
            if (String.IsNullOrEmpty(data) || data.Length < 8)
                return false;

            var initialLetter = data.Substring(0, 1).ToUpper();
            if (Char.IsLetter(data, 0))
            {
                switch (initialLetter)
                {
                    case "X":
                        data = "0" + data.Substring(1, data.Length - 1);
                        return ValidaNIF(data);
                    case "Y":
                        data = "1" + data.Substring(1, data.Length - 1);
                        return ValidaNIF(data);
                    case "Z":
                        data = "2" + data.Substring(1, data.Length - 1);
                        return ValidaNIF(data);
                    default:
                        if (new Regex("[A-Za-z][0-9]{7}[A-Za-z0-9]{1}$").Match(data).Success)
                            return ValidaNIF(data);
                        break;
                }
            }
            else if (Char.IsLetter(data, data.Length - 1))
            {
                if (new Regex("[0-9]{8}[A-Za-z]").Match(data).Success || new Regex("[0-9]{7}[A-Za-z]").Match(data).Success)
                    return ValidaNIF(data);
            }
            return false;
        }

        private static string getLetra(int id)
        {
            Dictionary<int, String> letras = new Dictionary<int, string>();
            letras.Add(0, "T");
            letras.Add(1, "R");
            letras.Add(2, "W");
            letras.Add(3, "A");
            letras.Add(4, "G");
            letras.Add(5, "M");
            letras.Add(6, "Y");
            letras.Add(7, "F");
            letras.Add(8, "P");
            letras.Add(9, "D");
            letras.Add(10, "X");
            letras.Add(11, "B");
            letras.Add(12, "N");
            letras.Add(13, "J");
            letras.Add(14, "Z");
            letras.Add(15, "S");
            letras.Add(16, "Q");
            letras.Add(17, "V");
            letras.Add(18, "H");
            letras.Add(19, "L");
            letras.Add(20, "C");
            letras.Add(21, "K");
            letras.Add(22, "E");
            return letras[id];
        }

        private static bool ValidaNIF(string data)
        {
            if (data == String.Empty)
                return false;
            try
            {
                String letra;
                letra = data.Substring(data.Length - 1, 1);
                data = data.Substring(0, data.Length - 1);
                int nifNum = int.Parse(data);
                int resto = nifNum % 23;
                string tmp = getLetra(resto);
                if (tmp.ToLower() != letra.ToLower())
                    return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }

        private static bool ValidaCIF(string data)
        {
            try
            {
                int pares = 0;
                int impares = 0;
                int suma;
                string ultima;
                int unumero;
                string[] uletra = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "0" };
                string[] fletra = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
                int[] fletra1 = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };
                string xxx;
                data = data.ToUpper();

                ultima = data.Substring(8, 1);

                int cont = 1;
                for (cont = 1; cont < 7; cont++)
                {
                    xxx = (2 * int.Parse(data.Substring(cont++, 1))) + "0";
                    impares += int.Parse(xxx.ToString().Substring(0, 1)) + int.Parse(xxx.ToString().Substring(1, 1));
                    pares += int.Parse(data.Substring(cont, 1));
                }

                xxx = (2 * int.Parse(data.Substring(cont, 1))) + "0";
                impares += int.Parse(xxx.Substring(0, 1)) + int.Parse(xxx.Substring(1, 1));

                suma = pares + impares;
                unumero = int.Parse(suma.ToString().Substring(suma.ToString().Length - 1, 1));
                unumero = 10 - unumero;
                if (unumero == 10) unumero = 0;

                if ((ultima == unumero.ToString()) || (ultima == uletra[unumero - 1]))
                    return true;
                else
                    return false;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        private static bool ValidarDNI(string dni)
        {
            //Comprobamos si el DNI tiene 9 digitos
            if (dni.Length != 9)
            {
                //No es un DNI Valido
                return false;
            }

            //Extraemos los números y la letra
            string dniNumbers = dni.Substring(0, dni.Length - 1);
            string dniLeter = dni.Substring(dni.Length - 1, 1);
            //Intentamos convertir los números del DNI a integer
            var numbersValid = int.TryParse(dniNumbers, out int dniInteger);
            if (!numbersValid)
            {
                //No se pudo convertir los números a formato númerico
                return false;
            }
            if (CalculateDNILeter(dniInteger) != dniLeter)
            {
                //La letra del DNI es incorrecta
                return false;
            }
            //DNI Valido :)
            return true;
        }

        private static string CalculateDNILeter(int dniNumbers)
        {
            //Cargamos los digitos de control
            string[] control = { "T", "R", "W", "A", "G", "M", "Y", "F", "P", "D", "X", "B", "N", "J", "Z", "S", "Q", "V", "H", "L", "C", "K", "E" };
            var mod = dniNumbers % 23;
            return control[mod];
        }

        //Esto es el CIF antiguo (se dejo de usar en 2008)
        public static bool validarNIFdePersonasJuridicas(string cif)
        {
            if (string.IsNullOrEmpty(cif)) return false;
            cif = cif.Trim().ToUpper();

            //Debe tener una longitud igual a 9 caracteres;
            if (cif.Length != 9) return false;
            // ... y debe comenzar por una letra, la cual pasamos a mayúscula, ... 
            // 
            string firstChar = cif.Substring(0, 1);
            // ...que necesariamente deberá de estar comprendida en 
            // el siguiente intervalo: ABCDEFGHJNPQRSUVW 
            // 
            string cadena = "ABCDEFGHJNPQRSUVW";
            if (cadena.IndexOf(firstChar) == -1) return false;
            try
            {
                Int32 sumaPar = default(Int32);
                Int32 sumaImpar = default(Int32);
                // A continuación, la cadena debe tener 7 dígitos + el dígito de control. 
                // 
                string cif_sinControl = cif.Substring(0, 8);
                string digits = cif_sinControl.Substring(1, 7);
                for (Int32 n = 0; n <= digits.Length - 1; n += 2)
                {
                    if (n < 6)
                    {
                        // Sumo las cifras pares del número que se corresponderá 
                        // con los caracteres 1, 3 y 5 de la variable «digits». 
                        // 
                        sumaPar += Convert.ToInt32(digits[n + 1].ToString());
                    }
                    // Multiplico por dos cada cifra impar (caracteres 0, 2, 4 y 6). 
                    // 
                    Int32 dobleImpar = 2 * Convert.ToInt32(digits[n].ToString());
                    // Acumulo la suma del doble de números impares. 
                    // 
                    sumaImpar += (dobleImpar % 10) + (dobleImpar / 10);
                }
                // Sumo las cifras pares e impares. 
                // 
                Int32 sumaTotal = sumaPar + sumaImpar;
                // Me quedo con la cifra de las unidades y se la resto a 10, siempre 
                // y cuando la cifra de las unidades sea distinta de cero 
                // 
                sumaTotal = (10 - (sumaTotal % 10)) % 10;
                // Devuelvo el Dígito de Control dependiendo del primer carácter 
                // del NIF pasado a la función. 
                //
                string digitoControl = "";
                switch (firstChar)
                {
                    case "N":
                    case "P":
                    case "Q":
                    case "R":
                    case "S":
                    case "W":
                        // NIF de entidades cuyo dígito de control se corresponde 
                        // con una letra. 
                        // 
                        // Al estar los índices de los arrays en base cero, el primer 
                        // elemento del array se corresponderá con la unidad del número 
                        // 10, es decir, el número cero. 
                        // 
                        char[] characters = { 'J', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I' };
                        digitoControl = characters[sumaTotal].ToString();
                        break;
                    default:
                        // NIF de las restantes entidades, cuyo dígito de control es un número. 
                        // 
                        digitoControl = sumaTotal.ToString();
                        break;
                }
                return digitoControl.Equals(cif.Substring(8, 1));
            }
            catch (Exception)
            {
                // Cualquier excepción producida, devolverá false. 
                return false;
            }
        }

        //ACTUALIZADO (porque el cif ya no usa)
        //Valida el NIF de Personas Físicas y de Personas Jurídicas 
        private static bool validarNifActualizado(string dato)
        {
            bool esNif = false;
            try
            {
                //Si el primer caracter empieza por una letra puede que sea un Nif de Personas Juridicas (un cif antiguo)
                //y sino puede que sea un NIF personas fisicas (Solo se valida un DNI)
                char primerCaracter = dato[0];
                if (Char.IsLetter(primerCaracter))
                {
                    esNif = validarNIFdePersonasJuridicas(dato);
                }
                else if (!Char.IsLetter(primerCaracter))
                {
                    esNif = ValidarDNI(dato);
                }


                //esNif = ValidaNIF(dato);
            }
            catch (Exception e)
            {
                esNif = false;
            }


            return esNif;
        }

    }
}