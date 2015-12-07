using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.Finanzas
{
    public class IngCajaItem
    {
        public virtual int idIngCajaItem { get; set; }
        public virtual int idIngCaja { get; set; }
        public virtual string descripcion { get; set; }
        public virtual decimal cantidad { get; set; }
        public virtual decimal precio { get; set; }
        public virtual decimal total { get; set; }
    }
}
