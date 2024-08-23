using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClaseEntidades;
using System.Data.SqlClient;
using System.Data;

namespace ClaseDeDatos
{
    public class CD_Reporte
    {
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
