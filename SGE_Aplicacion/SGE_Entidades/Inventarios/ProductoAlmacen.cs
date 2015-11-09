using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.Inventarios
{
    public class ProductoAlmacen
    {
        public virtual int idProductoAlmacen { get; set; }
        public virtual int idProducto { get; set; }
        public virtual Almacen almacen { get; set; }
        public virtual int stockFisico { get; set; }
        public virtual int stockComprometido { get; set; }
        public virtual int stockSolicitado { get; set; }
        public virtual int stockDisponible { get; set; }
    }
}
