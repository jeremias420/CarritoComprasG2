using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


using ClaseEntidades;
using ClaseBDNegocio;
using System.IO;

namespace CarritowebG2.Controllers
{
    public class TiendaController : Controller
    {
        // GET: Tienda
        public ActionResult Index()
        {
            return View();
        }
         public ActionResult detalleProducto (int prod_ID)
        {

            Producto oProducto = new Producto();
            bool conversion;

            oProducto = new CN_Producto().Listar().Where(p => p.prod_ID == prod_ID).FirstOrDefault();

            if (oProducto !=null)
            {
                oProducto.prod_Base64 = CN_Recursos.ConvertirBase64(Path.Combine(oProducto.prod_RutaImagen, oProducto.prod_NombreImagen),out conversion);
                oProducto.prod_Extension = Path.GetExtension(oProducto.prod_NombreImagen);

            }
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