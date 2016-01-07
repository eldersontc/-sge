using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.Administracion
{
    public class Paginacion
    {
        public virtual int pagActual { get; set; }
        public virtual int nroRegistros { get; set; }
    }
}
