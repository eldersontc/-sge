using SGE.AccesoDatos.Base;
using SGE.Entidades.Inventarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.AccesoDatos.Inventarios
{
    public class daProductoUnidad : daBase<ProductoUnidad>
    {
        public void EliminarPorIdProducto(int idProducto)
        {
            string sql = string.Format("DELETE Inventarios.ProductoUnidad WHERE idProducto = {0}", idProducto);
            Ejecutar(sql);
        }
    }
}
