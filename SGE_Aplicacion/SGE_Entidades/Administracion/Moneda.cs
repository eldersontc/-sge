using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.Administracion
{
    public class Moneda
    {
        public virtual int idMoneda { get; set; }
        public virtual string nombre { get; set; }
        public virtual string simbolo { get; set; }
        public virtual bool activo { get; set; }
    }
}
