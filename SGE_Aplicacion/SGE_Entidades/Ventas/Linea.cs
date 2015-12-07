using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.Ventas
{
    public class Linea
    {
        public virtual int idLinea { get; set; }
        public virtual string descripcion { get; set; }
        public virtual bool activo { get; set; }
    }
}
