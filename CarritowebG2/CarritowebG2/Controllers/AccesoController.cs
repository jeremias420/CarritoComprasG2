using ClaseEntidades;
using ClaseBDNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CarritowebG2.Controllers
{
    public class AccesoController : Controller
    {
        // GET: Acceso
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Registrar()
        {
            return View();
        }

        public ActionResult Reestablcer()
        {
            return View();
        }
        public ActionResult CambiarClave()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registrar(Cliente objeto)
        {
            int resultado;
            string mensaje = string.Empty;
            ViewData["Nombre"] = string.IsNullOrEmpty(objeto.clie_Nombre) ? "" : objeto.clie_Nombre;
            ViewData["Apellido"] = string.IsNullOrEmpty(objeto.clie_Apellido) ? "" : objeto.clie_Apellido;
            ViewData["Correo"] = string.IsNullOrEmpty(objeto.clie_Correo) ? "" : objeto.clie_Correo;

            if (objeto.clie_Clave != objeto.clie_Confirmar_Clave)
            {
                ViewBag.Error = "Las contraseñas no coinciden";
                return View();

            }
            resultado = new CN_Cliente().Registrar(objeto, out mensaje);
            if (resultado > 0)
            {
                ViewBag.Error = null;
                return RedirectToAction("Index", "Acceso");
            }
            else
            {
                ViewBag.Error = mensaje;
                return View();
            }
        }


        [HttpPost]
        public ActionResult Index(string correo, string clave)
        {
            Cliente oCliente = null;
            oCliente = new CN_Cliente().Listar().Where(item => item.clie_Correo == correo && item.clie_Clave == CN_Recursos.ConvertirSha256(clave)).FirstOrDefault();

            if (oCliente == null)
            {
                ViewBag.Error = "Correo o contraseña incorrecta";
                return View();
            }
            else
            {
                if (oCliente.clie_Reestablecer)
                {
                    TempData["clie_ID"] = oCliente.clie_ID;
                    return RedirectToAction("CambiarClave", "Acceso");
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(oCliente.clie_Correo, false);
                    Session["Cliente"] = oCliente;
                    ViewBag.Error = null;
                    return RedirectToAction("Index", "Tienda");
                }
            }
        }


        [HttpPost]
        public ActionResult Reestablecer(string clie_Correo)
        {
            Cliente oCliente = new Cliente();
            oCliente = new CN_Cliente().Listar().Where(item => item.clie_Correo == clie_Correo).FirstOrDefault();

            if (oCliente == null)
            {
                ViewBag.Error = "No se encontro un Cliente con ese correo";
                return View();
            }
            string mensaje = string.Empty;
            bool respuesta = new CN_Cliente().ReestablecerClave(oCliente.clie_ID, clie_Correo, out mensaje);

            if (respuesta)
            {
                ViewBag.Error = null;
                return RedirectToAction("Index", "Acceso");

            }
            else
            {
                ViewBag.Error = mensaje;
                return View();
            }

        }
        [HttpPost]
        public ActionResult CambiarClave(string clie_ID, string claveActual, string nuevaClave, string confirmarClave)
        {
            Cliente oCliente = new Cliente();

            oCliente = new CN_Cliente().Listar().Where(u => u.clie_ID == int.Parse(clie_ID)).FirstOrDefault();

            if (oCliente.clie_Clave != CN_Recursos.ConvertirSha256(claveActual))
            {
                TempData["clie_ID"] = clie_ID;
                ViewData["vclave"] = "";
                ViewBag.Error = "La contraseña actual no es correcta";
                return View();
            }
            else if (nuevaClave != confirmarClave)
            {
                TempData["clie_ID"] = clie_ID;
                ViewData["vclave"] = claveActual;
                ViewBag.Error = "Las contraseñas deben ser iguales";
                return View();
            }
            ViewData["vclave"] = "";
            nuevaClave = CN_Recursos.ConvertirSha256(nuevaClave);
            string mensaje = string.Empty;
            bool respuesta = new CN_Cliente().CambiarClave(int.Parse(clie_ID), nuevaClave, out mensaje);
            if (respuesta)
            {
                return RedirectToAction("Index");
            }
            else
            {
                TempData["clie_ID"] = clie_ID;
                ViewBag.Error = mensaje;
                return View();
            }
        }

        public ActionResult CerrarSesion()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Acceso");
        }

    }
}