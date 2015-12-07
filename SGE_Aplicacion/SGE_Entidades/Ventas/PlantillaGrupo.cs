using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.Ventas
{
    public class PlantillaGrupo
    {
        public virtual int idPlantillaGrupo { get; set; }
        public virtual int idPlantilla { get; set; }
        public virtual string titulo { get; set; }
        public virtual List<PlantillaItem> items { get; set; }
        public virtual List<int> idsItems { get; set; }
    }
}
