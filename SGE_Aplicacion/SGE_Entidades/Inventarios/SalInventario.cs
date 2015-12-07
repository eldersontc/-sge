using SGE.Entidades.Administracion;
using SGE.Entidades.Ventas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.Inventarios
{
    public class SalInventario
    {
        public virtual int idSalInventario { get; set; }
        public virtual Numeracion numeracion { get; set; }
        public virtual string numero { get; set; }
        public virtual Cliente cliente { get; set; }
        public virtual Empleado responsable { get; set; }
        public virtual DateTime fechaCreacion { get; set; }
        public virtual Almacen almacen { get; set; }
        public virtual Moneda moneda { get; set; }
        public virtual string observacion { get; set; }
        public virtual decimal subTotal { get; set; }
        public virtual decimal tipoCambio { get; set; }
        public virtual decimal pcjImpuesto { get; set; }
        public virtual decimal monImpuesto { get; set; }
        public virtual decimal total { get; set; }
        public List<SalInventarioItem> items { get; set; }
    }
}
