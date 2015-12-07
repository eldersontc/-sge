using SGE.AccesoDatos.Base;
using SGE.Entidades.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.AccesoDatos.Facturacion
{
    public class daFacturaItem : daBase<FacturaItem>
    {
        public void EliminarPorIdFactura(int idFactura)
        {
            string sql = string.Format("DELETE Facturacion.FacturaItem WHERE idFactura = {0}", idFactura);
            Ejecutar(sql);
        }
    }
}
