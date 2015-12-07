using SGE.Entidades.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.Finanzas
{
    public class Caja
    {
        public virtual int idCaja { get; set; }
        public virtual string descripcion { get; set; }
        public virtual decimal monto { get; set; }
        public virtual Moneda moneda { get; set; }
        public virtual bool activo { get; set; }
    }
}
