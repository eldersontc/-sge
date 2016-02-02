using SGE.Entidades.Inventarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.Ventas
{
    public class PlantillaItem
    {
        public virtual int idPlantillaItem { get; set; }
        public virtual int idPlantilla { get; set; }
        public virtual string titulo { get; set; }
        public virtual Material material { get; set; }
        public virtual bool flagMA { get; set; }
        public virtual bool flagMC { get; set; }
        public virtual bool flagGRF { get; set; }
        public virtual bool flagPGS { get; set; }
        public virtual bool flagCPS { get; set; }
    }
}
