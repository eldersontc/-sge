using SGE.AccesoDatos.Base;
using SGE.Entidades.Ventas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.AccesoDatos.Ventas
{
    public class daSolCotizacionGrupo : daBase<SolCotizacionGrupo>
    {
        public void EliminarPorIdSolCotizacion(int idSolCotizacion)
        {
            string sql = string.Format("DELETE Ventas.SolCotizacionGrupo WHERE idSolCotizacion = {0}", idSolCotizacion);
            Ejecutar(sql);
        }
    }
}
