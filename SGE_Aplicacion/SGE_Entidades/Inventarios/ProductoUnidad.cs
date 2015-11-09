using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.Inventarios
{
    public class ProductoUnidad
    {
        public virtual int idProductoUnidad { get; set; }
        public virtual int idProducto { get; set; }
        public virtual Unidad unidad { get; set; }
        public virtual int factor { get; set; }
    }
}
