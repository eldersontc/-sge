using SGE.AccesoDatos.Base;
using SGE.Entidades.Finanzas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.AccesoDatos.Finanzas
{
    public class daIngCajaItem : daBase<IngCajaItem>
    {
        public void EliminarPorIdIngCaja(int idIngCaja)
        {
            string sql = string.Format("DELETE Finanzas.IngCajaItem WHERE idIngCaja = {0}", idIngCaja);
            Ejecutar(sql);
        }
    }
}
