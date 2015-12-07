using SGE.AccesoDatos.Base;
using SGE.Entidades.Produccion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.AccesoDatos.Produccion
{
    public class daOrdenProduccionItem : daBase<OrdenProduccionItem>
    {
        public void EliminarPorIdOrdenProduccion(int idOrdenProduccion)
        {
            string sql = string.Format("DELETE Produccion.OrdenProduccionItem WHERE idOrdenProduccion = {0}", idOrdenProduccion);
            Ejecutar(sql);
        }
    }
}
