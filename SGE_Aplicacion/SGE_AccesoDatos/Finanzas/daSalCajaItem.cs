using SGE.AccesoDatos.Base;
using SGE.Entidades.Finanzas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.AccesoDatos.Finanzas
{
    public class daSalCajaItem : daBase<SalCajaItem>
    {
        public void EliminarPorIdSalCaja(int idSalCaja)
        {
            string sql = string.Format("DELETE Finanzas.SalCajaItem WHERE idSalCaja = {0}", idSalCaja);
            Ejecutar(sql);
        }
    }
}
