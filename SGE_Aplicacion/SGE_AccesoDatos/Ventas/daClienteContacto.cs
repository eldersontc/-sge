using SGE.AccesoDatos.Base;
using SGE.Entidades.Ventas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.AccesoDatos.Ventas
{
    public class daClienteContacto : daBase<ClienteContacto>
    {
        public void EliminarPorIdCliente(int idCliente)
        {
            string sql = string.Format("DELETE Ventas.ClienteContacto WHERE idCliente = {0}", idCliente);
            Ejecutar(sql);
        }
    }
}
