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
        public void EliminarPorIdCotizacionItem(int idCotizacionItem)
        {
            string sql = string.Format("DELETE Ventas.CotizacionServicio WHERE idCotizacionItem = {0}", idCotizacionItem);
            Ejecutar(sql);
        }
    }
}
