using SGE.AccesoDatos.Base;
using SGE.Entidades.Inventarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.AccesoDatos.Inventarios
{
    public class daIngInventarioItem : daBase<IngInventarioItem>
    {
        public void EliminarPorIdIngInventario(int idIngInventario)
        {
            string sql = string.Format("DELETE Inventarios.IngInventarioItem WHERE idIngInventario = {0}", idIngInventario);
            Ejecutar(sql);
        }
    }
}
