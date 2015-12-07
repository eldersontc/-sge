using SGE.AccesoDatos.Base;
using SGE.Entidades.Ventas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.AccesoDatos.Ventas
{
    public class daPresupuestoItem : daBase<PresupuestoItem>
    {
        public void EliminarPorIdPresupuesto(int idPresupuesto)
        {
            string sql = string.Format("DELETE Ventas.PresupuestoItem WHERE idPresupuesto = {0}", idPresupuesto);
            Ejecutar(sql);
        }
    }
}
