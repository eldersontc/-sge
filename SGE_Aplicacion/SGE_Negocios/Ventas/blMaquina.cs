using SGE.AccesoDatos.Ventas;
using SGE.Entidades.Administracion;
using SGE.Entidades.Ventas;
using SGE.Negocios.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Negocios.Ventas
{
    public class blMaquina : blBase
    {
        public blMaquina(Sesion sesion) { base.sesion = sesion; }

        daMaquina daMaquina;

        public IList<Maquina> ObtenerTodos()
        {
            IList<Maquina> maquinas;
            try
            {
                daMaquina = new daMaquina();
                daMaquina.AbrirSesion();
                maquinas = daMaquina.ObtenerTodos();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                daMaquina.CerrarSesion();
            }
            return maquinas;
        }

        public IList<Maquina> ObtenerActivos()
        {
            IList<Maquina> maquinas;
            try
            {
                daMaquina = new daMaquina();
                daMaquina.AbrirSesion();
                List<object[]> filtros = new List<object[]>();
                filtros.Add(new object[] { "activo", true });
                maquinas = daMaquina.ObtenerLista(filtros);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                daMaquina.CerrarSesion();
            }
            return maquinas;
        }

        public bool Agregar(Maquina maquina)
        {
            try
            {
                daMaquina = new daMaquina();
                daMaquina.IniciarTransaccion();
                daMaquina.Agregar(maquina);
                daMaquina.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daMaquina.AbortarTransaccion();
                throw;
            }
            finally
            {
                daMaquina.CerrarSesion();
            }
            return true;
        }

        public bool Actualizar(Maquina maquina)
        {
            try
            {
                daMaquina = new daMaquina();
                daMaquina.IniciarTransaccion();
                Maquina almacen_ = daMaquina.ObtenerPorId(maquina.idMaquina);
                almacen_.descripcion = maquina.descripcion;
                almacen_.cntCrp = maquina.cntCrp;
                almacen_.minGrj = maquina.minGrj;
                almacen_.maxGrj = maquina.maxGrj;
                almacen_.xMinPlg = maquina.xMinPlg;
                almacen_.xMaxPlg = maquina.xMaxPlg;
                almacen_.yMinPlg = maquina.yMinPlg;
                almacen_.yMaxPlg = maquina.yMaxPlg;
                almacen_.mrgPnz = maquina.mrgPnz;
                almacen_.mrgSal = maquina.mrgSal;
                almacen_.mrgEsc = maquina.mrgEsc;
                almacen_.mrgCes = maquina.mrgCes;
                almacen_.mrgCll = maquina.mrgCll;
                almacen_.minRsl = maquina.minRsl;
                almacen_.activo = maquina.activo;
                daMaquina.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daMaquina.AbortarTransaccion();
                throw;
            }
            finally
            {
                daMaquina.CerrarSesion();
            }
            return true;
        }

        public bool Eliminar(int idMaquina)
        {
            try
            {
                daMaquina = new daMaquina();
                daMaquina.IniciarTransaccion();
                daMaquina.EliminarPorId(idMaquina, constantes.esquemas.Ventas);
                daMaquina.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daMaquina.AbortarTransaccion();
                throw;
            }
            finally
            {
                daMaquina.CerrarSesion();
            }
            return true;
        }
    }
}
