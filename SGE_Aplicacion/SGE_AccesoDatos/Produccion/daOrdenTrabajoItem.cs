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
        public void EliminarPorIdOrdenTrabajoGrupo(int idOrdenTrabajoGrupo)
        {
            string sql = string.Format("DELETE Produccion.OrdenTrabajoItem WHERE idOrdenTrabajoGrupo = {0}", idOrdenTrabajoGrupo);
            Ejecutar(sql);
        }
    }
}
