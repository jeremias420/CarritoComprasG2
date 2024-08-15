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
    }
}