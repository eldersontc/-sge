using SGE.AccesoDatos.Base;
using SGE.Entidades.Ventas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.AccesoDatos.Ventas
{
    public class daLpServicioItem : daBase<LpServicioItem>
    {
        public void EliminarPorIdLpServicio(int idLpServicio)
        {
            string sql = string.Format("DELETE Ventas.LpServicioItem WHERE idLpServicio = {0}", idLpServicio);
            Ejecutar(sql);
        }
    }
}
