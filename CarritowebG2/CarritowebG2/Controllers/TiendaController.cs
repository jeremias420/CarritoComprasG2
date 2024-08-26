using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


using ClaseEntidades;
using ClaseBDNegocio;

namespace CarritowebG2.Controllers
{
    public class TiendaController : Controller
    {
        // GET: Tienda
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public JsonResult ListaCategorias(){
            List<Categoria> lista = new List<Categoria>();
            lista = new CN_Categoria().Listar();
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }


    }
}