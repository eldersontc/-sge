using SGE.AccesoDatos.Base;
using SGE.Entidades.Ventas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.AccesoDatos.Ventas
{
    public class daLpMaquinaItem : daBase<LpMaquinaItem>
    {
        public void EliminarPorIdLpMaquina(int idLpMaquina)
        {
            string sql = string.Format("DELETE Ventas.LpMaquinaItem WHERE idLpMaquina = {0}", idLpMaquina);
            Ejecutar(sql);
        }
    }
}
