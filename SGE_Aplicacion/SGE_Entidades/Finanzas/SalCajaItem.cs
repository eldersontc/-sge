using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.Finanzas
{
    public class SalCajaItem
    {
        public virtual int idSalCajaItem { get; set; }
        public virtual int idSalCaja { get; set; }
        public virtual string descripcion { get; set; }
        public virtual decimal cantidad { get; set; }
        public virtual decimal precio { get; set; }
        public virtual decimal total { get; set; }
    }
}
