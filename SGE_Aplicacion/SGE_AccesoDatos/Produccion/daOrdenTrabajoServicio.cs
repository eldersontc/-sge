using SGE.AccesoDatos.Base;
using SGE.Entidades.Produccion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.AccesoDatos.Produccion
{
    public class daOrdenTrabajoServicio : daBase<OrdenTrabajoServicio>
    {
        public void EliminarPorIdOrdenTrabajoItem(int idOrdenTrabajoItem)
        {
            string sql = string.Format("DELETE Produccion.OrdenTrabajoServicio WHERE idOrdenTrabajoItem = {0}", idOrdenTrabajoItem);
            Ejecutar(sql);
        }
    }
}
