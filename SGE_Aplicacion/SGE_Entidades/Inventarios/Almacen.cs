using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.Inventarios
{
    public class Almacen
    {
        public virtual int idAlmacen { get; set; }
        public virtual string codigo { get; set; }
        public virtual string descripcion { get; set; }
        public virtual bool activo { get; set; }
    }
}
