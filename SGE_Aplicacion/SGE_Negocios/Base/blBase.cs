using SGE.AccesoDatos.Administracion;
using SGE.Entidades.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Negocios.Base
{
    public class blBase
    {
        public Sesion sesion { get; set; }

        public string generarNumeracion(dynamic daBase, int idNumeracion)
        {
            daNumeracion daNumeracion = new daNumeracion();
            daNumeracion.AsignarSesion(daBase);
            Numeracion numeracion = daNumeracion.ObtenerPorId(idNumeracion);
            string numero = string.Empty;
            if (numeracion.automatico) 
            {
                numeracion.numeroActual = numeracion.numeroActual + 1;
                numero = string.Format("{0}-{1}", numeracion.serie, numeracion.numeroActual.ToString().PadLeft((int)numeracion.longitudNumero, '0'));
            }
            return numero;
        }
    }
}
