using SGE.AccesoDatos.Base;
using SGE.Entidades.Inventarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.AccesoDatos.Inventarios
{
    public class daProductoAlmacen : daBase<ProductoAlmacen>
    {
        public void EliminarPorIdProducto(int idProducto)
        {
            string sql = string.Format("DELETE Inventarios.ProductoAlmacen WHERE idProducto = {0}", idProducto);
            Ejecutar(sql);
        }
    }
}
