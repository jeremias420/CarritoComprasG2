using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClaseEntidades;
using ClaseBDNegocio;
using System.Data;
using ClosedXML.Excel;
using System.IO;

namespace PresentacionAdmin.Controllers
{
    [Authorize]
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

            return Json(new { data = objLista }, JsonRequestBehavior.AllowGet);
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

            return Json(new { resultado = resultado, Mensaje = Mensaje }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult EliminarUsuario(int usua_ID)
        {
            bool respuesta = false;
            string Mensaje = string.Empty;

            respuesta = new CN_Usuarios().Eliminar(usua_ID, out Mensaje);

            return Json(new { resultado = respuesta, Mensaje = Mensaje }, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public JsonResult VistaDashboard()
        {
            DashBoard objeto = new CN_Reporte().VerDashBoard();
            return Json(new { resultado = objeto }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult ListaReporte(string FechaInicio, string FechaFin, string IDTransaccion)
        {

            List<Reporte> oLista = new List<Reporte>();

            oLista = new CN_Reporte().Compra(FechaInicio, FechaFin, IDTransaccion);
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public FileResult ExportarVenta(string FechaInicio, string FechaFin, string IDTransaccion)
        {

            List<Reporte> oLista = new List<Reporte>();
            oLista = new CN_Reporte().Compra(FechaInicio, FechaFin, IDTransaccion);

            DataTable dt = new DataTable();

            dt.Locale = new System.Globalization.CultureInfo("es-PE");
            dt.Columns.Add("Fecha Venta", typeof(string));
            dt.Columns.Add("Cliente", typeof(string));
            dt.Columns.Add("Producto", typeof(string));
            dt.Columns.Add("Precio", typeof(decimal));
            dt.Columns.Add("Cantidad", typeof(int));
            dt.Columns.Add("Total", typeof(decimal));
            dt.Columns.Add("IdTransaccio", typeof(string));

            foreach(Reporte rp in oLista)
            {

                dt.Rows.Add(new object[]
                {

                    rp.FechaVenta,
                    rp.Cliente,
                    rp.Precio,
                    rp.Cantidad,
                    rp.Producto,
                    rp.Total,
                    rp.IDTransaccion

                });

            }

            dt.TableName = "Datos";


            using(XLWorkbook wb = new XLWorkbook())
            {

                wb.Worksheets.Add(dt);
                using(MemoryStream stream = new MemoryStream())
                {

                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ReporteVenta" + DateTime.Now.ToString() + ".xlsx");

                }

            }


        }


    }
}