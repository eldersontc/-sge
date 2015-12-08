using SGE.AccesoDatos.Base;
using SGE.Entidades.Ventas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.AccesoDatos.Ventas
{
    public class daLpMaterialEscala : daBase<LpMaterialEscala>
    {
        public void EliminarPorIdLpMaterialUnidad(int idLpMaterialUnidad)
        {
            string sql = string.Format("DELETE Ventas.LpMaterialEscala WHERE idLpMaterialUnidad = {0}", idLpMaterialUnidad);
            Ejecutar(sql);
        }
    }
}
