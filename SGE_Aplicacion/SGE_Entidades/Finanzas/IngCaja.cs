using SGE.Entidades.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.Finanzas
{
    public class IngCaja
    {
        public virtual int idIngCaja { get; set; }
        public virtual Numeracion numeracion { get; set; }
        public virtual string numero { get; set; }
        public virtual DateTime fechaCreacion { get; set; }
        public virtual Empleado responsable { get; set; }
        public virtual Moneda moneda { get; set; }
        public virtual Caja caja { get; set; }
        public virtual string observacion { get; set; }
        public virtual decimal subTotal { get; set; }
        public virtual decimal pcjImpuesto { get; set; }
        public virtual decimal monImpuesto { get; set; }
        public virtual decimal tipoCambio { get; set; }
        public virtual decimal total { get; set; }
        public virtual List<IngCajaItem> items { get; set; }
    }
}
