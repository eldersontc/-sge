using SGE.AccesoDatos.Base;
using SGE.Entidades.Ventas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.AccesoDatos.Ventas
{
    public class daCotizacionItem : daBase<CotizacionItem>
    {
        public void EliminarPorIdCotizacionGrupo(int idCotizacionGrupo)
        {
            string sql = string.Format("DELETE Ventas.CotizacionItem WHERE idCotizacionGrupo = {0}", idCotizacionGrupo);
            Ejecutar(sql);
        }
    }
}
