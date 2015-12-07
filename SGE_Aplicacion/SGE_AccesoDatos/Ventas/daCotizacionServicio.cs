using SGE.AccesoDatos.Base;
using SGE.Entidades.Ventas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.AccesoDatos.Ventas
{
    public class daCotizacionServicio : daBase<CotizacionServicio>
    {
        public void EliminarPorIdCotizacion(int idCotizacion)
        {
            string sql = string.Format("DELETE Ventas.CotizacionServicio WHERE idCotizacion = {0}", idCotizacion);
            Ejecutar(sql);
        }
    }
}
