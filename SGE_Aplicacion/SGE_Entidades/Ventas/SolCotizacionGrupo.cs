using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.Ventas
{
    public class SolCotizacionGrupo
    {
        public virtual int idSolCotizacionGrupo { get; set; }
        public virtual int idSolCotizacion { get; set; }
        public virtual string titulo { get; set; }
        public virtual int cantidad { get; set; }
        public virtual List<SolCotizacionItem> items { get; set; }
    }
}
