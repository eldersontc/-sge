using SGE.Entidades.Administracion;
using SGE.Entidades.Ventas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.Produccion
{
    public class OrdenProduccion
    {
        public virtual int idOrdenProduccion { get; set; }
        public virtual Numeracion numeracion { get; set; }
        public virtual string numero { get; set; }
        public virtual DateTime fechaCreacion { get; set; }
        public virtual Cliente cliente { get; set; }
        public virtual Empleado responsable { get; set; }
        public virtual int estado { get; set; }
        public virtual List<OrdenProduccionItem> items { get; set; }
        public virtual List<int> idsItems { get; set; }
    }
}
