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
        public virtual List<PlantillaGrupo> grupos { get; set; }
        public virtual List<int> idsGrupos { get; set; }

        public Plantilla() {
            grupos = new List<PlantillaGrupo>();
            idsGrupos = new List<int>();
        }
    }
}
