using SGE.AccesoDatos.Base;
using SGE.Entidades.Ventas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.AccesoDatos.Ventas
{
    public class daSolCotizacionItem : daBase<SolCotizacionItem>
    {
        public void EliminarPorIdSolCotizacionGrupo(int idSolCotizacionGrupo)
        {
            string sql = string.Format("DELETE Ventas.SolCotizacionItem WHERE idSolCotizacionGrupo = {0}", idSolCotizacionGrupo);
            Ejecutar(sql);
        }
    }
}
