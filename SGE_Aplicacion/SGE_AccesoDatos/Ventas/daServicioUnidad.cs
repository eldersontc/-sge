using SGE.AccesoDatos.Base;
using SGE.Entidades.Ventas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.AccesoDatos.Ventas
{
    public class daServicioUnidad : daBase<ServicioUnidad>
    {
        public void EliminarPorIdServicio(int idServicio)
        {
            string sql = string.Format("DELETE Ventas.ServicioUnidad WHERE idServicio = {0}", idServicio);
            Ejecutar(sql);
        }
    }
}
