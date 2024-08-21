using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClaseEntidades;
using ClaseBDNegocio;

namespace PresentacionAdmin.Controllers
{
    public class MantenedorController : Controller
    {
        public ActionResult Categoria()
        {
            return View();
        }
        public ActionResult Marca()
        {
            return View();
        }
        public ActionResult Producto()
        {
            return View();
        }

        // CATEGORIA

        #region CATEGORIA

        [HttpGet]
        public JsonResult ListarCategoria()
        {
            List<Categoria> objLista = new List<Categoria>();

            objLista = new CN_Categoria().Listar();

            return Json(new { data = objLista }, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GuardarCategoria(Categoria Objeto)
        {
            object resultado;
            string Mensaje = string.Empty;

            if (Objeto.cate_ID == 0){

                resultado = new CN_Categoria().Registrar(Objeto, out Mensaje);
            }
            else{
                resultado = new CN_Categoria().Editar(Objeto, out Mensaje);
            }


            return Json(new { resultado = resultado, Mensaje = Mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarCategoria(int id)
        {
            bool respuesta = false;
            string Mensaje = string.Empty;

            respuesta = new CN_Categoria().Eliminar(id, out Mensaje);

            return Json(new { resultado = respuesta, Mensaje = Mensaje }, JsonRequestBehavior.AllowGet);

        }

        #endregion

        // MARCA

        #region MARCA

        [HttpGet]
        public JsonResult ListarMarca()
        {
            List<Marca> objLista = new List<Marca>();

            objLista = new CN_Marca().Listar();

            return Json(new { data = objLista }, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GuardarMarca(Marca Objeto)
        {
            object resultado;
            string Mensaje = string.Empty;

            if (Objeto.marc_ID == 0)
            {

                resultado = new  CN_Marca().Registrar(Objeto, out Mensaje);
            }
            else
            {
                resultado = new CN_Marca().Editar(Objeto, out Mensaje);
            }


            return Json(new { resultado = resultado, Mensaje = Mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarMarca(int id)
        {
            bool respuesta = false;
            string Mensaje = string.Empty;

            respuesta = new CN_Marca().Eliminar(id, out Mensaje);

            return Json(new { resultado = respuesta, Mensaje = Mensaje }, JsonRequestBehavior.AllowGet);

        }
        #endregion
    }
}