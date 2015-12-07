using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.Ventas
{
    public class FormaPago
    {
        public virtual int idFormaPago { get; set; }
        public virtual string descripcion { get; set; }
        public virtual bool credito { get; set; }
        public virtual int dias { get; set; }
        public virtual bool activo { get; set; }
    }
}
