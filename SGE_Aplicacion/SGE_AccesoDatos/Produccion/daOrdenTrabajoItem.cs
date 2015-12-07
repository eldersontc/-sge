using SGE.AccesoDatos.Base;
using SGE.Entidades.Produccion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.AccesoDatos.Produccion
{
    public class daOrdenTrabajoItem : daBase<OrdenTrabajoItem>
    {
        public void EliminarPorIdOrdenTrabajo(int idOrdenTrabajo)
        {
            string sql = string.Format("DELETE Produccion.OrdenTrabajoItem WHERE idOrdenTrabajo = {0}", idOrdenTrabajo);
            Ejecutar(sql);
        }
    }
}
