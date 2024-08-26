using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClaseEntidades;
using ClaseBDNegocio;
using System.IO;
using Newtonsoft.Json;
using System.Globalization;
using System.Configuration;

namespace PresentacionAdmin.Controllers
{
    [Authorize]
    public class MantenedorController : Controller
    {
        private object configurationManager;

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


        public JsonResult GuardarProducto(string Objeto, HttpPostedFileBase archivoImagen)
        {
            object resultado;
            string Mensaje = string.Empty;
            bool operacionExitosa = true;
            bool GuardarImagenExito = true;

            Producto oProducto = new Producto();
            oProducto = JsonConvert.DeserializeObject<Producto>(Objeto);
            decimal Precio;

            if (decimal.TryParse(oProducto.prod_precioTexto,System.Globalization.NumberStyles.AllowDecimalPoint,new CultureInfo("es-AR"), out Precio))
            {

                oProducto.prod_Precio = Precio;

            }
            else
            {

                return Json(new { operacionExitosa = false, Mensaje = "El formato del precio debe ser ##.##" }, JsonRequestBehavior.AllowGet);

            }
            if (oProducto.prod_ID == 0)
            {

                resultado = new CN_Producto().Registrar(oProducto, out Mensaje);
            }
            else
            {
                resultado = new CN_Producto().Editar(oProducto, out Mensaje);
            }

            if (oProducto.prod_ID == 0)
            {
                int idProductoGenerado = new CN_Producto().Registrar(oProducto, out Mensaje);

                if (idProductoGenerado != 0)
                {

                    oProducto.prod_ID = idProductoGenerado;

                }
                else
                {
                    operacionExitosa = false;
                }
            }
            else
            {

                operacionExitosa = new CN_Producto().Editar(oProducto, out Mensaje);

            }
            if (operacionExitosa == true)
            {

                if (archivoImagen != null)
                {

                    string ruta_guardar = ConfigurationManager.AppSettings["ServidorFotos"];
                    string prod_extension = Path.GetExtension(archivoImagen.FileName);
                    string nombre_imagen = string.Concat(oProducto.prod_ID.ToString(),prod_extension);


                    try
                    {

                        archivoImagen.SaveAs(Path.Combine(ruta_guardar, nombre_imagen));

                    }
                    catch(Exception ex)
                    {

                        string msg = ex.Message;
                        GuardarImagenExito = false;

                    }

                    if (GuardarImagenExito == true)
                    {

                        oProducto.prod_RutaImagen = ruta_guardar;
                        oProducto.prod_NombreImagen = nombre_imagen;
                        bool rspta = new CN_Producto().GuardarDatosImagen(oProducto, out Mensaje);

                    }
                    else
                    {
                        Mensaje = "Se guardo el producto pero hubo un error con la imagen";

                    }


                }

            }

            return Json(new { prod_operacionExitosa = operacionExitosa, idGenerado = oProducto.prod_ID, Mensaje = Mensaje }, JsonRequestBehavior.AllowGet);
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