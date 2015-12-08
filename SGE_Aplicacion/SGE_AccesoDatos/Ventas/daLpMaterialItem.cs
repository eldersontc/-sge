using SGE.AccesoDatos.Base;
using SGE.Entidades.Ventas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.AccesoDatos.Ventas
{
    public class daLpMaterialItem : daBase<LpMaterialItem>
    {
        public void EliminarPorIdLpMaterial(int idLpMaterial)
        {
            string sql = string.Format("DELETE Ventas.LpMaterialItem WHERE idLpMaterial = {0}", idLpMaterial);
            Ejecutar(sql);
        }
    }
}
