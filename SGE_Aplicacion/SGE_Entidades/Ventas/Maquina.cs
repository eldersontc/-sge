using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.Ventas
{
    public class Maquina
    {
        public virtual int idMaquina { get; set; }
        public virtual string descripcion { get; set; }
        public virtual int cntCrp { get; set; }
        public virtual int minGrj { get; set; }
        public virtual int maxGrj { get; set; }
        public virtual int xMinPlg { get; set; }
        public virtual int xMaxPlg { get; set; }
        public virtual int yMinPlg { get; set; }
        public virtual int yMaxPlg { get; set; }
        public virtual int mrgPnz { get; set; }
        public virtual int mrgSal { get; set; }
        public virtual int mrgEsc { get; set; }
        public virtual int mrgCes { get; set; }
        public virtual int mrgCll { get; set; }
        public virtual int minRsl { get; set; }
        public virtual int maxRsl { get; set; }
        public virtual bool activo { get; set; }
    }
}
