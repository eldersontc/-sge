using SGE.AccesoDatos.Base;
using SGE.Entidades.Ventas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.AccesoDatos.Ventas
{
    public class daLpMaquinaEscala : daBase<LpMaquinaEscala>
    {
        public void EliminarPorIdLpMaquinaItem(int idLpMaquinaItem)
        {
            string sql = string.Format("DELETE Ventas.LpMaquinaEscala WHERE idLpMaquinaItem = {0}", idLpMaquinaItem);
            Ejecutar(sql);
        }
    }
}
