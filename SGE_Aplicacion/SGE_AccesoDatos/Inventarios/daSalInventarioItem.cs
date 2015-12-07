using SGE.AccesoDatos.Base;
using SGE.Entidades.Inventarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.AccesoDatos.Inventarios
{
    public class daSalInventarioItem : daBase<SalInventarioItem>
    {
        public void EliminarPorIdSalInventario(int idSalInventario)
        {
            string sql = string.Format("DELETE Inventarios.SalInventarioItem WHERE idSalInventario = {0}", idSalInventario);
            Ejecutar(sql);
        }
    }
}
