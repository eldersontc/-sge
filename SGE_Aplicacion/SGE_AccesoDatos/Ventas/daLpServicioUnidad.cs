using SGE.AccesoDatos.Base;
using SGE.Entidades.Ventas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.AccesoDatos.Ventas
{
    public class daLpServicioUnidad : daBase<LpServicioUnidad>
    {
        public void EliminarPorIdLpServicioItem(int idLpServicioItem)
        {
            string sql = string.Format("DELETE Ventas.LpServicioUnidad WHERE idLpServicioItem = {0}", idLpServicioItem);
            Ejecutar(sql);
        }
    }
}
