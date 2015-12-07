using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.Inventarios
{
    public class Material
    {
        public virtual int idMaterial { get; set; }
        public virtual string codigo { get; set; }
        public virtual string descripcion { get; set; }
        public virtual bool inventarios { get; set; }
        public virtual bool compras { get; set; }
        public virtual bool ventas { get; set; }
        public virtual decimal costoUltimaCompra { get; set; }
        public virtual decimal costoPromedio { get; set; }
        public virtual decimal costoReferencia { get; set; }
        public virtual decimal alto { get; set; }
        public virtual decimal largo { get; set; }
        public virtual Unidad unidadBase { get; set; }
        public virtual List<MaterialUnidad> unidades { get; set; }
        public virtual List<MaterialAlmacen> almacenes { get; set; }
        public virtual List<int> idsUnidades { get; set; }
        public virtual List<int> idsAlmacenes { get; set; }
        public virtual bool activo { get; set; }
    }
}
