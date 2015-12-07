using SGE.AccesoDatos.Base;
using SGE.Entidades.Ventas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.AccesoDatos.Ventas
{
    public class daClienteDireccion : daBase<ClienteDireccion>
    {
        public void EliminarPorIdCliente(int idCliente)
        {
            string sql = string.Format("DELETE Ventas.ClienteDireccion WHERE idCliente = {0}", idCliente);
            Ejecutar(sql);
        }
    }
}
