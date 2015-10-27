using SGE.AccesoDatos.Base;
using SGE.Entidades.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.AccesoDatos.Administracion
{
    public class daItemReporte : daBase<ItemReporte>
    {
        public void EliminarPorIdReporte(int idReporte) {
            string sql = string.Format("DELETE Administracion.ItemReporte WHERE idReporte = {0}", idReporte);
            Ejecutar(sql);
        }
    }
}
