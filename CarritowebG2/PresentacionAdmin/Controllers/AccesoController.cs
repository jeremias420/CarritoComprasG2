using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClaseEntidades;
using ClaseBDNegocio;

namespace PresentacionAdmin.Controllers
{
    public class AccesoController : Controller
    {
        // GET: Acceso
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CambiarClave()
        {
            return View();
        }
        public ActionResult Reestablecer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string correo, string clave)
        {
            Usuario oUsuario = new Usuario();
            oUsuario = new CN_Usuarios().Listar().Where(u => u.usua_Correo == correo && u.usua_Clave == CN_Recursos.ConvertirSha256(clave)).FirstOrDefault();

            if (oUsuario == null)
            {
                ViewBag.Error = "Correo o contraseña incorrecta";
                return View();
            }
            else
            {
                if(oUsuario.usua_Reestablecer){
                    //TempData nos permite guardar una variable para distintas vistas
                    TempData["usua_ID"] = oUsuario.usua_ID;
                    return RedirectToAction("CambiarClave");
                }
                ViewBag.Error = null;
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult CambiarClave(string usua_ID, string claveActual, string nuevaClave, string confirmarClave)
        {
            Usuario oUsuario = new Usuario();

            oUsuario = new CN_Usuarios().Listar().Where(u => u.usua_ID == int.Parse(usua_ID)).FirstOrDefault();

            if(oUsuario.usua_Clave != CN_Recursos.ConvertirSha256(claveActual))
            {
                TempData["usua_ID"] = usua_ID;
                ViewBag.Error = "La contraseña actual no es correcta";
                return View();
            }
            else if (nuevaClave != confirmarClave)
            {
                TempData["usua_ID"] = usua_ID;
                ViewBag.Error = "Las contraseñas deben ser iguales";
                return View();
            }

            return View();
        }
    }
}