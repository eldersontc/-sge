using SGE.AccesoDatos.Base;
using SGE.Entidades.Ventas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.AccesoDatos.Ventas
{
    public class daLpMaterialUnidad : daBase<LpMaterialUnidad>
    {
        public void EliminarPorIdLpMaterialItem(int idLpMaterialItem)
        {
            string sql = string.Format("DELETE Ventas.LpMaterialUnidad WHERE idLpMaterialItem = {0}", idLpMaterialItem);
            Ejecutar(sql);
        }
    }
}
