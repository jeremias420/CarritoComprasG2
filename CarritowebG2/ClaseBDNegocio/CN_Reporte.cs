using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClaseEntidades;
using ClaseDeDatos;

namespace ClaseBDNegocio
{
    public class CN_Reporte
    {

        private CD_Reporte objClaseDatos = new CD_Reporte();

        public List<Reporte> Compra(string FechaInicio, string FechaFin, string IDTransaccion)
        {
            return objClaseDatos.Compra(FechaInicio, FechaFin, IDTransaccion);
        }

        public DashBoard VerDashBoard()
        {
            return objClaseDatos.VerDashBoard();
        }
    }
}
