using SGE.Entidades.Inventarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.Ventas
{
    public class CotizacionItem
    {
        public virtual int idCotizacionItem { get; set; }
        public virtual int idCotizacionGrupo { get; set; }
        public virtual string titulo { get; set; }
        public virtual Servicio servicio { get; set; }
        public virtual Maquina maquina { get; set; }
        public virtual Material material { get; set; }
        public virtual bool flagMA { get; set; }
        public virtual bool flagMC { get; set; }
        public virtual bool flagTYR { get; set; }
        public virtual bool flagGRF { get; set; }
        public virtual bool flagMAT { get; set; }
        public virtual bool flagSRV { get; set; }
        public virtual bool flagFND { get; set; }
        public virtual decimal valXMA { get; set; }
        public virtual decimal valYMA { get; set; }
        public virtual decimal valXMC { get; set; }
        public virtual decimal valYMC { get; set; }
        public virtual decimal valTC { get; set; }
        public virtual decimal valRT { get; set; }
        public virtual decimal valFND { get; set; }
        public virtual decimal valXFI { get; set; }
        public virtual decimal valYFI { get; set; }
        public virtual decimal valSEPX { get; set; }
        public virtual decimal valSEPY { get; set; }
        public virtual int valPLG { get; set; }
        public virtual bool flagGPR { get; set; }
        public virtual bool flagGIR { get; set; }
        public virtual int valPZSP { get; set; }
        public virtual int valPZSI { get; set; }
        public virtual MetodoImpresion metodoImpresion { get; set; }
        public virtual int valMAT { get; set; }
        public virtual int valDEM { get; set; }
        public virtual int valPRD { get; set; }
        public virtual int valCNT { get; set; }
        public virtual int valPGS { get; set; }
        public virtual string observacion { get; set; }
        public virtual bool flagINCP { get; set; }
        public virtual bool flagPRECP { get; set; }
        public virtual decimal valTLMAQ { get; set; }
        public virtual decimal valTLMAT { get; set; }
        public virtual decimal valTLSRV { get; set; }
        public virtual decimal valTOTAL { get; set; }
        public virtual List<CotizacionServicio> servicios { get; set; }
        public virtual List<int> idsServicios { get; set; }

        public CotizacionItem() {
            servicios = new List<CotizacionServicio>();
            idsServicios = new List<int>();
        }
    }
}
