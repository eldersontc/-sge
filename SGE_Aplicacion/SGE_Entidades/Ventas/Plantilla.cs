using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.Ventas
{
    public class Plantilla
    {
        public virtual int idPlantilla { get; set; }
        public virtual string descripcion { get; set; }
        public virtual Linea linea { get; set; }
        public virtual bool activo { get; set; }
        public virtual List<PlantillaItem> items { get; set; }
        public virtual List<int> idsItems { get; set; }

        public Plantilla() {
            items = new List<PlantillaItem>();
            idsItems = new List<int>();
        }
    }
}
