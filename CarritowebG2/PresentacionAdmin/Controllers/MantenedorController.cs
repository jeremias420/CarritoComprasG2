using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClaseEntidades;
using ClaseBDNegocio;
using System.IO;
namespace PresentacionAdmin.Controllers
{
    [Authorize]
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

            if (Objeto.cate_ID == 0) {

                resultado = new CN_Categoria().Registrar(Objeto, out Mensaje);
            }
            else {
                resultado = new CN_Categoria().Editar(Objeto, out Mensaje);
            }


            return Json(new { resultado = resultado, Mensaje = Mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarCategoria(int Cate_ID)
        {
            bool respuesta = false;
            string Mensaje = string.Empty;

            respuesta = new CN_Categoria().Eliminar(Cate_ID, out Mensaje);

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

                resultado = new CN_Marca().Registrar(Objeto, out Mensaje);
            }
            else
            {
                resultado = new CN_Marca().Editar(Objeto, out Mensaje);
            }


            return Json(new { resultado = resultado, Mensaje = Mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarMarca(int marc_ID)
        {
            bool respuesta = false;
            string Mensaje = string.Empty;

            respuesta = new CN_Marca().Eliminar(marc_ID, out Mensaje);

            return Json(new { resultado = respuesta, Mensaje = Mensaje }, JsonRequestBehavior.AllowGet);

        }
        #endregion
        
        // PRODUCTO

        #region Producto
        [HttpGet]
        public JsonResult ListarProducto()
        {
            List<Producto> objLista = new List<Producto>();

            objLista = new CN_Producto().Listar();

            return Json(new { data = objLista }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GuardarProducto(Producto Objeto)
        {
            object resultado;
            string Mensaje = string.Empty;

            if (Objeto.prod_ID == 0)
            {

                resultado = new CN_Producto().Registrar(Objeto, out Mensaje);
            }
            else
            {
                resultado = new CN_Producto().Editar(Objeto, out Mensaje);
            }


            return Json(new { resultado = resultado, Mensaje = Mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarProducto(int prod_ID)
        {
            bool respuesta = false;
            string Mensaje = string.Empty;

            respuesta = new CN_Producto().Eliminar(prod_ID, out Mensaje);

            return Json(new { resultado = respuesta, Mensaje = Mensaje }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult ImagenProducto(int prod_ID)
        {

            bool conversion = false;
            Producto oProducto = new CN_Producto().Listar().Where(p => p.prod_ID == prod_ID).FirstOrDefault();
            
            string textoBase64 = CN_Recursos.ConvertirBase64(Path.Combine(oProducto.prod_RutaImagen, oProducto.prod_NombreImagen), out conversion);


            return Json(new
            {

                conversion = conversion,
                textoBase64 = textoBase64,
                extension = Path.GetExtension(oProducto.prod_Nombre)


            },
            JsonRequestBehavior.AllowGet
            );
        }

    }
    #endregion
}