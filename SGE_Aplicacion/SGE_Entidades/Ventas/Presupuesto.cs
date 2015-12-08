using SGE.Entidades.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.Ventas
{
    public class Presupuesto
    {
        public virtual int idPresupuesto { get; set; }
        public virtual Numeracion numeracion { get; set; }
        public virtual string numero { get; set; }
        public virtual Cliente cliente { get; set; }
        public virtual Moneda moneda { get; set; }
        public virtual DateTime fechaCreacion { get; set; }
        public virtual string instrucciones { get; set; }
        public virtual decimal total { get; set; }
        public virtual int estado { get; set; }
        public virtual List<PresupuestoItem> items { get; set; }
        public virtual List<int> idsItems { get; set; }
    }
}
