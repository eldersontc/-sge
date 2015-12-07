using SGE.AccesoDatos.Base;
using SGE.Entidades.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.AccesoDatos.Facturacion
{
    public class daGuiaRemisionItem : daBase<GuiaRemisionItem>
    {
        public void EliminarPorIdGuiaRemision(int idGuiaRemision)
        {
            string sql = string.Format("DELETE Facturacion.GuiaRemisionItem WHERE idGuiaRemision = {0}", idGuiaRemision);
            Ejecutar(sql);
        }
    }
}
