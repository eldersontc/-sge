using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.Administracion
{
    public class Numeracion
    {
        public virtual int idNumeracion { get; set; }
        public virtual string descripcion { get; set; }
        public virtual int documento { get; set; }
        public virtual bool automatico { get; set; }
        public virtual string serie { get; set; }
        public virtual int? numeroActual { get; set; }
        public virtual int? longitudNumero { get; set; }
        public virtual bool impuesto { get; set; }
        public virtual decimal? porcentajeImpuesto { get; set; }
        public virtual bool activo { get; set; }
    }
}
