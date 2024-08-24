using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClaseEntidades;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace ClaseDeDatos
{
    public class CD_Reporte
    {

        public List<Reporte> Compra(string FechaInicio, string FechaFin, string IDTransaccion)
        {
            List<Reporte> lista = new List<Reporte>();
            try
            {
                using (SqlConnection Oconexion = new SqlConnection(Conexion.CN))
                {

                    SqlCommand cmd = new SqlCommand("sp_ReporteVentas", Oconexion);
                    cmd.Parameters.AddWithValue("FechaInicio", FechaInicio);
                    cmd.Parameters.AddWithValue("FechaFin", FechaFin);
                    cmd.Parameters.AddWithValue("IDTransaccion", IDTransaccion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    Oconexion.Open();

                    using (SqlDataReader DR = cmd.ExecuteReader())
                    {
                        while (DR.Read())
                        {
                            lista.Add(new Reporte()
                            {
                                FechaVenta = DR["FechaVenta"].ToString(),
                                Cliente = DR["Cliente"].ToString(),
                                Producto = DR["Producto"].ToString(),
                                Precio = Convert.ToDecimal(DR["Precio"], new CultureInfo("es-PE")),
                                Cantidad = Convert.ToInt32 (DR["Cantidad"].ToString()),
                                Total = Convert.ToDecimal(DR["Total"], new CultureInfo("es-PE")),
                                IDTransaccion = DR["IDTransaccion"].ToString()
                            });
                        }
                    }
                }

            }
            catch
            {
                lista = new List<Reporte>();
            }

            return lista;
        }


        public DashBoard VerDashBoard()
        {

            DashBoard objeto = new DashBoard();
            try
            {
                using (SqlConnection Oconexion = new SqlConnection(Conexion.CN))
                {
                    SqlCommand cmd = new SqlCommand("sp_ReporteDashboard", Oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    Oconexion.Open();

                    using (SqlDataReader DR = cmd.ExecuteReader())
                    {
                        while (DR.Read())
                        {
                            objeto = new DashBoard()
                            {
                                TotalCliente = Convert.ToInt32(DR["TotalCliente"]),
                                TotalVenta = Convert.ToInt32(DR["TotalVenta"]),
                                TotalProducto = Convert.ToInt32(DR["TotalProducto"]),
                            };
                        }
                    }
                }

            }
            catch
            {
                objeto = new DashBoard();
            }

            return objeto;
        }
    }
}
