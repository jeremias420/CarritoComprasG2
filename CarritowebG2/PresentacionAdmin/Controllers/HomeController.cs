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
            string mensaje = string.Empty;

            if (Objeto.usua_ID == 0)
            {

                resultado = new CN_Usuarios().Registrar(Objeto, out mensaje);
            }
            else
            {
                resultado = new CN_Usuarios().Editar(Objeto, out mensaje);

            }
            return Json(new { resultado = resultado, mensaje = mensaje}, JsonRequestBehavior.AllowGet);
        }

    }
}