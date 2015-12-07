using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.Inventarios
{
    public class MaterialUnidad
    {
        public virtual int idMaterialUnidad { get; set; }
        public virtual int idMaterial { get; set; }
        public virtual Unidad unidad { get; set; }
        public virtual int factor { get; set; }
    }
}
