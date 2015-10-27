using SGE.AccesoDatos.Administracion;
using SGE.Entidades.Administracion;
using SGE.Negocios.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Negocios.Administracion
{
    public class blPerfil
    {
        daPerfil daPerfil;

        public IList<Perfil> ObtenerTodos()
        {
            IList<Perfil> perfiles;
            try
            {
                daPerfil = new daPerfil();
                daPerfil.AbrirSesion();
                perfiles = daPerfil.ObtenerTodos();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                daPerfil.CerrarSesion();
            }
            return perfiles;
        }

        public IList<Perfil> ObtenerActivos()
        {
            IList<Perfil> perfiles;
            try
            {
                daPerfil = new daPerfil();
                daPerfil.AbrirSesion();
                List<object[]> filtros = new List<object[]>();
                filtros.Add(new object[]{ "activo", true });
                perfiles = daPerfil.ObtenerLista(filtros);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                daPerfil.CerrarSesion();
            }
            return perfiles;
        }

        public bool Agregar(Perfil perfil)
        {
            try
            {
                daPerfil = new daPerfil();
                daPerfil.IniciarTransaccion();
                daPerfil.Agregar(perfil);
                daPerfil.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daPerfil.AbortarTransaccion();
                throw;
            }
            finally
            {
                daPerfil.CerrarSesion();
            }
            return true;
        }

        public bool Actualizar(Perfil perfil)
        {
            try
            {
                daPerfil = new daPerfil();
                daPerfil.IniciarTransaccion();
                Perfil perfil_ = daPerfil.ObtenerPorId(perfil.idPerfil);
                perfil_.nombre = perfil.nombre;
                perfil_.activo = perfil.activo;
                daPerfil.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daPerfil.AbortarTransaccion();
                throw;
            }
            finally
            {
                daPerfil.CerrarSesion();
            }
            return true;
        }

        public bool Eliminar(int idPerfil)
        {
            try
            {
                daPerfil = new daPerfil();
                daPerfil.IniciarTransaccion();
                daPerfil.EliminarPorId(idPerfil, constantes.esquemas.Administracion);
                daPerfil.ConfirmarTransaccion();
            }
            catch (Exception)
            {
                daPerfil.AbortarTransaccion();
                throw;
            }
            finally
            {
                daPerfil.CerrarSesion();
            }
            return true;
        }
    }
}
