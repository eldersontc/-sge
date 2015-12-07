using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.Ventas
{
    public class MetodoImpresion
    {
        public virtual int idMetodoImpresion { get; set; }
        public virtual string descripcion { get; set; }
        public virtual int fcPases { get; set; }
        public virtual int fcCambios { get; set; }
        public virtual int fcX { get; set; }
        public virtual int fcY { get; set; }
        public virtual string letras { get; set; }
        public virtual bool activo { get; set; }
    }
}
