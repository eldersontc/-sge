using SGE.AccesoDatos.Base;
using SGE.Entidades.Ventas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.AccesoDatos.Ventas
{
    public class daPlantillaGrupo : daBase<PlantillaGrupo>
    {
        public void EliminarPorIdPlantilla(int idPlantilla)
        {
            string sql = string.Format("DELETE Ventas.PlantillaGrupo WHERE idPlantilla = {0}", idPlantilla);
            Ejecutar(sql);
        }
    }
}
