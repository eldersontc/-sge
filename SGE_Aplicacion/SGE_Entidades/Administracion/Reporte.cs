using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.Administracion
{
    public class Reporte
    {
        public virtual int idReporte { get; set; }
        public virtual string descripcion { get; set; }
        public virtual int? documento { get; set; }
        public virtual string ubicacion { get; set; }
        public virtual List<ItemReporte> items { get; set; }
        public virtual bool activo { get; set; }
        public virtual List<int> idsItems { get; set; }
    }
}
