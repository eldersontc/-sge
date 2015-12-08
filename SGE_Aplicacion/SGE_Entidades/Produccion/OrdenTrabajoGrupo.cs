using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.Produccion
{
    public class OrdenTrabajoGrupo
    {
        public virtual int idOrdenTrabajoGrupo { get; set; }
        public virtual int idOrdenTrabajo { get; set; }
        public virtual string titulo { get; set; }
        public virtual int cantidad { get; set; }
        public virtual List<OrdenTrabajoItem> items { get; set; }
        public virtual List<int> idsItems { get; set; }
    }
}
