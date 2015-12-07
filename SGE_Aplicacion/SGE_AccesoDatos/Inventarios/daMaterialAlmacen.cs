using SGE.AccesoDatos.Base;
using SGE.Entidades.Inventarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.AccesoDatos.Inventarios
{
    public class daMaterialAlmacen : daBase<MaterialAlmacen>
    {
        public void EliminarPorIdMaterial(int idMaterial)
        {
            string sql = string.Format("DELETE Inventarios.MaterialAlmacen WHERE idMaterial = {0}", idMaterial);
            Ejecutar(sql);
        }
    }
}
