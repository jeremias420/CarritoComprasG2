using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClaseEntidades;
using ClaseBDNegocio;


namespace PresentacionAdmin.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Usuarios()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ListarUsuario()
        {
            List<Usuario> objLista = new List<Usuario>();

            objLista = new CN_Usuarios().Listar();

            return Json(new {data = objLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarUsuarios(Usuario Objeto)
        {
            object resultado;
            string Mensaje = string.Empty;

            if (Objeto.usua_ID == 0)
            {

                resultado = new CN_Usuarios().Registrar(Objeto, out Mensaje);
            }
            else
            {
                resultado = new CN_Usuarios().Editar(Objeto, out Mensaje);

            }

            return Json(new { resultado = resultado, Mensaje = Mensaje}, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult EliminarUsuario(int id)
        {
            bool respuesta = false;
            string Mensaje = string.Empty;

            respuesta = new CN_Usuarios().Eliminar(id, out Mensaje);

            return Json(new { resultado = respuesta, Mensaje = Mensaje }, JsonRequestBehavior.AllowGet);

        }

    }
}