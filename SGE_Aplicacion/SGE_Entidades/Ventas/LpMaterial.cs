using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.Ventas
{
    public class LpMaterial
    {
        public virtual int idLpMaterial { get; set; }
        public virtual string descripcion { get; set; }
        public virtual bool activo { get; set; }
        public virtual List<LpMaterialItem> items { get; set; }
        public virtual List<int> idsItems { get; set; }
    }
}
