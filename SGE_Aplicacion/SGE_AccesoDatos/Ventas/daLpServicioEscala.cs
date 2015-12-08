using SGE.AccesoDatos.Base;
using SGE.Entidades.Ventas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.AccesoDatos.Ventas
{
    public class daLpServicioEscala : daBase<LpServicioEscala>
    {
        public void EliminarPorIdLpServicioUnidad(int idLpServicioUnidad)
        {
            string sql = string.Format("DELETE Ventas.LpServicioEscala WHERE idLpServicioUnidad = {0}", idLpServicioUnidad);
            Ejecutar(sql);
        }
    }
}
