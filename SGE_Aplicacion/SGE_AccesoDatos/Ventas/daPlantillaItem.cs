using SGE.AccesoDatos.Base;
using SGE.Entidades.Ventas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.AccesoDatos.Ventas
{
    public class daPlantillaItem : daBase<PlantillaItem>
    {
        public void EliminarPorIdPlantillaGrupo(int idPlantillaGrupo)
        {
            string sql = string.Format("DELETE Ventas.PlantillaItem WHERE idPlantillaGrupo = {0}", idPlantillaGrupo);
            Ejecutar(sql);
        }
    }
}
